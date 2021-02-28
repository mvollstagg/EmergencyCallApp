using System;
using System.Security.Cryptography;
using System.Text;

namespace EmergencyCall.Services.Helpers
{
    public class HashHelper
    {
        public static string CreatePasswordHash(string password, string secretKey)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (secretKey == null) throw new ArgumentNullException(nameof(secretKey));
            StringBuilder hash = new StringBuilder();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            using (var hmac = new HMACSHA512(secretKeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(passwordBytes);
                foreach (var value in hashValue)
                {
                    hash.Append(value.ToString("x2"));
                }
            }
            return hash.ToString();
        }
        public static bool VerifyPasswordHash(string password, string secretKey, string passwordHash)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (secretKey == null) throw new ArgumentNullException(nameof(secretKey));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            string hash = CreatePasswordHash(password, secretKey);
            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
