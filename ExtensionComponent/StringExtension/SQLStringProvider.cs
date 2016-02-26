using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;
using System.Web.Security;
using System.Web;

namespace ExtensionComponent
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

        // 生成全表查询SQL
        public static string CreateSQLQueryAll<T>(this string str)
        {
            StringBuilder SQL = new StringBuilder("SELECT * FROM ");
            SQL.Append(typeof(T).Name);
            return SQL.ToString();
        }

        // 生成根据主键Id查询的SQL
        public static string CreateSQLQueryById<T>(this string str)
        {
            StringBuilder SQL = new StringBuilder("SELECT * FROM ");
            SQL.Append(typeof(T).Name);
            SQL.Append(" WHERE ID=@ID");
            return SQL.ToString();
        }

        // 生成根据ID更新的SQL
        public static string CreateSQLUpdateById<T>(this string str)
        {
            Type type = typeof(T);
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

        // 生成根据指定列和条件的更新SQL
        public static string CreateSQLUpdateByProperties<T>(this string str, object columnParam, object conditionParam = null)
        {
            // 表类型
            Type tableType = typeof(T);
            // 列对象类型
            Type columnType = columnParam.GetType();
            PropertyInfo[] columnProperties = columnType.GetProperties();

            StringBuilder SQL = new StringBuilder("UPDATE " + tableType.Name + " SET ");
            object value = null;
            char[] appendArray = new char[columnProperties.Length];
            for (int i = 0; i < columnProperties.Length - 1; ++i)
            {
                appendArray[i] = ',';
            }
            appendArray[appendArray.Length - 1] = ' ';

            for (int i = 0; i < columnProperties.Length; ++i)
            {
                value = columnProperties[i].GetValue(columnParam, null);
                if (value == null)
                {
                    //Console.WriteLine("{0}:Null", columnProperties[i].Name);
                    SQL.Append(columnProperties[i].Name);
                    SQL.Append("=NULL");
                    SQL.Append(appendArray[i]);
                }
                else if (value.GetType().Name.Equals("String") || value.GetType().Name.Equals("DateTime"))
                {
                    SQL.Append(columnProperties[i].Name);
                    SQL.Append("='");
                    SQL.Append(value);
                    SQL.Append("'");
                    SQL.Append(appendArray[i]);
                    //Console.WriteLine("{0}:'{1}'", columnProperties[i].Name, value);
                }
                else
                {
                    SQL.Append(columnProperties[i].Name);
                    SQL.Append("=");
                    SQL.Append(value);
                    SQL.Append(appendArray[i]);
                }
            }

            if (conditionParam != null)
            {
                // 条件对象类型
                Type conditionType = conditionParam.GetType();
                PropertyInfo[] conditionProperties = conditionType.GetProperties();
                string[] conditionAppendArray = new string[conditionProperties.Length];
                for (int i = 0; i < conditionProperties.Length - 1; ++i)
                {
                    conditionAppendArray[i] = " AND ";
                }
                SQL.Append("WHERE ");
                for (int i = 0; i < conditionProperties.Length; ++i)
                {
                    value = conditionProperties[i].GetValue(conditionParam, null);
                    if (value == null)
                    {
                        //Console.WriteLine("{0}:Null", columnProperties[i].Name);
                        SQL.Append(conditionProperties[i].Name);
                        SQL.Append(" IS NULL");
                        SQL.Append(conditionAppendArray[i]);
                    }
                    else if (value.GetType().Name.Equals("String") || value.GetType().Name.Equals("DateTime"))
                    {
                        SQL.Append(conditionProperties[i].Name);
                        SQL.Append("='");
                        SQL.Append(value);
                        SQL.Append("'");
                        SQL.Append(conditionAppendArray[i]);
                        //Console.WriteLine("{0}:'{1}'", columnProperties[i].Name, value);
                    }
                    else
                    {
                        SQL.Append(conditionProperties[i].Name);
                        SQL.Append("=");
                        SQL.Append(value);
                        SQL.Append(conditionAppendArray[i]);
                    }
                }
            }
            return SQL.ToString();
        }

        // 生成根据指定列和条件的查询SQL
        public static string Query<T>(this string str, object columnParam = null, object conditionParam = null)
        {
            StringBuilder SQL = CreateColumnParam(columnParam);
            SQL.Append(typeof(T).Name);
            SQL.Append(CreateConditionParam(conditionParam));
            return SQL.ToString();
        }

        public static StringBuilder CreateColumnParam(object columnParam = null)
        {
            StringBuilder SQL = new StringBuilder("SELECT ");
            if (columnParam == null)
            {
                SQL.Append("*");
            }
            else
            {
                Type columnType = columnParam.GetType();
                PropertyInfo[] columnProperties = columnType.GetProperties();
                char[] appendArray = new char[columnProperties.Length];
                for (int i = 0; i < columnProperties.Length - 1; ++i)
                {
                    appendArray[i] = ',';
                }
                appendArray[appendArray.Length - 1] = ' ';

                for (int i = 0; i < columnProperties.Length; ++i)
                {
                    SQL.Append(columnProperties[i].Name);
                    SQL.Append(appendArray[i]);
                }
            }
            SQL.Append(" FROM ");
            return SQL;
        }

        public static string CreateConditionParam(object conditionParam = null)
        {
            StringBuilder SQL = new StringBuilder();
            if (conditionParam != null)
            {
                SQL.Append(" WHERE ");
                object value = null;
                Type conditionType = conditionParam.GetType();
                PropertyInfo[] conditionProperties = conditionType.GetProperties();
                string[] conditionAppendArray = new string[conditionProperties.Length];
                for (int i = 0; i < conditionProperties.Length - 1; ++i)
                {
                    conditionAppendArray[i] = " AND ";
                }
                for (int i = 0; i < conditionProperties.Length; ++i)
                {
                    value = conditionProperties[i].GetValue(conditionParam, null);
                    if (value == null)
                    {
                        //Console.WriteLine("{0}:Null", columnProperties[i].Name);
                        SQL.Append(conditionProperties[i].Name);
                        SQL.Append(" IS NULL");
                        SQL.Append(conditionAppendArray[i]);
                    }
                    else if (value.GetType().Name.Equals("String") || value.GetType().Name.Equals("DateTime"))
                    {
                        SQL.Append(conditionProperties[i].Name);
                        SQL.Append("='");
                        SQL.Append(value);
                        SQL.Append("'");
                        SQL.Append(conditionAppendArray[i]);
                        //Console.WriteLine("{0}:'{1}'", columnProperties[i].Name, value);
                    }
                    else
                    {
                        SQL.Append(conditionProperties[i].Name);
                        SQL.Append("=");
                        SQL.Append(value);
                        SQL.Append(conditionAppendArray[i]);
                    }
                }
            }
            return SQL.ToString();
        }

        public static string ConvertEncoding(this string str,string srcEncoding="GB2312",string destEncoding="UTF-8")
        {
            // 源格式编码
            Encoding SRC = Encoding.GetEncoding(srcEncoding);
            // 源格式编码
            byte[] buffer = SRC.GetBytes(str);

            // 目标格式编码
            Encoding DEST = Encoding.GetEncoding(destEncoding);
            // 转换为 目标格式编码
            byte[] buffer2 = Encoding.Convert(SRC, DEST, buffer, 0, buffer.Length);

            return SRC.GetString(buffer2, 0, buffer2.Length);
        }

        public static string ConvertToUrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }
    }
}
