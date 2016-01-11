using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBS.Interface.EContract.Model
{
    public class AmountCalculationModel
    {
        private string orderId = string.Empty;
        public string OrderId { get { return this.orderId; } }

        private List<EBS.Interface.Model.N_Order_Payment> nopList = null;

        private int suitId = -1;
        public int SuitId { get { return this.suitId; } }

        public string ConstructContractNo { get; set; }

        #region 基础数据
        //套餐
        private decimal suitAmount = 0.0m;
        public decimal SuitAmount { get { return this.suitAmount; } }
        private decimal upgradeAmount = 0.0m;
        public decimal UpgradeAmount { get { return this.upgradeAmount; } }
        private decimal outerAmount = 0.0m;
        public decimal OuterAmount { get { return this.outerAmount; } }
        //自定义
        private decimal materialAmount = 0.0m;
        public decimal MaterialAmount { get { return this.materialAmount; } }
        private decimal techAmount = 0.0m;
        public decimal TechAmount { get { return this.techAmount; } }
        private decimal auxiliaryAmount = 0.0m;
        public decimal AuxiliaryAmount { get { return this.auxiliaryAmount; } }
        //公用
        private decimal packageAmount = 0.0m;
        public decimal PackageAmount { get { return this.packageAmount; } }

        private decimal changeAmount = 0.0m;
        public decimal ChangeAmount { get { return this.changeAmount; } }
        private decimal designAmount = 0.0m;
        public decimal DesignAmount { get { return this.designAmount; } }
        private decimal discountAmount = 0.0m;
        public decimal DiscountAmount { get { return this.discountAmount; } }

        //比率
        private decimal remotionRate = 0.0m;
        public decimal RemotionRate { get { return this.remotionAmount; } }
        private decimal manageRate = 0.0m;
        private decimal taxRate = 0.0m;
        private decimal chanageTaxAmount = 0.0m;
        public decimal ChangeTaxAmount { get { return this.chanageTaxAmount; } }
        #endregion

        private decimal baseAmount = 0.0m;
        private decimal discountUsageAmount = 0.0m;
        public decimal DiscountUsageAmount { get { return this.discountUsageAmount; } }

        private decimal taxAmount = 0.0m;
        public decimal TaxAmount { get { return this.taxAmount; } }

        private decimal remotionAmount = 0.0m;
        public decimal RemotionAmount { get { return this.remotionAmount; } }

        private decimal manageAmount = 0.0m;
        public decimal ManageAmount { get { return this.manageAmount; } }

        private decimal contractAmount = 0.0m;
        public decimal ContractAmount { get { return this.contractAmount; } }



        private decimal orderAmount = 0.0m;
        public decimal OrderAmount { get { return this.orderAmount; } }

        public AmountCalculationModel(string OrderId, decimal remotionRate = -1, bool UpdateRateData = false)
        {
            this.orderId = OrderId;
            this.nopList = EBS.Interface.Data.DBOper.N_Order_Payment.GetList("IsDel=0 and OrderId=@OrderId", "PaymentType asc", new object[] { this.orderId }, false);
            var model = EBS.Interface.Data.DBOper.N_Order_QuoteInfo.Get("IsDel=0 and OrderId=@OrderId", "CreateTime desc", new object[] { this.orderId }, false);
            this.ConstructContractNo = model.ConstructContractNO;
            this.suitId = model.SuitID;
            if (remotionRate == -1)
            {
                this.remotionRate = this.nopList.Find(x => x.PaymentType == 4).Rate == -1 ? 0 : this.nopList.Find(x => x.PaymentType == 4).Rate;
            }
            else
            {
                this.remotionRate = remotionRate;
            }
            GetOrderAmount();
            if (UpdateRateData)
            {
                UpdateData();
            }
        }

        public AmountCalculationModel(string OrderId, bool UpdateRateData = false)
            : this(OrderId, -1, UpdateRateData)
        { }

        private void GetBaseAmount()
        {
            this.suitAmount = this.nopList.Find(x => x.PaymentType == 0).SingleAmount;
            this.upgradeAmount = this.nopList.Find(x => x.PaymentType == 3).SingleAmount;
            this.outerAmount = this.nopList.Find(x => x.PaymentType == 2).SingleAmount;
            this.packageAmount = this.nopList.Find(x => x.PaymentType == 1).SingleAmount;
            this.changeAmount = this.nopList.Find(x => x.PaymentType == 7).SingleAmount;
            this.chanageTaxAmount = this.nopList.Find(x => x.PaymentType == 8).SingleAmount;
            this.discountAmount = this.nopList.Find(x => x.PaymentType == 9).SingleAmount;
            this.materialAmount = this.nopList.Find(x => x.PaymentType == 10).SingleAmount;
            this.techAmount = this.nopList.Find(x => x.PaymentType == 11).SingleAmount;
            this.auxiliaryAmount = this.nopList.Find(x => x.PaymentType == 12).SingleAmount;
            this.designAmount = EBS.Interface.Data.DBOper.N_Order_QuoteInfo.Get("IsDel=0 and OrderId=@orderId", "CreateTime desc", new object[] { this.orderId }, false).DesignAmount;


            this.manageRate = this.nopList.Find(x => x.PaymentType == 5).Rate;
            this.taxRate = this.nopList.Find(x => x.PaymentType == 6).Rate;

            this.baseAmount = this.suitId == 0 ? (this.materialAmount + this.auxiliaryAmount + this.techAmount + this.packageAmount) : (this.suitAmount + this.upgradeAmount + this.outerAmount + this.packageAmount);
        }

        private void GetContractAmount()
        {
            GetRateAmount();
            this.contractAmount = this.baseAmount * (1 + this.remotionRate) * (1 + this.manageRate) * (1 + this.taxRate);
        }

        private void GetDiscountUsageAmount()
        {
            GetBaseAmount();
            var noqiModel = EBS.Interface.Data.DBOper.N_Order_QuoteInfo.Get("IsDel=0 and OrderId=@OrderId", "CreateTime desc", new object[] { this.orderId }, false);
            if (noqiModel.DiscountUsageType > 0)
            {
                this.discountUsageAmount = noqiModel.DiscountUsageType == 1 ? (this.upgradeAmount - this.discountAmount >= 0 ? this.discountAmount : this.upgradeAmount) : noqiModel.DiscountUsageType == 2 ? (this.outerAmount - this.discountAmount >= 0 ? this.discountAmount : this.outerAmount) : this.discountAmount;
            }
            else
            {
                this.discountUsageAmount = 0.0m;
            }
        }

        private void GetRateAmount()
        {
            GetBaseAmount();
            this.remotionAmount = this.baseAmount * this.remotionRate;
            this.manageAmount = this.baseAmount * (1 + this.remotionRate) * this.manageRate;
            this.taxAmount = this.baseAmount * (1 + this.remotionRate) * (1 + this.manageRate) * this.taxRate;
        }

        private void GetOrderAmount()
        {
            GetContractAmount();
            GetDiscountUsageAmount();
            this.orderAmount = this.contractAmount + this.changeAmount - this.discountUsageAmount + this.designAmount;
        }

        private void UpdateData()
        {
            GetRateAmount();
            List<KeyValuePair<int, decimal>> dataList = new List<KeyValuePair<int, decimal>>()
            {
                new  KeyValuePair<int, decimal> (4,this.remotionAmount),
                new  KeyValuePair<int, decimal> (5,this.manageAmount),
                new  KeyValuePair<int, decimal> (6,this.taxAmount)
            };
            foreach (var item in dataList)
            {
                EBS.Interface.Data.DBOper.N_Order_Payment.Update("IsDel=0 and OrderId=@orderId and PaymentType=@type", "SingleAmount=@amount", new object[] { this.orderId, item.Key, item.Value });
            }

        }
    }
}