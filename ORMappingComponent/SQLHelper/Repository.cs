// ***********************************************************************
// Assembly         : ComponentORM
// Author           : Lacrima-PC
// Created          : 12-18-2015
//
// Last Modified By : Lacrima-PC
// Last Modified On : 12-21-2015
// ***********************************************************************
// <copyright file="Repository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ComponentORM.ORMappingTools;
using ORMappingComponent.ISQLHelper;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ComponentORM.SQLHelper
{
    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private IQueryable<T> query{ get ; set; }

        private DBHelper db { get; set; }

        public int Count
        {
            get
            {
                return this.query.Count();
            }
        }

        public Repository(int dbType = 0)
        {
            this.db = new DBHelper(dbType);
            this.query = db.AsQueryable<T>();
        }

        public Repository(IQueryable<T> query)
        {
            this.query = query;
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return this.query.Where(predicate).AsQueryable<T>();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> keySelector, out int resultCount, int pageIndex = 0, int pageSize = 10)
        {
            int skipCount = pageIndex * pageSize;
            var resetSet = predicate != null ? this.query.Where(predicate).OrderBy(keySelector).AsQueryable() : this.query.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(pageSize) : resetSet.Skip(skipCount).Take(pageSize);
            resultCount = resetSet.Count();
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

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return this.query.FirstOrDefault(predicate);
        }
    }
}
