using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class Order_PayRecord
    {
        public int ID { get; set; }
        public string PayOrderId { get; set; }
        public string OrderId { get; set; }
        public string CityName { get; set; }
        public long GjSoufunId { get; set; }
        public string GjRealName { get; set; }
        public int GjTeamID { get; set; }
        public string GjTeamName { get; set; }
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public decimal PayMoney { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int PayState { get; set; }
        public System.DateTime PayTime { get; set; }
        public string PayNo { get; set; }
        public string PayRequestURL { get; set; }
        public string PayPostData { get; set; }
        public string PayReturnData { get; set; }
        public int RealPayState { get; set; }
        public Nullable<System.DateTime> RealPayTime { get; set; }
        public Nullable<decimal> RealPayMoney { get; set; }
        public string PayRemark { get; set; }
        public int OrderType { get; set; }
        public int CityId { get; set; }
        public Nullable<int> VoucherID { get; set; }
        public Nullable<decimal> VoucherAmount { get; set; }
        public int chargetype { get; set; }
        public string LastModifyAdminUser { get; set; }
        public int LastModifyAdminID { get; set; }
        public System.DateTime LastModifyTime { get; set; }
        public decimal RealPayAmount { get; set; }
        public string CouponID { get; set; }
        public Nullable<int> PayCashVersion { get; set; }
        public Nullable<decimal> PayRate { get; set; }
        public Nullable<decimal> PayRateDiscount { get; set; }
        public Nullable<int> CouponType { get; set; }
        public Nullable<decimal> CouponRate { get; set; }
        public Nullable<decimal> IntegralNum { get; set; }
        public Nullable<decimal> IntegralDiscount { get; set; }
        public string IntegralSequencelID { get; set; }
        public Nullable<int> EarnestID { get; set; }
        public Nullable<decimal> EarnestAmount { get; set; }
    }
}
