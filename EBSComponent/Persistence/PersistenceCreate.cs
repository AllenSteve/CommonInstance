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
using System.Collections;

namespace EBSComponent.Persistence
{
    public partial class PersistenceEBS
    {
        /// <summary>
        /// 新增实体对象到数据库
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象.</param>
        /// <param name="isTransaction">是否为事务操作</param>
        /// <returns>返回结果</returns>
        public int Add<T>(T entity, bool isTransaction = false) where T : new()
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
                        ret = this.writeConnection.Execute(sql.Insert<T>(), entity, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ret = 0;
                    }
                }
            }
            else
            {
                try
                {
                    ret = this.writeConnection.Execute(sql.Insert<T>(), entity);
                }
                catch(Exception ex)
                {
                    return ret = 0;
                }
            }
            return ret;
        }

        /// <summary>
        /// 新增实体对象集合到数据库
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entityCollection">实体对象</param>
        /// <param name="isTransaction">是否为事务操作</param>
        /// <returns>返回结果</returns>
        public int AddRange<T>(IEnumerable<T> entityCollection, bool isTransaction = false) where T : new()
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
                        foreach(var entity in entityCollection)
                        {
                            ret += this.writeConnection.Execute(sql.Insert<T>(), entity, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ret = 0;
                    }
                }
            }
            else
            {
                try
                {
                    foreach (var entity in entityCollection)
                    {
                        ret += this.writeConnection.Execute(sql.Insert<T>(), entity);
                    }
                }
                catch (Exception ex)
                {
                    return ret;
                }
            }
            return ret;
        }
    }
}
