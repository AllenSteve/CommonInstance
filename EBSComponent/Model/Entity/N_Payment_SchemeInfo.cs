using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Payment_SchemeInfo
    {
        public int ID { get; set; }
        public string SchemeName { get; set; }
        public int SchemeType { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int Status { get; set; }
        public int SchemeCategory { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
