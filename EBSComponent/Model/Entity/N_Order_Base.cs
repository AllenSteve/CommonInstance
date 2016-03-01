using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Order_Base
    {
        public int ID { get; set; }
        public string OrderId { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public int OrderType { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int UserRank { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string TrueName { get; set; }
        public long SoufunId { get; set; }
        public string SoufunName { get; set; }
        public string Phone { get; set; }
        public string IdentityNumber { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }
        public int FamilyType { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long EstateId { get; set; }
        public string EstateName { get; set; }
        public string BuildingNO { get; set; }
        public string UnitNO { get; set; }
        public string RoomNO { get; set; }
        public decimal Area { get; set; }
        public int HouseStatus { get; set; }
        public int HouseUse { get; set; }
        public int HouseType { get; set; }
        public int PreferStyle { get; set; }
        public System.DateTime DeliveryTime { get; set; }
        public System.DateTime GrabOrderDate { get; set; }
        public System.DateTime SignContractDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public int IsEarnest { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public int CustomerDel { get; set; }
        public System.DateTime StartConstructTime { get; set; }
    }
}
