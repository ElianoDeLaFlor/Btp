using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Btp.Models
{
    public static class Crypter
    {
        public static string Crypte(string str)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));

        }
    }
}