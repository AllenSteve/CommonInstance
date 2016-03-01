using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Payment_SchemeDetail
    {
        public int ID { get; set; }
        public int DetailType { get; set; }
        public string DetailName { get; set; }
        public int SchemeId { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
