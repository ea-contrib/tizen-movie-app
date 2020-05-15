using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Autofac;
using AutoMapper;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using TMA.Configuration;

namespace TMA.IdentityService
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddControllersWithViews();

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddIdentityServer(x => x.Csp = new CspOptions()
                {
                    AddDeprecatedHeader = true,
                    Level = CspLevel.One
                })
                .AddDeveloperSigningCredential();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
            });

            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();


            services.AddCors();


            services.PostConfigure<IdentityServerOptions>(o =>
            {
                var idpUrl = new Uri(Configuration.IdpUrl());
                o.PublicOrigin = idpUrl.GetLeftPart(UriPartial.Authority);
                o.IssuerUri = Configuration.IdpUrl().TrimEnd('/');
                o.Authentication.CookieSlidingExpiration = true;
                o.Authentication.CookieLifetime = TimeSpan.FromMinutes(Configuration.SessionExpirationInMinutes());
                o.Events.RaiseSuccessEvents = true;
                o.Events.RaiseFailureEvents = true;
                o.Events.RaiseErrorEvents = true;
            });

        }

        public void Configure(IApplicationBuilder app, IdentityServerOptions identityServerOptions)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.Use((context, next) =>
            {
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();
                return next();
            });

            app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.All
                });

            app.UseCors();

            var culture = "en-US";
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture),
                // Formatting numbers, dates, etc.
                SupportedCultures = new List<CultureInfo> { new CultureInfo(culture) },
                // UI strings that we have localized.
                SupportedUICultures = new List<CultureInfo> { new CultureInfo(culture) },
            });


            app.UseSession();
           
            var idpUrl = new Uri(Configuration.IdpUrl());

            var pathBase = idpUrl.AbsolutePath?.TrimEnd('/');
            if (string.IsNullOrEmpty(pathBase))
            {
                app.UseStaticFiles();
                app.UseRouting();
                app.UseIdentityServer();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });
            }
            else
            {
                app.Map(pathBase, idp =>
                {
                    idp.UseStaticFiles();
                    idp.UseRouting();
                    idp.UseIdentityServer();
                    idp.UseAuthorization();
                    idp.UseEndpoints(endpoints =>
                    {
                        endpoints.MapDefaultControllerRoute();
                    });
                });
            }

        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
          
            // builder.RegisterType<Mappings.GrantProfile>().As<Profile>().SingleInstance();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in ctx.Resolve<IList<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<IdentityServiceModule>();
            RegisterAutoMapper(builder);
        }
    }
}
