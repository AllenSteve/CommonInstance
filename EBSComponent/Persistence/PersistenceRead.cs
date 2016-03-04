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

namespace EBSComponent.Persistence
{
    public partial class PersistenceEBS
    {
        /// <summary>
        /// 根据参数列表和结果列查询数据表
        /// </summary>
        /// <typeparam name="T">数据表实体类型</typeparam>
        /// <param name="conditionParam">条件参数</param>
        /// <param name="columnParam">列参数</param>
        /// <returns>结果集</returns>
        public IEnumerable<T> Query<T>(object conditionParam = null, object columnParam = null)
        {
            return this.readConnection.Query<T>(sql.Query<T>(conditionParam, columnParam));
        }

        public IQueryable<T> AsQueryable<T>(object conditionParam = null, object columnParam = null)
        {
            return this.readConnection.Query<T>(sql.Query<T>(conditionParam, columnParam)).AsQueryable();
        }
    }
}
