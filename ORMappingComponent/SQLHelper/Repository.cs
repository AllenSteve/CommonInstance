// ***********************************************************************
// Assembly         : ORMappingComponent
// Author           : Dragonet
// Created          : 12-18-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-18-2015
// ***********************************************************************
// <copyright file="SqlGenerator.cs" company="Microsoft">
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

namespace ComponentORM.SQLHelper
{
    /// <summary>
    /// 用于自动生成SQL语句
    /// </summary>
    public class Repository<T>
    {
        private IQueryable<T> query{ get ; set; }

        public Repository()
        { 
        }

        public Repository(IQueryable<T> query)
        {
            this.query = query;
        }

        public IEnumerable<T> AsEnumerable()
        {
            return this.query.AsEnumerable();
        }

        public List<T> ToList()
        {
            return this.query.ToList();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return this.query.Where(predicate).AsQueryable<T>();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return this.query.Count(predicate) > 0;
        }

        public IQueryable<T> All()
        {
            return this.query;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return this.query.Count(predicate);
        }
    }
}
