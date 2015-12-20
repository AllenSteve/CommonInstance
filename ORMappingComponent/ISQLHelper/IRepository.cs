using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORMappingComponent.ISQLHelper
{
    public interface IRepository<T> where T : class
    {
        int Count { get; }

        IQueryable<T> All();

        T Find(Expression<Func<T, bool>> predicate);

        bool Contains(Expression<Func<T, bool>> predicate);

        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);
    }
}
