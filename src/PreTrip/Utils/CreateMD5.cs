using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PreTrip.Utils
{
    public class CreateMD5
    {
        private CreateMD5()
        {

        }

        public static string GetHash(string value)
        {
            var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(inputBytes);
            
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}