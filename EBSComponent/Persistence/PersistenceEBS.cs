using EbsComponent.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSComponent.Persistence
{
    public class PersistenceEBS
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionStr { get; set; }

        /// <summary>
        /// 用于自动生成SQL语句
        /// </summary>
        private static string sql = null;

        /// <summary>
        /// 使用的数据库枚举
        /// </summary>
        public DatabaseEnum database { get; set; }

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
                    _connection = this.CreateSqlConnection(this.database);
                }
                return _connection;
            }
        }

        public SqlConnection CreateSqlConnection(DatabaseEnum db = DatabaseEnum.测试库只读)
        {
            string databaseName = ((DatabaseEnum)db).ToString();
            this.connectionStr = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            return new SqlConnection(this.connectionStr);
        }

    }
}
