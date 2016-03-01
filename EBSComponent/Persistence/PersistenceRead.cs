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
        public IEnumerable<T> Query<T>(object conditionParam = null, object columnParam = null)
        {
            return this.connection.Query<T>(sql.Query<T>(conditionParam, columnParam));
        }

        public IQueryable<T> AsQueryable<T>(object conditionParam = null, object columnParam = null)
        {
            return this.connection.Query<T>(sql.Query<T>(conditionParam, columnParam)).AsQueryable();
        }
    }
}
