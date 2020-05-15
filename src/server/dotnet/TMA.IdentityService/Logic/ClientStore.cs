using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sustainsys.Saml2;
using Sustainsys.Saml2.AspNetCore2;
using Sustainsys.Saml2.Configuration;
using Sustainsys.Saml2.Metadata;
using TMA.Configuration;
using TMA.IdentityService.Contracts;
using TMA.IdentityService.Handlers;
using TMA.IdentityService.Helpers;
using Secret = IdentityServer4.Models.Secret;
using TMA.Common;

namespace TMA.IdentityService.Logic
{
    public class ClientStore : IClientStore, ICorsClientStore
    {
        private readonly IConfiguration _configuration;
        private const int IdentityTokenLifetime = 3600;
        private const int AccessTokenLifetime = 3600;

        private readonly HashSet<string> _applicationDomains = new HashSet<string>();
        public IAuthenticationSchemeProvider SchemeProvider { get; }
        public IOptionsMonitorCache<Saml2Options> Saml2OptionsCache { get; }
        public IOptionsMonitorCache<OAuthOptions> OAuthOptionsCache { get; }
        public IDataProtectionProvider DataProtectionProvider { get; }
        public ILoggerFactory LoggerFactory { get; }
        public UserStore UserStore { get; }


        private const string SamlCertificateFileName = "ppl_saml_cert.pfx";

        public ClientStore(
            IAuthenticationSchemeProvider schemeProvider,
            IOptionsMonitorCache<OAuthOptions> oAuthOptionsCache, IOptionsMonitorCache<Saml2Options> saml2OptionsCache,
            IDataProtectionProvider dataProtectionProvider, UserStore userStore, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            SchemeProvider = schemeProvider;
            Saml2OptionsCache = saml2OptionsCache;
            OAuthOptionsCache = oAuthOptionsCache;
            DataProtectionProvider = dataProtectionProvider;
            LoggerFactory = loggerFactory;
            UserStore = userStore;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(GetStaticClients().FirstOrDefault(x => x.ClientId.Equals(clientId))
                ?? GetTestIdpClient(clientId));
        }

