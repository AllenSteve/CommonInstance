using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class ConstructAtaccepnce_InfoSort
    {
        public int ID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string InfoSortName { get; set; }
        public long OperSoufunID { get; set; }
        public string OperSoufunName { get; set; }
        public int Status { get; set; }
        public int IsDel { get; set; }
    }
}
