// ***********************************************************************
// Assembly         : ORMappingComponent
// Author           : Dragonet
// Created          : 12-18-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-18-2015
// ***********************************************************************
// <copyright file="Query.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComponentORM.SQLHelper
{

    /// <summary>
    /// Class Query.
    /// http://www.cnblogs.com/yaozhenfa/p/iqueryable_and_iqueryprovider.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Query<T>: IQueryable<T>
    {
        private QueryProvider provider;

        private Expression expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Query{T}"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <exception cref="System.ArgumentNullException">provider</exception>
        public Query(QueryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            this.provider = provider;
            this.expression = Expression.Constant(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Query{T}"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="expression">The expression.</param>
        public Query(QueryProvider provider,Expression expression)
        {
            this.provider = provider;
            this.expression = expression;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator{`0}.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.provider.Execute(this.expression)).GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>一个可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.provider.Execute(this.expression)).GetEnumerator();
        }

        /// <summary>
        /// 获取在执行与 <see cref="T:System.Linq.IQueryable" /> 的此实例关联的表达式目录树时返回的元素的类型。
        /// </summary>
        /// <value>The type of the element.</value>
        /// <returns>一个 <see cref="T:System.Type" />，表示在执行与之关联的表达式目录树时返回的元素的类型。</returns>
        public Type ElementType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 获取与 <see cref="T:System.Linq.IQueryable" /> 的实例关联的表达式目录树。
        /// </summary>
        /// <value>The expression.</value>
        /// <returns>与 <see cref="T:System.Linq.IQueryable" /> 的此实例关联的 <see cref="T:System.Linq.Expressions.Expression" />。</returns>
        public Expression Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// 获取与此数据源关联的查询提供程序。
        /// </summary>
        /// <value>The provider.</value>
        /// <returns>与此数据源关联的 <see cref="T:System.Linq.IQueryProvider" />。</returns>
        public IQueryProvider Provider
        {
            get { return this.provider; }
        }
    }
}
