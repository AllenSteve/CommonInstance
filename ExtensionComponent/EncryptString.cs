using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;
using System.Web.Security;

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

        public static string CouponEncrypt(this string str, string param, string key)
        {
            string res = string.Empty;
            param += "&key=" + key;
            res = FormsAuthentication.HashPasswordForStoringInConfigFile(param, "md5").ToLowerInvariant();
            return res;
        }

        // 根据泛型表结构生成对应的SQL插入语句
        public static string CreateSQLInsertNewEntity<T>(this string str) where T : new() 
        {
            T tableName = new T();
            Type type = tableName.GetType();
            PropertyInfo[] properties = type.GetProperties();
            char[] appendArray = new char[properties.Length];
            for (int i = 0; i < properties.Length-1; ++i)
            {
                appendArray[i] = ',';
            }
            appendArray[appendArray.Length-1] = ')';
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

        // 生成全表查询SQL
        public static string CreateSQLQueryAll<T>(this string str) where T : new()
        {
            StringBuilder SQL = new StringBuilder("SELECT * FROM ");
            SQL.Append(new T().GetType().Name);
            return SQL.ToString();
        }

        // 生成根据主键Id查询的SQL
        public static string CreateSQLQueryById<T>(this string str) where T : new()
        {
            StringBuilder SQL = new StringBuilder("SELECT * FROM ");
            SQL.Append(new T().GetType().Name);
            SQL.Append(" WHERE ID=@ID");
            return SQL.ToString();
        }

        // 生成根据ID更新的SQL
        public static string CreateSQLUpdateById<T>(this string str) where T : new()
        {
            T table = new T();
            Type type = table.GetType();
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder SQL = new StringBuilder("UPDATE " + type.Name + " SET ");
            char[] appendArray = new char[properties.Length];
            for (int i = 0; i < properties.Length - 1; ++i)
            {
                appendArray[i] = ',';
            }
            appendArray[appendArray.Length - 1] = ' ';

            for (int i = 1; i < properties.Length; ++i)
            {
                SQL.Append(string.Format("{0}=@{0}{1}", properties[i].Name, appendArray[i]));
            }
            SQL.Append("WHERE ID=@ID");
            return SQL.ToString();
        }
    }
}
