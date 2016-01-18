using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public partial class CallbackUserInfo
    {
        public int ID { get; set; }
        public int ApplyUserID { get; set; }
        public long SoufunID { get; set; }
        public string RealName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string Phone { get; set; }
        public int Rating { get; set; }
        public int AddressID { get; set; }
        public string EstateAddress { get; set; }
        public decimal ReferenceArea { get; set; }
        public decimal Budget { get; set; }
        public System.DateTime DeliveryTime { get; set; }
        public System.DateTime MeasuringTime { get; set; }
        public int DecorateType { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public int SendState { get; set; }
        public int SendDealerCount { get; set; }
        public System.DateTime SendTime { get; set; }
        public int RegionID { get; set; }
        public string RegionName { get; set; }
    }
}
