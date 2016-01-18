using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public partial class SendOrderInfo
    {
        public int ID { get; set; }
        public string SendOrderID { get; set; }
        public int CallbackID { get; set; }
        public int DealerID { get; set; }
        public decimal ServiceFee { get; set; }
        public int State { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public string OrderID { get; set; }
        public decimal ProjectCost { get; set; }
        public int SendOrderType { get; set; }
    }
}
