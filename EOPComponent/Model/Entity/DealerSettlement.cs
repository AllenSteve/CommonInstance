using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public partial class DealerSettlement
    {
        public int Id { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string OrderID { get; set; }
        public int TradeType { get; set; }
        public string TradeName { get; set; }
        public decimal TransactionAmount { get; set; }
        public string OutsideRunningNo { get; set; }
        public string InsideRunningNo { get; set; }
        public int Status { get; set; }
        public System.DateTime TradeTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public string SendOrderID { get; set; }
        public int SUserID { get; set; }
        public string SUserName { get; set; }
        public string SUserPhone { get; set; }
        public string EstateAddress { get; set; }
    }
}
