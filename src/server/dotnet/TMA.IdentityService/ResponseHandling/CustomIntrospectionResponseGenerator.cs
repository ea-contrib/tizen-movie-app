using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using TMA.IdentityService.Extensions;

namespace TMA.IdentityService.ResponseHandling
{
    /// <summary>
    /// The introspection response generator
    /// </summary>
    /// <seealso cref="IdentityServer4.ResponseHandling.IIntrospectionResponseGenerator" />
    public class CustomIntrospectionResponseGenerator : IntrospectionResponseGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomIntrospectionResponseGenerator"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="logger">The logger.</param>
        public CustomIntrospectionResponseGenerator(IEventService events, ILogger<IntrospectionResponseGenerator> logger) : base(events, logger)
        {
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns></returns>
        public override async Task<Dictionary<string, object>> ProcessAsync(IntrospectionRequestValidationResult validationResult)
        {
            Logger.LogTrace("Creating introspection response");

            // standard response
            var response = new Dictionary<string, object>
            {
                { "active", false }
            };

            // token is invalid
            if (validationResult.IsActive == false)
            {
                Logger.LogDebug("Creating introspection response for inactive token.");
                await Events.RaiseAsync(new TokenIntrospectionSuccessEvent(validationResult));

                return response;
            }

            // expected scope not present
            if (await AreExpectedScopesPresentAsync(validationResult) == false)
            {
                return response;
            }
            Logger.LogDebug("Creating introspection response for active token.");


            var allowedClaims = validationResult.Api.UserClaims.Distinct();

            // get all allowed claims (without scopes)
            response = validationResult.Claims.Where(c => c.Type != JwtClaimTypes.Scope && allowedClaims.Contains(c.Type) || c.Type == JwtClaimTypes.Expiration).ToClaimsDictionary();

            // add active flag
            response.Add("active", true);

            // calculate scopes the caller is allowed to see
            var allowedScopes = validationResult.Api.Scopes.Select(x => x.Name);
            var scopes = validationResult.Claims.Where(c => c.Type == JwtClaimTypes.Scope).Select(x => x.Value);
            scopes = scopes.Where(x => allowedScopes.Contains(x));
            response.Add("scope", scopes.ToSpaceSeparatedString());

            await Events.RaiseAsync(new TokenIntrospectionSuccessEvent(validationResult));
            return response;
        }
    }
}