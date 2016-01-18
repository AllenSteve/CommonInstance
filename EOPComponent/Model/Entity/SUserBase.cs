using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public partial class SUserBase
    {
        public int ID { get; set; }
        public long SoufunID { get; set; }
        public string RealName { get; set; }
        public string Phone { get; set; }
        public int LastApplyID { get; set; }
        public System.DateTime LastApplyTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public int ApplyType { get; set; }
        public string DealerName { get; set; }
        public string DealerContact { get; set; }
        public string DealerContactPhone { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int VisitState { get; set; }
    }
}
