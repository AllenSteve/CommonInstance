using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Order_QuoteEx
    {
        public int ID { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string OrderID { get; set; }
        public string TrueName { get; set; }
        public string IdentityNum { get; set; }
        public string Mobile { get; set; }
        public string ReMarkMobile { get; set; }
        public long DistrictID { get; set; }
        public string DistrictName { get; set; }
        public long EstateID { get; set; }
        public string EstateName { get; set; }
        public string BuildingNO { get; set; }
        public string UnitNO { get; set; }
        public string RoomNO { get; set; }
        public string RemarkAddress { get; set; }
        public decimal RealArea { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
        public decimal QuoteArea { get; set; }
        public int IsAreaModified { get; set; }
        public string AreaModifyLog { get; set; }
        public int IsIdentityNumModified { get; set; }
        public string IdentityNumModifyLog { get; set; }
        public int IsAddressModified { get; set; }
        public string AddressModifyLog { get; set; }
    }
}
