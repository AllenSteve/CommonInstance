using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ExtensionComponent
{
    //string类型的扩展方法
    public static class EncryptString
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string MaskPhoneNo(this string str)
        {
            return string.Format("{0}****{1}",str.Substring(0,3),str.Substring(7));
        }

        public static string Encrypt(this string str)
        {
            return str;
        }

        public static string Decrypt(this string str)
        {
            return str; 
        }
    }
}
