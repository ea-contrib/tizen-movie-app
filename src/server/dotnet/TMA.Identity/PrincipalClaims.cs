using System.Collections.Generic;

namespace TMA.Identity
{
    public static class PrincipalClaims
    {
        public const string UserName = "name";
        public const string FirstName = "first_name";
        public const string LastName = "last_name";
        public const string Email = "email";
        public const string SessionId = "sid";
        public const string ExternalIdentifier = nameof(ExternalIdentifier);
        public const string SubjectId = "sub";
        public const string AccessToken = "access_token";
        public const string RefreshToken = "refresh_token";
        public const string IdentityToken = "id_token";
        public const string ExpiresAt = nameof(ExpiresAt);

        public static List<string> All =>
            new List<string>
            {
                UserName,
                FirstName,
                LastName,
                Email,
                SessionId,
                ExternalIdentifier,
                SubjectId,
                AccessToken,
                RefreshToken,
                IdentityToken,
                ExpiresAt,
            };
    }
}