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
using System.Configuration;
using System.Reflection;

namespace ORMappingComponent
{
    public class DBHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// 
        //private static readonly string connStr = @"Data Source=LACRIMA\LACRIMA;Initial Catalog=CompanyDB;UID=sa;PWD=123456;";
        private static readonly string connStr = @"Data Source=192.168.127.154\localdb;Initial Catalog=workDB;UID=sa;PWD=123456;";
        //private static readonly string connStr = "Data Source=123.103.35.138;Initial Catalog=jjjy_test1107;UID=jjjy_test_admin;PWD=3791f38D;";

        /// <summary>
        /// 用于自动生成SQL语句
        /// </summary>
        private static string sql = null;

        public Sqldb dbType { get; set; }

        /// <summary>
        /// 打开到数据库的连接
        /// </summary>
        private SqlConnection _connection;

        public SqlConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = CreateConnection(this.dbType);
                }
                return _connection;
            }
        }

        public DBHelper(int type = 0)
        {
            if (type == 0)
            {
                _connection = OpenSqlConnection(connStr);
            }
            else if (this.ContainsConnectionType(type))
            {
                _connection = CreateConnection((Sqldb)type);
            }
            else
            {
                _connection = null;
            }
        }

        public void SwitchDB(Sqldb db)
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
            this.dbType = db;
        }

        public static SqlConnection CreateConnection(Sqldb db = Sqldb.OrderReadOnly)
        {
            string dbName = ((Sqldb)db).ToString();
            string sqlConnectionStr = ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
            //string sqlConnectionStr = ConfigurationManager.AppSettings[dbName];
            return new SqlConnection(sqlConnectionStr);
        }

        public enum Sqldb
        {
            /// <summary>
            /// 用户只读库
            /// </summary>
            UserReadOnly = 1,
            /// <summary>
            /// 用户写库
            /// </summary>
            UserWrite = 2,
            /// <summary>
            /// 订单只读库
            /// </summary>
            OrderReadOnly = 3,
            /// <summary>
            /// 订单写库
            /// </summary>
            OrderWrite = 4,
            /// <summary>
            /// 服务中心只读库
            /// </summary>
            ServiceCenterReadOnly = 5,
            /// <summary>
            /// 服务中心写库
            /// </summary>
            ServiceCenterWrite = 6
        }

        /// <summary>
        /// 查询全表
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <returns>全表数据</returns>
        public IEnumerable<T> QueryAll<T>()
        {
            return connection.Query<T>(sql.CreateSQLQueryAll<T>());
        }

        /// <summary>
        /// 数据查询--通过参数列表查询--可以是关联查询的返回结果
        /// </summary>
        /// <typeparam name="object">对象类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>查询列表</returns>
        public IEnumerable<object> Query(string querySQL, object paramArray = null)
        {
            return connection.Query(querySQL, paramArray);
        }

        /// <summary>
        /// 数据查询--通过参数列表查询--最常用的查询功能
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>查询列表</returns>
        public IEnumerable<T> Query<T>(string querySQL, object paramArray = null)
        {
            return connection.Query<T>(querySQL, paramArray);
        }

        /// <summary>
        /// 数据查询--此处仅用于测试，还有不规范的地方（此方法是通过对象直接查找，类似于拼接参数数组）
        /// 注意在使用时引入System.Linq，可对结果集合使用ToList()方法将其转化为List
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="queryObject">查询对象</param>
        /// <returns>对象列表</returns>
        public IEnumerable<T> Query<T>(string querySQL, T entity)
        {
            return connection.Query<T>(querySQL, entity);
        }

        /// <summary>
        /// 数据查询--根据数据的主键ID信息查询单条记录
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="querySQL">查询SQL</param>
        /// <param name="id">查询id</param>
        /// <returns>对象</returns>
        public T Query<T>(int id)
        {
            return connection.Query<T>(sql.CreateSQLQueryById<T>(), new { ID = id }).FirstOrDefault();
        }

        /// <summary>
        /// 执行组合型SQL语句，用于数据的增删改操作
        /// </summary>
        /// <param name="execSQL">插入SQL</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>返回受到Insert,Update 和 Delete 操作影响的行数，所有其他查询都返回 –1，存储过程中如果含有set nocount on也会导致返回值为-1</returns>
        public int ExecuteSQL(string execSQL, object paramArray)
        {
            return connection.Execute(execSQL, paramArray);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="insertSQL">插入SQL</param>
        /// <param name="entity">要插入的数据对象类型与T保持一致</param>
        /// <returns>返回受到Insert操作影响的行数，所有其他查询都返回 –1</returns>
        public int Add<T>(string insertSQL, T entity)
        {
            return connection.Execute(insertSQL, entity);
        }

        /// <summary>
        /// 新增记录-注意数据表类中第一个字段必须为自动增长型的ID
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="entity">要插入的对象实体</param>
        /// <returns>返回插入记录数</returns>
        public int Add<T>(T entity)
        {
            return connection.Execute(sql.CreateSQLInsertNewEntity<T>(), entity);
        }

        public int AddWithID<T>(T entity)
        {
            return connection.Execute(sql.CreateSQLInsertNewEntityWithID<T>(), entity);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="deleteSQL">删除SQL</param>
        /// <param name="delObject">删除对象</param>
        /// <returns>返回收到Delete影响的行数</returns>
        public int Delete<T>(string deleteSQL, T entity)
        {
            return connection.Execute(deleteSQL, entity);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="updateSQL">更新SQL</param>
        /// <param name="updateObject">更新对象</param>
        /// <returns>返回收到Update影响的行数</returns>
        public int Update<T>(string updateSQL, T entity)
        {
            return connection.Execute(updateSQL, entity);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="updateSQL">更新SQL</param>
        /// <param name="updateObject">更新对象</param>
        /// <returns>返回收到Update影响的行数</returns>
        public int Update<T>(T entity)
        {
            return connection.Execute(sql.CreateSQLUpdateById<T>(), entity);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="columnParam">属性列表</param>
        /// <param name="conditionParam">条件列表</param>
        /// <returns>更新行数</returns>
        public int Update<T>(object columnParam, object conditionParam)
        {
            return connection.Execute(sql.CreateSQLUpdateByProperties<T>(columnParam, conditionParam));
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


        private bool ContainsConnectionType(int dbType)
        {
            var array = Enum.GetValues(typeof(Sqldb));
            for (int i = 0; i < array.Length; ++i)
            {
                if (dbType == (int)array.GetValue(i))
                {
                    return true;
                }
            }
            return false;
        }
    }
    public struct SqlConnectionString
    {
        public string UserReadOnly
        {
            get { return ""; }
        }
        public string UserWrite
        {
            get { return ""; }
        }
        public string OrderReadOnly
        {
            get { return ""; }
        }
        public string OrderWrite
        {
            get { return ""; }
        }
        public string ServiceCenterReadOnly
        {
            get { return ""; }
        }
        public string ServiceCenterWrite
        {
            get { return ""; }
        }
    }
}
