using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Payment_SchemeItem
    {
        public int ID { get; set; }
        public int OrderStateCode { get; set; }
        public string OrderStateName { get; set; }
        public decimal Rate { get; set; }
        public int Sort { get; set; }
        public int DetailId { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int DetailType { get; set; }
    }
}
