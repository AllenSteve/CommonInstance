using ORMappingComponent.TableClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ExtensionComponent;

namespace ORMappingComponent
{
    public class DapperHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string connStr = @"Data Source=.\localdb;Initial Catalog=workDB;UID=sa;PWD=123456;";

        /// <summary>
        /// 用于自动生成SQL语句
        /// </summary>
        private static string sql = null;

        /// <summary>
        /// 打开到数据库的连接
        /// </summary>
        private static SqlConnection _connection;

        public static SqlConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = OpenSqlConnection(connStr);
                }
                return _connection;
            }
        }

        /// <summary>
        /// 数据查询--通过参数列表查询--可以是关联查询的返回结果
        /// </summary>
        /// <typeparam name="object">对象类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>查询列表</returns>
        public static IEnumerable<object> Query(string querySQL, object paramArray = null)
        {
            using (connection)
            {
                return connection.Query<object>(querySQL, paramArray);
            }
        }

        /// <summary>
        /// 数据查询--通过参数列表查询--最常用的查询功能
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>查询列表</returns>
        public static IEnumerable<T> Query<T>(string querySQL, object paramArray = null)
        {
            using (connection)
            {
                return connection.Query<T>(querySQL, paramArray);
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
        public static IEnumerable<T> Query<T>(string querySQL, T entity)
        {
            using (connection)
            {
                return connection.Query<T>(querySQL, entity);
            }
        }

        /// <summary>
        /// 执行组合型SQL语句，用于数据的增删改操作
        /// </summary>
        /// <param name="execSQL">插入SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>返回受到Insert,Update 和 Delete 操作影响的行数，所有其他查询都返回 –1，存储过程中如果含有set nocount on也会导致返回值为-1</returns>
        public static int ExecuteSQL(string execSQL, object paramArray)
        {
            using (connection)
            {
                return connection.Execute(execSQL, paramArray);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="insertSQL">插入SQL</param>
        /// <param name="entity">要插入的数据对象类型与T保持一致</param>
        /// <returns>返回受到Insert操作影响的行数，所有其他查询都返回 –1</returns>
        public static int Add<T>(string insertSQL, T entity)
        {
            using (connection)
            {
                return connection.Execute(insertSQL, entity);
            }
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="entity">要插入的对象实体</param>
        /// <returns>返回插入记录数</returns>
        public static int Add<T>(T entity) where T : new()
        {
            using (connection)
            {
                return connection.Execute(sql.CreateInsertSQL<T>(), entity);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="deleteSQL">删除SQL</param>
        /// <param name="delObject">删除对象</param>
        /// <returns>返回收到Delete影响的行数</returns>
        public static int Delete<T>(string deleteSQL, T entity)
        {
            using (connection)
            {
                return connection.Execute(deleteSQL, entity);
            }
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="updateSQL">更新SQL</param>
        /// <param name="updateObject">更新对象</param>
        /// <returns>返回收到Update影响的行数</returns>
        public static int Update<T>(string updateSQL, T entity)
        {
            using (connection)
            {
                return connection.Execute(updateSQL, entity);
            }
        }

        /// <summary>
        /// 打开数据库连接字符串
        /// </summary>
        /// <param name="sqlConnectionString">数据库连接字符串，默认为null</param>
        /// <returns>返回数据库连接对象</returns>
        public static SqlConnection OpenSqlConnection(string sqlConnectionString = null)
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            return conn;
        }
    }
}
