using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using TMA.Configuration;
using TMA.Identity;

namespace TMA.IdentityService.Logic
{
    public class ResourceStore : IResourceStore
    {
        private readonly IConfiguration _configuration;
        private IEnumerable<IdentityResource> IdentityResources => GetIdentityResources();
        private IEnumerable<ApiResource> ApiResources => GetApiResources();

        public ResourceStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns></returns>
        public Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
        {
            var result = new IdentityServer4.Models.Resources(IdentityResources, ApiResources);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Finds the API resource by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var api = from a in ApiResources
                where a.Name == name
                select a;
            return Task.FromResult(api.FirstOrDefault());
        }

        /// <summary>
        /// Finds the identity resources by scope.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">names</exception>
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));

            var identity = from i in IdentityResources
                where names.Contains(i.Name)
                select i;

            return Task.FromResult(identity);
        }

        /// <summary>
        /// Finds the API resources by scope.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">names</exception>
        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));

            var api = from a in ApiResources
                let scopes = (from s in a.Scopes where names.Contains(s.Name) select s)
                where scopes.Any()
                select a;

            return Task.FromResult(api);
        }
    
        protected IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResource(Config.IdentityResourceMvc, "SPA", PrincipalClaims.All),
            };
        }

        protected IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(Config.ApiResourceWeb, "Web API", PrincipalClaims.All)
                {
                    ApiSecrets = new List<Secret>
                    {
                        new Secret(_configuration.ApiSecret().Sha256())
                    }
                }
            };
        }
    }
}