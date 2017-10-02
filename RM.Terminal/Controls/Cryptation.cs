using System;
using System.Security.Cryptography;
using System.Text;

namespace RM.Terminal
{
    static class Cryptation
    {
        private static readonly string Salt = Convert.ToBase64String(Encoding.ASCII.GetBytes("#RacMon2017!#"));

        public static string GetSha1(string input, string username)
        {
            string userSalt = Convert.ToBase64String(Encoding.ASCII.GetBytes(username));
            input += Salt;
            input += userSalt;
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Crypting using MD5.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Hash(string input)
        {
            try
            {
                StringBuilder hash = new StringBuilder();
                MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
                foreach (byte t in md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input)))
                {
                    hash.Append(t.ToString("x2"));
                }
                return hash.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error creating course ID: {ex}");
                return "";
            }
        }
    }
}
