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
        /// 更新实体对象到数据库
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象.</param>
        /// <param name="isTransaction">是否为事务操作</param>
        /// <returns>返回结果</returns>
        public int Update<T>(T entity, bool isTransaction = false)
        {
            return this.writeConnection.Execute(sql.Update<T>(), entity);
        }
    }
}
