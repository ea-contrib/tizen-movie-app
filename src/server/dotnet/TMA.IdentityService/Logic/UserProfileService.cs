using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using TMA.Identity;
using TMA.Common;

namespace TMA.IdentityService.Logic
{
    public class UserProfileService : IProfileService
    {
        protected ILogger Logger { get; }
        protected UserStore UserStore { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultProfileService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UserProfileService(ILogger<UserProfileService> logger, UserStore userStore)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            UserStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            List<Claim> claims = await GetRegularModeClaims(context);

            context.AddRequestedClaims(claims);
            context.LogIssuedClaims(Logger);
        }

        public virtual async Task<List<Claim>> GetRegularModeClaims(ProfileDataRequestContext context)
        {
            var claimsPrincipal = await UserStore.FindById(context.Subject.SubjectId());
            if (!context.ValidatedRequest?.SessionId?.IsNullOrWhiteSpace() ?? false)
            {
                claimsPrincipal.ClaimsIdentity().SetSessionId(context.ValidatedRequest.SessionId);
            }

            var claims = ExtendClaimsList(new List<Claim>(claimsPrincipal.Claims));

            foreach (var subjectClaimsGroup in context.Subject.Claims.GroupBy(x => x.Type))
            {
                if (claims.Any(x => x.Type.Equals(subjectClaimsGroup.Key, StringComparison.InvariantCultureIgnoreCase)))
                {
                    claims.RemoveAll(x => x.Type.Equals(subjectClaimsGroup.Key, StringComparison.InvariantCultureIgnoreCase));
                }
                claims.AddRange(subjectClaimsGroup);
            }

            return claims;
        }
        
        public static List<Claim> ExtendClaimsList(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims);

            return identity.Claims.ToList();
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);
            var subjectId = context.Subject.SubjectId();
            context.IsActive = UserStore.IsActive(subjectId);
            return Task.CompletedTask;
        }
    }
}