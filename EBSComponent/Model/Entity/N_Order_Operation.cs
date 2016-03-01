using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Order_Operation
    {
        public int ID { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal Paid { get; set; }
        public decimal Unpay { get; set; }
        public int IsVisit { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int QuoteMark { get; set; }
        public int MaterialApproveStatus { get; set; }
        public System.DateTime MaterialApproveTime { get; set; }
        public int CheckSortID { get; set; }
        public int CheckActionID { get; set; }
        public int CheckAtaccepnceID { get; set; }
        public int PaymentSchemeId { get; set; }
        public int IsSuit { get; set; }
    }
}
