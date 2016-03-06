// ***********************************************************************
// Assembly         : EBS.Service.Bussiness.PaymentService
// Author           : 仇士龙
// Created          : 02-04-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-04-2016
// ***********************************************************************
// <copyright file="SQLStringProvider.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EBS.Interface.EContract.Method.EBSExtension
{
    /// <summary>
    /// 生成SQL语句的扩展类
    /// </summary>
    public static class SQLStringProvider
    {
        // 根据泛型表结构生成对应的SQL插入语句
        // 该方法适用于表结构中第一个字段名为ID的INT型主键自增的表
        public static string Insert<T>(this string str)
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
                if (!properties[i].PropertyType.Name.Equals("Byte[]") && !properties[i].Name.Equals("OID"))
                {
                    sql.Append(properties[i].Name);
                    sql.Append(appendArray[i]);
                }
            }

            sql.Append(" VALUES(");
            for (int i = 1; i < properties.Length; ++i)
            {
                if (!properties[i].PropertyType.Name.Equals("Byte[]") && !properties[i].Name.Equals("OID"))
                {
                    sql.Append('@');
                    sql.Append(properties[i].Name);
                    sql.Append(appendArray[i]);
                }
            }
            return sql.ToString();
        }

        // 生成全表查询SQL
        public static string QueryAll<T>(this string str)
        {
            StringBuilder SQL = new StringBuilder("SELECT * FROM ");
            SQL.Append(typeof(T).Name);
            return SQL.ToString();
        }

        public static string Query<T>(this string str, object conditionParam = null, object columnParam = null)
        {
            StringBuilder SQL = SQLStringProvider.CreateColumnParam(columnParam);
            SQL.Append(typeof(T).Name);
            SQL.Append(SQLStringProvider.CreateConditionParam(conditionParam));
            return SQL.ToString();
        }

        public static StringBuilder CreateColumnParam(object columnParam = null)
        {
            StringBuilder SQL = new StringBuilder("SELECT ");
            if(columnParam==null)
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
            if (conditionParam!=null)
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

        public static string QueryNodeList(this string str, string orderId)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" SELECT I.* FROM N_Payment_SchemeItem AS I  ");
            SQL.Append(" LEFT JOIN N_Payment_SchemeDetail AS D ON I.DetailId = D.ID ");
            SQL.Append(" LEFT JOIN N_Order_Operation AS O ON  D.SchemeId = O.PaymentSchemeId");
            SQL.Append(" WHERE O.OrderId=");
            SQL.Append("'");
            SQL.Append(orderId);
            SQL.Append("'");
            SQL.Append(" AND O.IsDel=0");
            SQL.Append(" ORDER BY OrderStateCode");
            return SQL.ToString();
        }

        public static string ParseString<T>(this string str,T model)
        {
            Type type = typeof(T);
            if (type.Name.Equals("String"))
            {
                return model.ToString();
            }
            else
            {
                PropertyInfo[] properties = type.GetProperties();
                StringBuilder retStr = new StringBuilder();
                foreach (var item in properties)
                {
                    retStr.Append(item.Name);
                    retStr.Append("\t=\t");
                    retStr.Append(item.GetValue(model, null));
                    retStr.Append("\r\n");
                }
                return retStr.ToString();
            }
        }

        public static string QueryNodeSortList(this string str, string orderId)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" SELECT I.* FROM ConstructAtaccepnce_InfoSortDetails AS I  ");
            SQL.Append(" LEFT JOIN ConstructAtaccepnce_InfoSort AS D ON I.InfoSortID = D.ID ");
            SQL.Append(" LEFT JOIN N_Order_Operation AS O ON  D.ID = O.CheckSortID");
            SQL.Append(" WHERE O.OrderId=");
            SQL.Append("'");
            SQL.Append(orderId);
            SQL.Append("'");
            SQL.Append(" AND O.IsDel=0");
            SQL.Append(" ORDER BY I.Sort");
            return SQL.ToString();
        }

        // 生成根据指定列和条件的更新SQL
        public static string Update<T>(this string str, object columnParam = null, object conditionParam = null)
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
                if (columnProperties[i].Name.ToUpper().Equals("ID") || columnProperties[i].Name.ToUpper().Equals("OID") || columnProperties[i].PropertyType.Equals(typeof(Byte[])))
                {
                    continue;
                }

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

        public static string Delete<T>(this string str, T entity, object conditionParam = null)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder SQL = new StringBuilder();

            if (entity != null)
            {
                SQL.Append(" DELETE FROM ");
                SQL.Append(type.Name);
                SQL.Append(" WHERE ");
                SQL.Append(properties.FirstOrDefault().Name);
                SQL.Append(" = ");
                SQL.Append(properties.FirstOrDefault().GetValue(entity, null));
            }
            else if (conditionParam != null)
            {
                SQL.Append(" DELETE FROM ");
                SQL.Append(type.Name);
                SQL.Append(SQLStringProvider.CreateConditionParam(conditionParam));
            }
            return SQL.ToString();
        }

        public static string Remove<T>(this string str, T entity, object conditionParam = null)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder SQL = new StringBuilder();

            if (entity != null)
            {
                SQL.Append(" UPDATE ");
                SQL.Append(type.Name);
                SQL.Append(" SET ISDEL=0 ");
                SQL.Append(" WHERE ");
                SQL.Append(properties.FirstOrDefault().Name);
                SQL.Append(" = ");
                SQL.Append(properties.FirstOrDefault().GetValue(entity, null));
            }
            else if (conditionParam != null)
            {
                SQL.Append(" UPDATE ");
                SQL.Append(type.Name);
                SQL.Append(" SET ISDEL=0 ");
                SQL.Append(SQLStringProvider.CreateConditionParam(conditionParam));
            }
            return SQL.ToString();
        }
    }
}
