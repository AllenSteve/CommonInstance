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
    }
}
