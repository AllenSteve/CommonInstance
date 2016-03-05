using EBSComponent.Enums;
using EBSComponent.Model.EntityType;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EBSComponent.Interface
{
    public class Persistence<T> where T : EntityBase
    {
        private MethodInfo _method { get; set; }

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
               
        private MethodInfo method
        {
            get
            {
                if(this._method == null)
                {
                    this._method = typeof(T).GetMethod("GetEntityType");
                }
                return this._method;
            }
        }

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
        protected SqlConnection CreateSqlConnection(EntityTypeEnum database)
        {
            string databaseName = ((EntityTypeEnum)database).ToString();
            string connectionStr = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            return new SqlConnection(connectionStr);
        }

        public Persistence()
        {
            T instance = Activator.CreateInstance<T>();
            this.read_database = (EntityTypeEnum)this.method.Invoke(instance, new object[] { AccessModeEnum.READ });
            this.write_database = (EntityTypeEnum)this.method.Invoke(instance, new object[] { AccessModeEnum.WRITE }); 
        }
        
    }
}
