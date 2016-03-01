using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;
using System.Web.Security;
using System.Web;

namespace ExtensionComponent.String
{
    //string类型的扩展方法
    public static partial class StringExtension
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string MaskPhoneNo(this string str)
        {
            return string.Format("{0}****{1}", str.Substring(0, 3), str.Substring(7));
        }

        public static string Encrypt(this string str)
        {
            return str;
        }

        public static string Decrypt(this string str)
        {
            return str;
        }

        public static string CouponEncrypt(this string str, string param, string key)
        {
            string res = string.Empty; 
            param += "&key=" + key;
            res = FormsAuthentication.HashPasswordForStoringInConfigFile(param, "md5").ToLowerInvariant();
            return res;
        }

        public static string CreateSQLInsertNewEntityWithID<T>(this string str)
        {
            Type type = typeof(T);//tableName.GetType();
            PropertyInfo[] properties = type.GetProperties();
            char[] appendArray = new char[properties.Length];
            for (int i = 0; i < properties.Length - 1; ++i)
            {
                appendArray[i] = ',';
            }
            appendArray[appendArray.Length - 1] = ')';
            StringBuilder sql = new StringBuilder("INSERT INTO ");
            sql.Append(type.Name);
            sql.Append('(');
            for (int i = 0; i < properties.Length; ++i)
            {
                sql.Append(properties[i].Name);
                sql.Append(appendArray[i]);
            }

            sql.Append(" VALUES(");
            for (int i = 0; i < properties.Length; ++i)
            {
                sql.Append('@');
                sql.Append(properties[i].Name);
                sql.Append(appendArray[i]);
            }
            return sql.ToString();
        }

        // 根据泛型表结构生成对应的SQL插入语句
        public static string CreateSQLInsertNewEntity<T>(this string str)
        {
            Type type = typeof(T);//tableName.GetType();
            PropertyInfo[] properties = type.GetProperties();
            char[] appendArray = new char[properties.Length];
            for (int i = 0; i < properties.Length - 1; ++i)
            {
                appendArray[i] = ',';
            }
            appendArray[appendArray.Length - 1] = ')';
            StringBuilder sql = new StringBuilder("INSERT INTO ");
            sql.Append(type.Name);
            sql.Append('(');
            for (int i = 1; i < properties.Length; ++i)
            {
                sql.Append(properties[i].Name);
                sql.Append(appendArray[i]);
            }

            sql.Append(" VALUES(");
            for (int i = 1; i < properties.Length; ++i)
            {
                sql.Append('@');
                sql.Append(properties[i].Name);
                sql.Append(appendArray[i]);
            }
            return sql.ToString();
        }
    }
}