        private List<Client> GetStaticClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ab-spa",
                    ClientName = "A&B SPA",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = GenerateClientAppUrls(new []{"/", "/silent-renew.html"}),
                    PostLogoutRedirectUris = GenerateClientAppUrls("/"),
                    AllowedCorsOrigins = GenerateClientAppUrls("/"),
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        Config.IdentityResourceMvc,
                        Config.ApiResourceWeb,
                        Config.OfflineAccess
                    },
                    AccessTokenLifetime = AccessTokenLifetime,
                    IdentityTokenLifetime = IdentityTokenLifetime,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = false,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    AccessTokenType = AccessTokenType.Reference,
                }
            };
        }

        private Client GetTestIdpClient(string clientId)
        {
            if (_configuration.IsPostmanClientEnabled() && clientId.Equals("postman", StringComparison.InvariantCulture))
            {
                return new Client
                {
                    ClientId = "postman",
                    ClientSecrets = { new Secret(_configuration.PostmanClientSecret().Sha256()) },
                    ClientName = "Postman Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        Config.IdentityResourceMvc,
                        Config.ApiResourceWeb
                    },
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = AccessTokenLifetime,
                    IdentityTokenLifetime = IdentityTokenLifetime,
                    AllowOfflineAccess = true,
                    EnableLocalLogin = true,
                };
            }

            return null;
        }

        private List<string> GenerateClientAppUrls(string[] relativeAppUrl)
        {
            return relativeAppUrl.SelectMany(GenerateClientAppUrls).ToList();
        }

        private List<string> GenerateClientAppUrls(string relativeAppUrl)
        {
            var appUrl = _configuration.AppUrl();
            var applicationName = new Uri(appUrl).PathAndQuery.Replace("/","");

            var applicationDomains = new List<string> {appUrl};
            applicationDomains.AddRange(_applicationDomains);

            return UrlHelper.GenerateDomainUrls($"/{applicationName}{relativeAppUrl}".Replace("//", "/"), applicationDomains);
        }

       

        public void InitStore()
        {
            InitializeApplicationDomainsFromConfig();

            // if (authScheme.AuthType == AuthType.OAuth2)
            // {
            //     AddOrUpdate($"{authScheme.ProgramId}", "OAuth 2", config);
            // }
            // else if (authScheme.AuthType == AuthType.Saml)
            // {
            //     AddOrUpdate($"{authScheme.ProgramId}", "WSO", config);
            // }
            // else
            // {
            //     RemoveScheme($"{authScheme.ProgramId}");
            // }
        }



        private void InitializeApplicationDomainsFromConfig()
        {
            foreach (var siteDomain in _configuration.AllowedHosts())
            {
                var domainWithPort = siteDomain.Trim('/').Split(":");
                string domain = siteDomain;
                int port = -1;
                if (domainWithPort.Length > 1)
                {
                    domain = domainWithPort[0];

                    if (int.TryParse(domainWithPort[1], out var parsedPort))
                    {
                        port = parsedPort;
                    }
                }

                UriBuilder builder = new UriBuilder(Uri.UriSchemeHttp, domain, port);
                _applicationDomains.Add(builder.ToString());
            }

            _applicationDomains.Add(_configuration.IdpUrl());
            _applicationDomains.Add(_configuration.AppUrl());

        }

        private void RemoveScheme(string scheme)
        {
            if (SchemeProvider.GetSchemeAsync(scheme).Result != null)
            {
                SchemeProvider.RemoveScheme(scheme);
                OAuthOptionsCache.TryRemove(scheme);
                Saml2OptionsCache.TryRemove(scheme);
            }
        }

        private void AddOrUpdate(string scheme, string displayName, OAuth2Configuration options)
        {
            RemoveScheme(scheme);
            SchemeProvider.AddScheme(new AuthenticationScheme(scheme, displayName, typeof(ExtendedOAuthHandler<OAuthOptions>)));

            var dataProtectionProvider = DataProtectionProvider.CreateProtector(scheme);

            var oauthSettings = new OAuthOptions
            {
                AuthorizationEndpoint = options.AuthorizationEndpointUrl,
                TokenEndpoint = options.TokenEndpointUrl,
                UserInformationEndpoint = options.UserInformationEndpoint,
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                CallbackPath = new PathString($"/signin-oauth2{scheme}"),
                SaveTokens = true,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                StateDataFormat = new PropertiesDataFormat(dataProtectionProvider),
                DataProtectionProvider = dataProtectionProvider,
                Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var claims = context.Principal.Claims.ToList();
                        if (!context.AccessToken.IsNullOrWhiteSpace())
                        {

                            try
                            {
                                var jwtToken = new JwtSecurityToken(context.AccessToken);
                                claims.AddRange(jwtToken.Claims);
                            }
                            catch (ArgumentException)
                            {
                                
                            }
                        }

                        if (context.Options?.UserInformationEndpoint?.IsNullOrWhiteSpace() == false)
                        {
                            var tokenClient = new UserInfoClient(context.Options.UserInformationEndpoint);
                            var userInfo = await tokenClient.GetAsync(context.AccessToken);

                            if (userInfo.Claims?.Any() == true)
                            {
                                claims.AddRange(userInfo?.Claims);
                            }
                        }
                        context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Identity.AuthenticationType));
                    }
                },
                Backchannel = new HttpClient(new HttpClientHandler())
                {
                    MaxResponseContentBufferSize = 1024 * 1024 * 10,
                    Timeout = TimeSpan.FromSeconds(60)
                }
            };

            if (!options.Scope.IsNullOrWhiteSpace())
            {
                foreach (var scope in options.Scope.Split().Where(x=>!x.IsNullOrWhiteSpace()))
                {
                    oauthSettings.Scope.Add(scope);
                }
            }
            OAuthOptionsCache.TryAdd(scheme, oauthSettings);
        }

        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return Task.FromResult(GetStaticClients().Any(x =>
                x.AllowedCorsOrigins.Any(corsOrigin => new Uri(corsOrigin).Host.Equals(new Uri(origin).Host))));
        }
    }
}
