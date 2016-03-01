using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class ConstructAtaccepnce_InfoSortDetails
    {
        public int ID { get; set; }
        public int InfoSortID { get; set; }
        public string ConstructName { get; set; }
        public string SimpleName { get; set; }
        public int StateCode { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int Sort { get; set; }
        public int IsDel { get; set; }
    }
}
