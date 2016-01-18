using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class OrderFollowUpEvaluateLog
    {
        public int ID { get; set; }
        public long SoufunID { get; set; }
        public int FollowUpID { get; set; }
        public int IsFlower { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public long OperSoufunID { get; set; }
    }
}
