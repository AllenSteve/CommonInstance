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

        private static IDbConnection connection = OpenSqlConnection(connStr);

        /// <summary>
        /// 数据新增
        /// </summary>
        public static int Add<T>(string insertStr, T insertObj)
        {
            using (connection)
            {
                //UserInfo user = new UserInfo() { Code = "abcd", Name = "Tom", Description = "Test User" };
                //string query = "insert into UserInfo(Code,Name,Description) values (@Code,@Name,@Description)";
                //int row = connection.Execute(query, user);
                return connection.Execute(insertStr, insertObj);
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
        /// 数据查询
        /// </summary>
        public static List<T> Query<T>(string queryStr, T queryObj) 
        {
            using (connection)
            {
                //string query = "SELECT *  FROM UserInfo WHERE Id=@Id";
                //List<UserInfo> list = conn.Query<UserInfo>(query, new { Id =1}).ToList();
                //string query = "SELECT *  FROM UserInfo WHERE code=@Code";
                //return connection.Query<T>(query, new { code = "abcd" });

                return connection.Query<T>(queryStr, queryObj).ToList();
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
