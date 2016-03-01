using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class Order_Form
    {
        public int ID { get; set; }
        public string OrderId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string BuildingNO { get; set; }
        public string UnitNO { get; set; }
        public string RoomNO { get; set; }
        public string ContractNO { get; set; }
        public System.DateTime PlanStartBuildDate { get; set; }
        public System.DateTime PlanEndBuildDate { get; set; }
        public int WeekEndWork { get; set; }
        public int PlanDayCount { get; set; }
        public decimal DesignAmount { get; set; }
        public decimal RealArea { get; set; }
        public System.DateTime CreateTime { get; set; }
        public decimal TechAmount { get; set; }
        public decimal AuxiliaryAmount { get; set; }
        public decimal MaterialAmount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal PlatformTax { get; set; }
        public int CityFeeId { get; set; }
        public decimal CityFeeRate { get; set; }
        public decimal CityTaxRate { get; set; }
        public int IsDel { get; set; }
        public decimal ContractAmount { get; set; }
        public decimal OriginDesignAmount { get; set; }
        public decimal OriginTechAmount { get; set; }
        public decimal OriginMaterialAmount { get; set; }
        public decimal OriginAuxiliaryAmount { get; set; }
        public int SuitId { get; set; }
        public decimal SuitSinglePrice { get; set; }
        public decimal SuitDesignSinglePrice { get; set; }
        public int HasPlatformFee { get; set; }
        public int PaymentSchemeId { get; set; }
        public string IdentityNumber { get; set; }
        public string DesignContractNO { get; set; }
        public string ConstructContractNO { get; set; }
        public decimal MaterialUpdateAmount { get; set; }
        public decimal AuxiliaryUpdateAmount { get; set; }
        public decimal TechUpdateAmount { get; set; }
        public decimal NoSuitMaterialAmount { get; set; }
        public decimal NoSuitAuxiliaryAmount { get; set; }
        public decimal NoSuitTechAmount { get; set; }
        public decimal ConstructAmount { get; set; }
        public decimal YuanChengRate { get; set; }
        public decimal YuanChengAmount { get; set; }
        public decimal CustomConstructAmount { get; set; }
        public decimal CustomMaterialAmount { get; set; }
        public decimal SuitMinArea { get; set; }
        public decimal OrderChangeYuanChengAmount { get; set; }
        public decimal GuaranteeFeeRate { get; set; }
        public decimal FaultFeeRate { get; set; }
        public decimal NoSuitMaterialOrigin { get; set; }
        public decimal NoSuitAuxiliaryOrigin { get; set; }
        public decimal NoSuitTechOrigin { get; set; }
        public byte[] timestamp { get; set; }
        public int NPaymentSchemeId { get; set; }
        public int IsDelayPay { get; set; }
        public int NContractLock { get; set; }
        public int NOrderLock { get; set; }
        public int NQuoteStatus { get; set; }
    }
}
