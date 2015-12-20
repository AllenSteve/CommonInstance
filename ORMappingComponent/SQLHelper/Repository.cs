using ComponentORM.ORMappingTools;
using ORMappingComponent.ISQLHelper;
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
    /// 
    /// </summary>
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
    }
}
