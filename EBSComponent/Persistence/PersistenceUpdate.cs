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
        /// <returns>返回受影响行数</returns>
        public int Update<T>(T entity, object conditionParam = null, object columnParam = null, bool isTransaction = false)
        {
            int ret = 0;
            if(isTransaction)
            {
                if (this.writeConnection.State == ConnectionState.Closed)
                {
                    this.writeConnection.Open();
                }
                using (IDbTransaction transaction = this.writeConnection.BeginTransaction())
                {
                    try
                    {
                        if(columnParam == null)
                        {
                            ret = this.writeConnection.Execute(sql.Update<T>(entity, conditionParam),entity, transaction);
                        }
                        else
                        {
                            ret = this.writeConnection.Execute(sql.Update<T>(columnParam, conditionParam),entity, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        ret = -1;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                if(columnParam == null)
                {
                    ret = this.writeConnection.Execute(sql.Update<T>(entity, conditionParam),entity);
                }
                else
                {
                    ret = this.writeConnection.Execute(sql.Update<T>(columnParam, conditionParam),entity);
                }
            }
            return ret;
        }

        /// <summary>
        /// 更新实体对象集合到数据库
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象.</param>
        /// <param name="isTransaction">是否为事务操作</param>
        /// <returns>返回受影响行数</returns>
        private int Update<T>(IEnumerable<T> entityCollection, object columnParam = null, object conditionParam = null, bool isTransaction = false)
        {
            int ret = 0;
            if (isTransaction)
            {
                if (this.writeConnection.State == ConnectionState.Closed)
                {
                    this.writeConnection.Open();
                }
                using (IDbTransaction transaction = this.writeConnection.BeginTransaction())
                {
                    try
                    {
                        foreach (var entity in entityCollection)
                        {
                            ret += this.writeConnection.Execute(sql.Update<T>(columnParam, conditionParam), entity, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        ret = -1;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                foreach (var entity in entityCollection)
                {
                    ret += this.writeConnection.Execute(sql.Update<T>(columnParam, conditionParam), entity);
                }
            }
            return ret;
        }
    }
}
