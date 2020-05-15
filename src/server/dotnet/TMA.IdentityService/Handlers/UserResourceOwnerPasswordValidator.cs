using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using TMA.Identity;
using TMA.IdentityService.Logic;

namespace TMA.IdentityService.Handlers
{
    /// <summary>Resource owner password validator for test users</summary>
    /// <seealso cref="T:IdentityServer4.Validation.IResourceOwnerPasswordValidator" />
    public class UserResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ISystemClock _clock;
        private readonly UserStore _users;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserResourceOwnerPasswordValidator" /> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="clock">The clock.</param>
        public UserResourceOwnerPasswordValidator(UserStore users, ISystemClock clock)
        {
            _users = users;
            _clock = clock;
        }

        /// <summary>Validates the resource owner password credential</summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await _users.IsCredentialsValid(context.UserName, context.Password))
            {
                var byUsername =await _users.FindByUsername(context.UserName);
                var validationContext = context;
                var subjectId = byUsername.Id();
                var validationResult = new GrantValidationResult(subjectId.ToString(), "pwd", _clock.UtcNow.UtcDateTime, byUsername.Claims);
                validationContext.Result = validationResult;
            }
        }
    }
}