// ***********************************************************************
// Assembly         : ORMappingComponent
// Author           : Dragonet
// Created          : 12-18-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-18-2015
// ***********************************************************************
// <copyright file="QueryGenerator.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 查询生成器
/// http://www.cnblogs.com/yaozhenfa/p/iqueryable_and_iqueryprovider.html
/// </summary>
namespace ORMappingComponent.SQLHelper
{
    public class QueryProvider : IQueryProvider
    {
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Query<TElement>(this,expression);
        }

        /// <summary>
        /// 构造一个 <see cref="T:System.Linq.IQueryable" /> 对象，该对象可计算指定表达式目录树所表示的查询。
        /// http://www.cnblogs.com/yaozhenfa/p/iqueryable_and_iqueryprovider.html
        /// </summary>
        /// <param name="expression">表示 LINQ 查询的表达式目录树。</param>
        /// <returns>一个 <see cref="T:System.Linq.IQueryable" />，它可计算指定表达式目录树所表示的查询。</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = expression.Type;
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(Query<>).MakeGenericType(elementType),new object[]{this,expression});
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行指定表达式目录树所表示的查询。
        /// </summary>
        /// <param name="expression">表示 LINQ 查询的表达式目录树。</param>
        /// <returns>执行指定查询所生成的值。</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
