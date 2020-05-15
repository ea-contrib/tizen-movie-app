using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using TMA.Common;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MessageBus;

namespace TMA.IdentityService.Logic
{
    public class UserStore
    {
        private readonly IMessageBus _serviceBus;

        public UserStore(IMessageBus serviceBus)
        {
            _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        }

        /// <summary>Validates the credentials.</summary>
        /// <param name="login">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<bool> IsCredentialsValid(string login, string password)
        {
            var response = await ValidateCredentials(login, password);
            return response;
        }

        /// <summary>Validates the credentials.</summary>
        /// <param name="login">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private async Task<bool> ValidateCredentials(string login, string password)
        {
            var principalResponse = await _serviceBus.PublishAsync<IsPrincipalCredentialsValidCommand, ResponseMessage<bool>>(
                new IsPrincipalCredentialsValidCommand()
                {
                    Email = login,
                    Password = password
                });

            return principalResponse.Value;
        }

        /// <summary>Finds the user by subject identifier.</summary>
        /// <param name="id">The subject identifier.</param>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> FindById(int id)
        {
            var claimsResponse = await _serviceBus.PublishAsync<GetPrincipalClaimsCommand, ResponseMessage<ClaimsDto>>(
                new GetPrincipalClaimsCommand()
                {
                    Id = id
                });

            if (claimsResponse.Value?.Claims != null)
            {
                return CreateClaimsPrincipal(claimsResponse.Value.Claims);
            }

            return null;
        }

        /// <summary>Finds the user by username.</summary>
        /// <param name="email">The username.</param>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> FindByUsername(string email)
        {
            var claimsResponse = await _serviceBus.PublishAsync<GetPrincipalClaimsCommand, ResponseMessage<ClaimsDto>>(
                new GetPrincipalClaimsCommand()
                {
                    Email = email
                });

            if (claimsResponse.Value?.Claims != null)
            {
                return CreateClaimsPrincipal(claimsResponse.Value.Claims);
            }

            return null;
        }


        private static ClaimsPrincipal CreateClaimsPrincipal(List<Tuple<string, string, string>> response)
        {
            return new ClaimsPrincipal(
                new ClaimsIdentity(response.Select(x => new Claim(x.Item1, x.Item2, x.Item3)), "oidc"));
        }


        /// <summary>Automatically provisions a user.</summary>
        /// <param name="provider">The provider.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var surname = claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value
                          ?? claimsPrincipal.FindFirst(JwtClaimTypes.FamilyName)?.Value
                          ?? claimsPrincipal.FindFirst("surname")?.Value
                          ?? claimsPrincipal.FindFirst("last_name")?.Value;

            var forename = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value
                           ?? claimsPrincipal.FindFirst(JwtClaimTypes.GivenName)?.Value
                           ?? claimsPrincipal.FindFirst("forename")?.Value
                           ?? claimsPrincipal.FindFirst("first_name")?.Value;

            if (forename.IsNullOrWhiteSpace() && surname.IsNullOrWhiteSpace())
            {
                var name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value
                           ?? claimsPrincipal.FindFirst(JwtClaimTypes.Name)?.Value
                           ?? claimsPrincipal.FindFirst("display_name")?.Value;
                if (!name.IsNullOrWhiteSpace())
                {
                    var nameParts = name.Split().Where(x => !x.IsNullOrWhiteSpace()).ToArray();

                    if (nameParts.Any() && nameParts.Count() >= 2)
                    {
                        surname = nameParts[0];
                        forename = nameParts[1];
                    }
                }
            }
            //
            // var response = await _serviceBus.PublishAsync<SaveRequestMessage<PrincipalInfo>, ReplyMessage<string>>(
            //     new SaveRequestMessage<PrincipalInfo>()
            //     {
            //        Data = new PrincipalInfo
            //        {
            //            Surname = surname,
            //            Forename = forename,
            //            Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ??
            //                    claimsPrincipal.FindFirst(JwtClaimTypes.Email)?.Value,
            //            Password = Guid.NewGuid().ToString(),
            //            ExternalId = FormatExternalId(provider, userId),
            //        }
            //     });
            //
            // if (response.Value != null)
            // {
            //     return await FindById(response.Value);
            // }

            return null;
        }

        private string FormatExternalId(string provider, string userId)
        {
            return $"{provider}{userId}";
        }

        public bool IsActive(int id)
        {
            return true;
        }
    }
}