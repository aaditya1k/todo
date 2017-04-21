using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;  // Unicode Encoding
using System.Security.Cryptography; // SHA512 Managed

namespace Todo.Library
{
    public class HelperLibrary
    {
        public static string Hash_Password(string input_string, string salt)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytPassword = uEncode.GetBytes(input_string + salt);
            SHA512Managed sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(bytPassword);
            return Convert.ToBase64String(hash);
        }
    }
}