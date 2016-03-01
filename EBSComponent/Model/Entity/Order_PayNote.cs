using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class Order_PayNote
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public int PayTypeID { get; set; }
        public string PayTypeName { get; set; }
        public string PayName { get; set; }
        public decimal PayAmount { get; set; }
        public Nullable<decimal> NewPayAmount { get; set; }
        public int ShigongStageID { get; set; }
        public string ShigongStageName { get; set; }
        public string PayDesc { get; set; }
        public string PayMobile { get; set; }
        public System.DateTime Createtime { get; set; }
        public decimal AddAmount { get; set; }
        public long GjSoufunID { get; set; }
        public string GjRealName { get; set; }
        public string OpIp { get; set; }
        public int PayState { get; set; }
        public System.DateTime RealPayTime { get; set; }
        public int IsDel { get; set; }
        public int IsLastPay { get; set; }
        public int chargetype { get; set; }
        public decimal RealPayAmount { get; set; }
        public byte[] timestamp { get; set; }
        public Nullable<int> DingJinID { get; set; }
        public Nullable<decimal> DingJinAmount { get; set; }
        public System.DateTime ConsumeTime { get; set; }
        public decimal BackAmount { get; set; }
        public decimal DesignFeeAmount { get; set; }
    }
}
