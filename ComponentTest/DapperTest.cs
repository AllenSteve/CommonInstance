using ORMappingComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest
{
    public class DapperTest
    {
        private string conn;
        public DapperTest()
        {
            this.conn = @"Data Source=.\localdb;Initial Catalog=workDB;UID=sa;PWD=123456;Connect Timeout=32767 ";
            DapperInstance.OneToOne(conn);
        }

    }
}
