using EbsComponent.Enums;
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
        protected DatabaseEnum read_database { get; set; }

        /// <summary>
        /// 使用的数据库枚举-读写
        /// </summary>
        protected DatabaseEnum write_database { get; set; }

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
        protected SqlConnection CreateSqlConnection(DatabaseEnum database = DatabaseEnum.EBS_READ)
        {
            string databaseName = ((DatabaseEnum)database).ToString();
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
            Array array = System.Enum.GetValues(typeof(DatabaseEnum));
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
        /// 判断当前是否为调试模式
        /// </summary>
        /// <returns>当前模式</returns>
        protected bool IsDebugMode()
        {
            return false;
        }

        public PersistenceEBS(int readDatabase = 1, int writeDatabase = 2, bool isDebug = false)
        {
            if (isDebug || this.IsDebugMode())
            {
                this.read_database = DatabaseEnum.EBS_READ_TEST;
                this.write_database = DatabaseEnum.EBS_WRITE_TEST;
            }
            else
            {
                if (this.ContainsDatabase(readDatabase))
                {
                    this.read_database = (DatabaseEnum)readDatabase;
                    this.read_connection = this.CreateSqlConnection(this.read_database);
                }
                else
                {
                    this.read_connection = null;
                }

                if (this.ContainsDatabase(writeDatabase))
                {
                    this.write_database = (DatabaseEnum)writeDatabase;
                    this.write_connection = this.CreateSqlConnection(this.write_database);
                }
                else
                {
                    this.write_connection = null;
                }
            }
        }
    }
}
