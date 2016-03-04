using EBSComponent.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EBS.Interface.EContract.Method.EBSExtension;
using System.Data;
using System.Web;

namespace EBSComponent.Persistence
{
    public partial class PersistenceEBS
    {
        /// <summary>
        /// 用于自动生成SQL语句
        /// </summary>
        private static string sql = null;

        /// <summary>
        /// 打开到数据库的连接-只读
        /// </summary>
        private SqlConnection read_connection;

        /// <summary>
        /// 打开到数据库的连接-读写
        /// </summary>
        private SqlConnection write_connection;

        /// <summary>
        /// 使用的数据库枚举-只读
        /// </summary>
        protected EntityTypeEnum read_database { get; set; }

        /// <summary>
        /// 使用的数据库枚举-读写
        /// </summary>
        protected EntityTypeEnum write_database { get; set; }

        /// <summary>
        /// 读操作连接
        /// </summary>
        protected SqlConnection readConnection
        {
            get
            {
                if (read_connection == null)
                {
                    read_connection = this.CreateSqlConnection(this.read_database);
                }
                return read_connection;
            }
        }

        /// <summary>
        /// 写操作连接
        /// </summary>
        protected SqlConnection writeConnection
        {
            get
            {
                if (write_connection == null)
                {
                    write_connection = this.CreateSqlConnection(this.write_database);
                }
                return write_connection;
            }
        }

        /// <summary>
        /// 生成数据库连接
        /// </summary>
        /// <param name="database">数据库枚举</param>
        /// <returns>对应数据库连接</returns>
        protected SqlConnection CreateSqlConnection(EntityTypeEnum database = EntityTypeEnum.EBS_READ)
        {
            string databaseName = ((EntityTypeEnum)database).ToString();
            string connectionStr = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            return new SqlConnection(connectionStr);
        }

        /// <summary>
        /// 判断数据库枚举是否存在
        /// </summary>
        /// <param name="enmuValue">枚举值</param>
        /// <returns>枚举中是否存在该数据库</returns>
        protected  bool ContainsDatabase(int enmuValue)
        {
            Array array = System.Enum.GetValues(typeof(EntityTypeEnum));
            for (int i = 0; i < array.Length; ++i)
            {
                if (enmuValue == (int)array.GetValue(i))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 解析HTTP上下文
        /// </summary>
        /// <param name="context">HTTP上下文对象</param>
        /// <returns>解析字符串</returns>
        protected string ParseHttpContext(HttpContext context, string key)
        {
            if (context == null || context.Items[key] == null)
            {
                return string.Empty;
            }
            else
            {
                return context.Items[key].ToString();
            }
        }

        /// <summary>
        /// 判断当前是否为调试模式
        /// </summary>
        /// <returns>当前模式</returns>
        protected bool IsDebugMode()
        {
            return this.ParseHttpContext(HttpContext.Current, "IsTestUser").Equals("1");
        }

        public PersistenceEBS(int readDatabase = 1, int writeDatabase = 2, bool isDebug = false)
        {
            if (isDebug || this.IsDebugMode())
            {
                this.read_database = EntityTypeEnum.EBS_READ_TEST;
                this.write_database = EntityTypeEnum.EBS_WRITE_TEST;
            }
            else
            {
                if (this.ContainsDatabase(readDatabase))
                {
                    this.read_database = (EntityTypeEnum)readDatabase;
                }

                if (this.ContainsDatabase(writeDatabase))
                {
                    this.write_database = (EntityTypeEnum)writeDatabase;
                }
            }
        }
    }
}
