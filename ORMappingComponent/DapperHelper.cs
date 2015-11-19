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
        /// 数据查询--此处仅用于测试，还有不规范的地方
        /// </summary>
        /// <typeparam name="T">泛型类名</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="queryObject">查询对象</param>
        /// <returns>对象列表</returns>
        public static List<T> Query<T>(string querySQL, T queryObject)
        {
            using (connection)
            {
                return connection.Query<T>(querySQL, queryObject).ToList();
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">泛型类名</typeparam>
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
        /// 删除操作
        /// </summary>
        public static void Delete()
        {
 
        }

        /// <summary>
        /// 数据修改
        /// </summary>
        public static void Update()
        { 
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
