using System;
using System.Security.Cryptography;
using System.Text;

namespace Grasews.Infra.CrossCutting.Helpers
{
    public static class CryptHelper
    {
        private const string KEY = "grasews";

        //public static string GetMD5(string value)
        //{
        //    var valueBytes = Encoding.ASCII.GetBytes(value);

        //    var hash = MD5.Create().ComputeHash(valueBytes);

        //    var stringBuilder = new StringBuilder();

        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        stringBuilder.Append(hash[i].ToString("X2"));
        //    }

        //    return stringBuilder.ToString();
        //}

        public static string Encrypt(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);

            using (var MD5Crypto = new MD5CryptoServiceProvider())
            {
                var hash = MD5Crypto.ComputeHash(Encoding.UTF8.GetBytes(KEY));

                MD5Crypto.Clear();

                using (var tripleDESCrypto = new TripleDESCryptoServiceProvider
                {
                    Key = hash,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    var cTransform = tripleDESCrypto.CreateEncryptor();

                    var resultArray = cTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);

                    tripleDESCrypto.Clear();

                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
            }
        }

        public static string Decrypt(string value)
        {
            var valueBytes = Convert.FromBase64String(value);

            using (var MD5Crypto = new MD5CryptoServiceProvider())
            {
                var hash = MD5Crypto.ComputeHash(Encoding.UTF8.GetBytes(KEY));

                MD5Crypto.Clear();

                using (var tripleDESCrypto = new TripleDESCryptoServiceProvider { Key = hash, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    var cTransform = tripleDESCrypto.CreateDecryptor();

                    var resultArray = cTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);

                    tripleDESCrypto.Clear();

                    return Encoding.UTF8.GetString(resultArray);
                }
            }
        }
    }
}