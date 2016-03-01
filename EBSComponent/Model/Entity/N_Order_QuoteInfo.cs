using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_Order_QuoteInfo
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public string DesignContractNO { get; set; }
        public decimal DesignAmount { get; set; }
        public int DesignCycle { get; set; }
        public string ConstructContractNO { get; set; }
        public System.DateTime PlanStartBuildDate { get; set; }
        public System.DateTime PlanEndBuildDate { get; set; }
        public int IsWeekEndWork { get; set; }
        public int PlanDayCount { get; set; }
        public int SuitID { get; set; }
        public System.DateTime DesignContractingTime { get; set; }
        public System.DateTime ConstructionContractingTime { get; set; }
        public decimal QuoteAmount { get; set; }
        public int DiscountUsageType { get; set; }
        public int MaterialLock { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string ConstructModelName { get; set; }
        public string DesignModelName { get; set; }
    }
}
