using System;
using System.Security.Claims;

namespace TMA.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Claimses the identity.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>ClaimsIdentity.</returns>
        public static ClaimsIdentity ClaimsIdentity(this ClaimsPrincipal claimsPrincipal)
        {
            return (ClaimsIdentity) claimsPrincipal.Identity;
        }

        public static int Id(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetSubjectId();
        }

        public static int SubjectId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetSubjectId();
        }

        public static string Name(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetName();
        }

        public static string FirstName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetFirstName();
        }

        public static string LastName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetLastName();
        }

        public static string Email(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetEmail();
        }

        public static string SessionId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetSessionId();
        }

        public static string ExternalId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetExternalIdentifier();
        }

        public static string AccessToken(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetAccessToken();
        }

        public static string RefreshToken(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetRefreshToken();
        }

        public static string IdentityToken(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetIdentityToken();
        }

        public static DateTime? ExpiresAt(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.ClaimsIdentity().GetExpiresAt();
        }

    }
}