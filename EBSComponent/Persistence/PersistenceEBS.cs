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

        protected SqlConnection CreateSqlConnection(DatabaseEnum database = DatabaseEnum.测试库只读)
        {
            string databaseName = ((DatabaseEnum)database).ToString();
            string connectionStr = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            return new SqlConnection(connectionStr);
        }

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

        protected bool IsDebugMode()
        {
            return false;
        }

        public PersistenceEBS(int readDatabase = 1, int writeDatabase = 2, bool isDebug = false)
        {
            if (isDebug || this.IsDebugMode())
            {
                this.read_database = DatabaseEnum.沙箱库只读;
                this.write_database = DatabaseEnum.沙箱库读写;
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
