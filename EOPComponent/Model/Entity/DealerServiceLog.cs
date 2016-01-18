using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public partial class DealerServiceLog
    {
        public int ID { get; set; }
        public int ServiceType { get; set; }
        public long OperatorSoufunID { get; set; }
        public int OperatorRoleID { get; set; }
        public string OperatorRoleName { get; set; }
        public string OperatorRealName { get; set; }
        public string OrderID { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public int DealerID { get; set; }
        public int ApplyID { get; set; }
        public string SendOrderID { get; set; }
        public string SendOrderCompany { get; set; }
        public string SigningCompany { get; set; }
        public string CallbackRemark { get; set; }
        public string ApplySourcePageUrl { get; set; }
    }
}
