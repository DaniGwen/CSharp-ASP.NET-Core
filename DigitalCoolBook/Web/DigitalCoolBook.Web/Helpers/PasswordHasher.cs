using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DigitalCoolBook.App.Helpers
{
    public static class PasswordHasher
    {
        private static string HashPassword(string password)
        {
            // Add Salt to password
            byte[] salt = new byte[128 / 8];

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed.ToString();
        }
    }
}
