using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Fungry.API.Services
{
    public class PasswordService
    {

        private const int SaltSize = 12;

        public (String salt, String hashedpassword) GenerateSaltAndHash(String plainPassword)
        {
            if(string.IsNullOrWhiteSpace(plainPassword))
                    throw new ArgumentNullException(nameof(plainPassword));

            var buffer = RandomNumberGenerator.GetBytes(SaltSize);
            var salt = Convert.ToBase64String(buffer);

            
            var hashedPassword =GenerateHashPassword(plainPassword, salt);
            return (salt, hashedPassword);


        }

        public bool AreEqual (string plainPassword,string salt,string hashedPassword)
        {
            var newHashedPassword = GenerateHashPassword(plainPassword, salt);
            return newHashedPassword == hashedPassword; 
        }

        private  static string GenerateHashPassword(string plainPassword,string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            var hash = SHA256.HashData(bytes);

            return Convert.ToBase64String(hash);
            
        } 
    }
}
