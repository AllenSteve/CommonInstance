using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EBS.Interface.EContract.Method.EBSExtension;
using System.Data;
using System.Collections;

namespace EBSComponent.Persistence
{
    public partial class PersistenceEBS
    {
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="conditionParam">条件参数</param>
        /// <returns>受影响的行数</returns>
        public int Delete<T>(T entity, object conditionParam = null)
        {
            return this.writeConnection.Execute(sql.Delete<T>(conditionParam), entity);
        }

        /// <summary>
        /// 设置对象的删除标记
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="conditionParam">条件参数</param>
        /// <param name="columnParam">删除标记列参数</param>
        /// <returns>受影响的行数</returns>
        public int Remove<T>(T entity, object conditionParam = null, object columnParam = null)
        {
            return this.writeConnection.Execute(sql.Remove<T>(conditionParam, columnParam), entity);
        }
    }
}
