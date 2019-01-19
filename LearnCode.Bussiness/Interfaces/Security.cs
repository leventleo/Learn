using System;
using System.Text;

namespace LLearnCode.Bussiness.Interfaces
{
    public class Security
    {


        public static string HashCompute(string value, string salt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hmac.Key = Encoding.UTF8.GetBytes(salt);
                var hashString= Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(value)));
                return hashString;
            }


        }

    }
}
