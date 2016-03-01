using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionComponent.String
{
    public static partial class StringExtension
    {
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
    }
}
