using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using TMA.Common;

namespace TMA.IdentityService.ResponseHandling
{
    public class CustomAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public CustomAuthorizeRequestValidator(IAuthenticationSchemeProvider schemeProvider)
        {
            _schemeProvider = schemeProvider;
        }
        /// <summary>
        /// Custom validation logic for the authorize request.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            var idp = context.Result?.ValidatedRequest?.GetIdP();

            if (!idp.IsNullOrWhiteSpace())
            {
                var authenticationSchemes = (await _schemeProvider.GetAllSchemesAsync()).ToList();

                if (!authenticationSchemes.Any(x => x.Name.Equals(idp, StringComparison.InvariantCultureIgnoreCase)))
                {
                    context.Result?.ValidatedRequest.RemoveIdP();
                }
            }
        }
    }
}