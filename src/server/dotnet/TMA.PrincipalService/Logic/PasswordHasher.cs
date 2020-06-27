using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TMA.PrincipalService.Logic
{
    public class PasswordHasher
    {
        public const int Iterations = 100000;
        public const int HashSize = 24;
        public const int SaltSize = 64;

        public bool Verify(string password, string hashDB, string salt)
        {
            var hash = HashPassword(password, salt);

            return hash.Equals(hashDB, StringComparison.Ordinal);
        }

        public string HashPassword(string password, string salt)
        {
            var joined = password + salt;

            using SHA512 sha512Managed = new SHA512Managed();
            var result = sha512Managed.ComputeHash(Encoding.UTF8.GetBytes(joined));

            return Convert.ToBase64String(result);
        }

        public string CreateSalt()
        {
            var saltBytes = new byte[SaltSize];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }
    }
}
