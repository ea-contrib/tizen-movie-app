using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace TMA.Identity
{
    public static class ClaimsIdentityExtensions
    {
        #region Helper methods

        public static string GetStringClaim(this ClaimsIdentity identity, string claimType)
        {
            return identity.FindFirst(claimType)?.Value;
        }

        public static int GetIntClaim(this ClaimsIdentity identity, string claimType)
        {
            var strValue = identity.FindFirst(claimType)?.Value;
            return int.TryParse(strValue, out var intValue) ? intValue : int.MinValue;
        }

        public static void SetStringClaim(this ClaimsIdentity identity, string claimType, string value)
        {
            identity.RemoveAllClaimsOfType(claimType);
            if (!string.IsNullOrEmpty(value))
                identity.AddClaim(new Claim(claimType, value));
        }

        public static void RemoveAllClaimsOfType(this ClaimsIdentity identity, string claimType)
        {
            var oldClaims = identity.FindAll(claimType);
            foreach (var claim in oldClaims.ToList())
            {
                identity.RemoveClaim(claim);
            }
        }

        public static bool? GetBoolClaim(this ClaimsIdentity identity, string claimType)
        {
            var str = identity.FindFirst(claimType)?.Value;
            bool val;
            if (bool.TryParse(str, out val))
            {
                return val;
            }

            return null;
        }

        public static void SetBoolClaim(this ClaimsIdentity identity, string claimType, bool? value)
        {
            identity.RemoveAllClaimsOfType(claimType);
            if (value.HasValue)
                identity.AddClaim(new Claim(claimType, value.ToString(), ClaimValueTypes.Boolean));
        }

        public static DateTime? GetDateTimeClaim(this ClaimsIdentity identity, string claimType)
        {
            var str = identity.FindFirst(claimType)?.Value;
            DateTime val;
            if (DateTime.TryParseExact(str, "u", new CultureInfo("en-US"), DateTimeStyles.None, out val))
            {
                return val;
            }

            return null;
        }

        public static void SetDateTimeClaim(this ClaimsIdentity identity, string claimType, DateTime? value)
        {
            identity.RemoveAllClaimsOfType(claimType);
            if (value.HasValue)
                identity.AddClaim(new Claim(claimType, value?.ToUniversalTime().ToString("u"), ClaimValueTypes.DateTime));
        }

        public static Guid? GetGuidClaim(this ClaimsIdentity identity, string claimType)
        {
            var str = identity.FindFirst(claimType)?.Value;
            if (!string.IsNullOrEmpty(str))
            {
                return new Guid(Convert.FromBase64String(str));
            }

            return null;
        }

        public static void SetGuidClaim(this ClaimsIdentity identity, string claimType, Guid? value)
        {
            identity.RemoveAllClaimsOfType(claimType);
            if (value.HasValue) identity.AddClaim(new Claim(claimType, Convert.ToBase64String(value?.ToByteArray())));
        }

        public static List<string> GetStringListClaim(this ClaimsIdentity identity, string claimType)
        {
            var claims = identity.FindAll(claimType);
            var items = claims.Select(str => str.Value).ToList();
            return items.Distinct().ToList();
        }

        public static void SetStringListClaim(this ClaimsIdentity identity, string claimType, List<string> value)
        {
            identity.RemoveAllClaimsOfType(claimType);
            foreach (var guid in value)
            {
                identity.AddClaim(new Claim(claimType, guid));
            }
        }

        public static List<Guid> GetGuidListClaim(this ClaimsIdentity identity, string claimType)
        {
            var items = new List<Guid>();
            var claims = identity.FindAll(claimType);
            foreach (var claim in claims.SelectMany(s => s.Value.Split('_')))
            {
                if (!string.IsNullOrEmpty(claim))
                {
                    items.Add(new Guid(Convert.FromBase64String(claim)));
                }
            }

            return items.Distinct().ToList();
        }

        public static void SetGuidListClaim(this ClaimsIdentity identity, string claimType, List<Guid> value)
        {
            identity.RemoveAllClaimsOfType(claimType);
            identity.AddClaim(new Claim(claimType,
                string.Join("_", value.Select(x => Convert.ToBase64String(x.ToByteArray())))));
        }

        #endregion


        public static string GetName(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.UserName);
        }

        public static ClaimsIdentity SetName(this ClaimsIdentity identity, string name)
        {
            identity.SetStringClaim(PrincipalClaims.UserName, name);
            return identity;
        }

        public static string GetFirstName(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.FirstName);
        }

        public static ClaimsIdentity SetFirstName(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.FirstName, value);
            return identity;
        }

        public static string GetLastName(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.LastName);
        }

        public static ClaimsIdentity SetLastName(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.LastName, value);
            return identity;
        }

        public static string GetEmail(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.Email);
        }

        public static ClaimsIdentity SetEmail(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.Email, value);
            return identity;
        }

        public static string GetSessionId(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.SessionId);
        }

        public static ClaimsIdentity SetSessionId(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.SessionId, value);
            return identity;
        }

        public static string GetExternalIdentifier(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.ExternalIdentifier);
        }

        public static ClaimsIdentity SetExternalIdentifier(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.ExternalIdentifier, value);
            return identity;
        }

        public static int GetSubjectId(this ClaimsIdentity identity)
        {
            return identity.GetIntClaim(PrincipalClaims.SubjectId);
        }

        public static ClaimsIdentity SetSubjectId(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.SubjectId, value);
            return identity;
        }

        public static string GetAccessToken(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.AccessToken);
        }

        public static ClaimsIdentity SetAccessToken(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.AccessToken, value);
            return identity;
        }

        public static string GetRefreshToken(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.RefreshToken);
        }

        public static ClaimsIdentity SetRefreshToken(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.RefreshToken, value);
            return identity;
        }

        public static string GetIdentityToken(this ClaimsIdentity identity)
        {
            return identity.GetStringClaim(PrincipalClaims.IdentityToken);
        }

        public static ClaimsIdentity SetIdentityToken(this ClaimsIdentity identity, string value)
        {
            identity.SetStringClaim(PrincipalClaims.IdentityToken, value);
            return identity;
        }

        public static DateTime? GetExpiresAt(this ClaimsIdentity identity)
        {
            return identity?.GetDateTimeClaim(PrincipalClaims.ExpiresAt);
        }

        public static ClaimsIdentity SetExpiresAt(this ClaimsIdentity identity, DateTime expireTime)
        {
            identity?.SetDateTimeClaim(PrincipalClaims.ExpiresAt, expireTime);
            return identity;
        }

    }
}