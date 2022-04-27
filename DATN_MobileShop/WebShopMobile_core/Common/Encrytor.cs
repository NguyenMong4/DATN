using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebShopMobile_core.Common
{
    public class Encrytor
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            
                MD5 md5 = new MD5CryptoServiceProvider();

                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input));

                byte[] result = md5.Hash;
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    stringBuilder.Append(result[i].ToString("x2"));

                }
                return stringBuilder.ToString();
            
        }
    }
}
