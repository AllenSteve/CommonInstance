using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentORM.SQLHelper;

namespace EbsComponent.Method.DatabaseMethod
{
    public class RepositoryEBS
    {
        private int dbType { get; set; }

        public RepositoryEBS()
        {
            this.dbType = (int)ComponentORM.ORMappingTools.DBHelper.Sqldb.SandBox;
        }

        public Repository<T> Query<T>() where T : class
        {
            var repository = new Repository<T>(dbType);
            return repository;
        }
    }
}
