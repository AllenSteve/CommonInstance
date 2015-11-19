using ORMappingComponent.TableClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ORMappingComponent
{
    public static class DapperHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string connStr = @"Data Source=.\localdb;Initial Catalog=workDB;UID=sa;PWD=123456;";

        /// <summary>
        /// 打开到数据库的连接
        /// </summary>
        private static IDbConnection connection = OpenSqlConnection(connStr);

        /// <summary>
        /// 数据查询--通过参数列表查询--最常用的查询功能
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>查询列表</returns>
        public static IEnumerable<T> Query<T>(string querySQL, object paramArray)
        {
            using (connection)
            {
                return connection.Query<T>(querySQL, paramArray);
            }
        }

        /// <summary>
        /// 数据查询--通过主键匹配查询单表中的单个记录--这个方法有点废柴
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="K">主键数据类型</typeparam>
        /// <param name="querySQL">查询字符串</param>
        /// <param name="primaryKey">主键内容</param>
        /// <returns>查询结果对象或NULL</returns>
        public static T Query<T,K>(string querySQL, K primaryKey)
        {
            using (connection)
            {
                return connection.Query<T>(querySQL, primaryKey).FirstOrDefault();
            }
        }

        /// <summary>
        /// 数据查询--此处仅用于测试，还有不规范的地方（此方法是通过对象直接查找，类似于拼接参数数组）
        /// 注意在使用时引入System.Linq，可对结果集合使用ToList()方法将其转化为List
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="queryObject">查询对象</param>
        /// <returns>对象列表</returns>
        public static IEnumerable<T> Query<T>(string querySQL, T queryObject)
        {
            using (connection)
            {
                return connection.Query<T>(querySQL, queryObject);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="insertSQL">插入SQL</param>
        /// <param name="insertObject">要插入的数据对象类型与T保持一致</param>
        /// <returns>返回受到Insert,Update 和 Delete 操作影响的行数，所有其他查询都返回 –1，存储过程中如果含有set nocount on也会导致返回值为-1</returns>
        public static int Add<T>(string insertSQL, T insertObject)
        {
            using (connection)
            {
                return connection.Execute(insertSQL, insertObject);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="delSQL">删除SQL</param>
        /// <param name="delObject">删除对象</param>
        /// <returns>返回收到Delete影响的行数</returns>
        public static int Delete<T>(string delSQL, T delObject)
        {
            using (connection)
            {
                return connection.Execute(delSQL, delObject);
            }
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="updateSQL">更新SQL</param>
        /// <param name="updateObject">更新对象</param>
        /// <returns>返回收到Update影响的行数</returns>
        public static int Update<T>(string updateSQL, T updateObject)
        {
            using (connection)
            {
                return connection.Execute(updateSQL, updateObject);
            }
        }

        /// <summary>
        /// 打开数据库连接字符串
        /// </summary>
        /// <param name="sqlConnectionString"></param>
        /// <returns></returns>
        private static SqlConnection OpenSqlConnection(string sqlConnectionString=null)
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        } 

    }
}
