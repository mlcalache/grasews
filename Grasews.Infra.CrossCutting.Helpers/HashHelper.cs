using System;
using System.Security.Cryptography;

namespace Grasews.Infra.CrossCutting.Helpers
{
    public static class HashHelper
    {
        public static string GetHash(string input)
        {
            using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = System.Text.Encoding.UTF8.GetBytes(input);

                var byteHash = hashAlgorithm.ComputeHash(byteValue);

                return Convert.ToBase64String(byteHash);
            }
        }
    }
}