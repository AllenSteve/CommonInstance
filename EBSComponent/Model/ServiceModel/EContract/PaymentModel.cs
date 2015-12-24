using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBS.Interface.EContract.Model
{
    public class PaymentModel
    {
        #region 属性

        /// <summary>
        /// 报价客户信息
        /// </summary>
        public EBS.Interface.Model.N_Order_QuoteEx OrderQuoteEx { get; set; }

        /// <summary>
        /// 报价签约信息
        /// </summary>
        public EBS.Interface.Model.N_Order_QuoteInfo OrderQuoteInfo { get; set; }

        /// <summary>
        /// 房屋信息：list
        /// </summary>
        public List<EBS.Interface.Model.N_Order_Room> ListOrderRoom { get; set; }
        /// <summary>
        /// 订单结算:list
        /// </summary>
        public List<EBS.Interface.Model.N_Order_Payment> ListOrderPayment { get; set; }

        /// <summary>
        /// 订单材料信息:list
        /// </summary>
        public List<EBS.Interface.Model.N_Order_Material> ListOrderMaterial { get; set; }


        ///// <summary>
        ///// 辅材基础信息
        ///// </summary>
        //public List<EBS.Interface.Model.N_Auxiliary_Info> ListOrderAuxiliary { get; set; }

        ///// <summary>
        ///// 人工基础信息
        ///// </summary>
        //public List<EBS.Interface.Model.N_Tech_Info> listOrderTech { get; set; }


        /// <summary>
        /// 报价套餐信息:list
        /// </summary>
        public EBS.Interface.Model.N_Order_Suit OrderSuit { get; set; }

        /// <summary>
        /// 报价包信息：list
        /// </summary>
        public List<EBS.Interface.Model.N_Order_Package> ListOrderPackage { get; set; }

        /// <summary>
        /// 变更基础信息：list
        /// </summary>
        public List<EBS.Interface.Model.N_Order_ChangeInfo> ListOrderChangeInfo { get; set; }


        /// <summary>
        /// 城市费率信息
        /// </summary>
        public EBS.Interface.Model.CityFee CityFee { get; set; }

        /// <summary>
        /// 各种结算类型、金额、费率
        /// Dictionary<int PaymentType, Dictionary<decimal SingelAmount, decimal Rate>>
        /// </summary>
        public Dictionary<int, Dictionary<decimal, decimal>> Amount = new Dictionary<int, Dictionary<decimal, decimal>>();



        #region 套餐涉及独有属性
        /// <summary>
        /// 套餐费
        /// </summary>
        public decimal SuitAmount { get; set; }


        /// <summary>
        /// 套餐外费用 选择的单品总费用
        /// </summary>
        public decimal SuitAttachAmount { get; set; }

        /// <summary>
        /// 套餐升级费 
        /// </summary>
        public decimal SuitUpgradeAmount { get; set; }


        #endregion

        #region 个性化涉及独有属性
        /// <summary>
        /// 主材费
        /// </summary>
        public decimal MaterialAmount { get; set; }
        /// <summary>
        /// 辅料费
        /// </summary>
        public decimal AuxiliaryAmount { get; set; }
        /// <summary>
        /// 人工费
        /// </summary>
        public decimal TechAmount { get; set; }
        #endregion

        /// <summary>
        /// 包的费用 PackageType=0为基础包，基础包不收费
        /// </summary>
        public decimal PackageAmount { get; set; }

        /// <summary>
        /// 远程费率
        /// </summary>
        public decimal yuanchengRate { get; set; }

        /// <summary>
        /// 远程费
        /// </summary>
        public decimal RemoteAmount { get; set; }
        /// <summary>
        /// 管理费费率
        /// </summary>
        public decimal FeeRate { get; set; }

        /// <summary>
        /// 管理费
        /// </summary>
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 税金
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 折现金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 用于抵消套餐升级费用或者用于抵消套餐外费用
        /// </summary>
        public decimal DiscountAmountUse { get; set; }

        public decimal DiscountAmountLeft { get; set; }

        /// <summary>
        /// 变更费用
        /// </summary>
        public decimal ChangeAmount { get; set; }



        /// <summary>
        /// 变更税金
        /// </summary>
        public decimal ChangeTaxAmount { get; set; }
        /// <summary>
        /// 合同总金额
        /// </summary>
        public decimal ContractAmount { get; set; }

        /// <summary>
        /// 设计费
        /// </summary>
        public decimal DesignAmount { get; set; }

        public string ConstructContractNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="orderId"></param>
        public PaymentModel(string orderId = "")
        {
            this.OrderId = orderId;

            this.OrderQuoteEx = EBS.Interface.Data.DBOper.N_Order_QuoteEx.Get(" OrderID = @OrderID and IsDel = 0 ", " CreateTime desc", new object[] { orderId }, true);
            this.OrderQuoteInfo = EBS.Interface.Data.DBOper.N_Order_QuoteInfo.Get(" OrderID = @OrderID and IsDel = 0 ", " CreateTime desc", new object[] { orderId }, true);
            this.OrderSuit = EBS.Interface.Data.DBOper.N_Order_Suit.Get(" OrderID = @OrderID and IsDel = 0 ", " CreateTime desc", new object[] { orderId }, true);
            this.ListOrderPackage = EBS.Interface.Data.DBOper.N_Order_Package.GetList(" OrderID = @OrderID and IsDel = 0 ", "", new object[] { orderId }, true);
            this.ListOrderRoom = EBS.Interface.Data.DBOper.N_Order_Room.GetList(" OrderID = @OrderID and IsDel = 0 ", "", new object[] { orderId }, true);
            this.ListOrderPayment = EBS.Interface.Data.DBOper.N_Order_Payment.GetList(" OrderID = @OrderID and IsDel = 0 ", "", new object[] { orderId }, true);
            this.ListOrderMaterial = EBS.Interface.Data.DBOper.N_Order_Material.GetList(" OrderID = @OrderID and IsDel = 0 ", "", new object[] { orderId }, true);
            this.ListOrderChangeInfo = EBS.Interface.Data.DBOper.N_Order_ChangeInfo.GetList(" OrderID = @OrderID and IsDel = 0 ", "", new object[] { orderId }, true);
            this.CityFee = EBS.Interface.Data.DBOper.CityFee.Get(" CityID = @CityID and IsDel = 0 ", " CreateTime desc", new object[] { this.OrderQuoteEx.CityID }, true);
            this.ConstructContractNo = this.OrderQuoteInfo.ConstructContractNO;
        }

        #region 方法 获得结算明细

        /// <summary>
        /// 方法 获得结算明细
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public static PaymentModel GetPaymentList(string orderId)
        {
            PaymentModel Model = new PaymentModel(orderId);
            #region 远程费率处理
            decimal yuanchengRate;
            /*
             * 计算结算明细时需要用到手动输入的远程费率，如果没有手动输入则用默认的远程费率
             */
            //春梅修改处-------------
            //如果结算表里远程税率不为空默认0
            if (Model.ListOrderPayment.Find(p => p.PaymentType == 4) != null && Model.ListOrderPayment.Find(p => p.PaymentType == 4).ID > 0 && Model.ListOrderPayment.Find(p => p.PaymentType == 4).Rate != -1)
            {
                yuanchengRate = Model.ListOrderPayment.Find(p => p.PaymentType == 4).Rate;
            }
            else
            {
                //yuanchengRate = Model.CityFee.YuanchengRate;
                yuanchengRate = 0.0000M;
            }
            #endregion

            #region 套餐结算
            if (Model.OrderQuoteInfo.SuitID > 0)//套餐
            {
                #region 套餐费(套餐单价*签约面积)
                var suitUnitPrice = Model.OrderSuit.UnitPrice;
                var orderRealArea = Model.OrderQuoteEx.RealArea;//修改后的实际面积//Model.OrderQuoteEx.QuoteArea;//签约面积
                Model.SuitAmount = suitUnitPrice * orderRealArea;//套餐费

                #region 特殊处理 个性化独有结算类型对应金额 赋值0
                Model.Amount[10] = new Dictionary<decimal, decimal>() { { 0.0000M, 1.0000M } };
                Model.Amount[11] = new Dictionary<decimal, decimal>() { { 0.0000M, 1.0000M } };
                Model.Amount[12] = new Dictionary<decimal, decimal>() { { 0.0000M, 1.0000M } };
                #endregion

                //春梅修改处-----如果实际面积小于套餐起装面积那么就用起装面积计算（不明白问我）
                //if (Model.OrderQuoteInfo.MaterialLock==1)
                //{
                //    Model.SuitAmount=suitUnitPrice*
                //}
                var suitMinArea = Model.OrderSuit.MinArea;
                if (orderRealArea >= suitMinArea)
                {
                    Model.SuitAmount = suitUnitPrice * orderRealArea;//套餐费
                }
                else
                {
                    Model.SuitAmount = suitUnitPrice * suitMinArea;//套餐费
                }

                Model.Amount[0] = new Dictionary<decimal, decimal>() { { Model.SuitAmount, 1.0000M } };
                #endregion

                #region 包费
                //（基础包不收费，p.PackageType = 1,3为基础包）
                Model.PackageAmount = Model.ListOrderPackage.Where(p => p.PackageType != 1 && p.PackageType != 3).Sum(p => p.PackageAmount);
                Model.Amount[1] = new Dictionary<decimal, decimal>() { { Model.PackageAmount, 1.0000M } };
                #endregion

                #region 套餐升级费
                //升级后清单（p.UpgradedSourceID!=0）
                var listAfterUpgrade = Model.ListOrderMaterial.Where(p => p.SuitID == -1 && p.MaterialType == 0 && p.UpgradedSourceID != 0 && p.PackageID == 0);
                foreach (var item in listAfterUpgrade)
                {
                    //var beforeUpgrade = viewModel.ListOrderMaterial.First(p => p.ID == item.UpgradedSourceID);
                    //找到升级前的数据
                    var beforeUpgrade = EBS.Interface.Data.DBOper.N_Order_Material.Get(" Id=@Id ", "", new object[] { item.UpgradedSourceID }, true);
                    Model.SuitUpgradeAmount += (item.Price - beforeUpgrade.Price) * item.Num;
                }
                Model.Amount[3] = new Dictionary<decimal, decimal>() { { Model.SuitUpgradeAmount, 1.0000M } };
                #endregion

                #region 套餐外费用（主材费+辅料费+人工费）
                //SuitID=-1为单品,主材费+辅料费+人工费，包外单品不能升级和折现，包外单品不能折现，包内产品可以折现，升级后的产品不能折现
                //弃用201509281503
                //viewModel.SuitAttachAmount = viewModel.ListOrderMaterial.Where(p => p.IsUpgraded == 0).Sum(p => p.Price * p.Num);
                Model.SuitAttachAmount = Model.ListOrderMaterial.Where(p => p.SuitID == -1 && p.PackageID == 0 && p.UpgradedSourceID == 0).Sum(p => p.Price * p.Num);
                Model.Amount[2] = new Dictionary<decimal, decimal>() { { Model.SuitAttachAmount, 1.0000M } };
                #endregion

                #region （套餐费+套餐升级费用+套餐外费用+包的费用）temp
                decimal temp = Model.SuitAmount + Model.SuitUpgradeAmount + Model.SuitAttachAmount + Model.PackageAmount;
                #endregion

                #region 远程费
                //远程费=（套餐费+套餐升级费用+套餐外费用+包的费用）*远程费比例；
                Model.RemoteAmount = (temp * yuanchengRate);
                Model.Amount[4] = new Dictionary<decimal, decimal>() { { Model.RemoteAmount, yuanchengRate } };
                #endregion

                #region 管理费
                //管理费=（套餐费+套餐升级费用+套餐外费用+包的费用）*（1+远程费比例）*管理费比例；
                Model.FeeAmount = temp * (1 + yuanchengRate) * Model.CityFee.FeeRate;
                Model.Amount[5] = new Dictionary<decimal, decimal>() { { Model.FeeAmount, Model.CityFee.FeeRate } };
                #endregion

                #region 税金
                //税金=（套餐费+套餐升级费用+套餐外费用+包的费用）*（1+远程费比例）*（1+管理费比例）*税金比例；
                Model.TaxAmount = temp * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * Model.CityFee.TaxRate;
                Model.Amount[6] = new Dictionary<decimal, decimal>() { { Model.TaxAmount, Model.CityFee.TaxRate } };
                #endregion

                #region 折现金额
                //折现金额=当前折现产品搜房价*折现数量*0.8
                Model.DiscountAmount = Model.ListOrderMaterial.Where(p => p.MaterialType == 0).Sum(p => p.DiscountingAmount);
                //到结算页面N_Order_QuoteInfo必不为空，取QuoteInfo表里的 DiscountUsageType 0-不用于抵消；1-用于抵消升级费用；2-用于抵消套餐外费用
                #region QuoteInfo有DiscountUsageType

                if (Model.OrderQuoteInfo.DiscountUsageType == 2)
                {//用于抵消套餐外费用
                    if (Model.SuitAttachAmount - Model.DiscountAmount >= 0)
                    {//折现费用全部用于抵消
                        Model.DiscountAmountUse = Model.DiscountAmount;
                        Model.DiscountAmountLeft = 0.000M;
                        //viewModel.SuitAttachAmount -= viewModel.DiscountAmount;//20150924更改：套餐升级费及套餐外费用显示固有金额，不必减折现费用
                    }
                    else
                    {//折现费用部分用于抵消
                        Model.DiscountAmountLeft = Model.DiscountAmount - Model.SuitAttachAmount;
                        Model.DiscountAmountUse = Model.SuitAttachAmount;
                        //viewModel.SuitAttachAmount = 0.0000M;//20150924更改：套餐升级费及套餐外费用显示固有金额，不必减折现费用
                    }
                }
                else if (Model.OrderQuoteInfo.DiscountUsageType == 1)
                {//用于抵消套餐升级费用
                    if (Model.SuitUpgradeAmount - Model.DiscountAmount >= 0)
                    {//折现费用全部用于抵消
                        Model.DiscountAmountUse = Model.DiscountAmount;
                        Model.DiscountAmountLeft = 0.000M;
                        //viewModel.SuitUpgradeAmount -= viewModel.DiscountAmount;//20150924更改：套餐升级费及套餐外费用显示固有金额，不必减折现费用
                    }
                    else
                    {//折现费用部分用于抵消
                        Model.DiscountAmountLeft = Model.DiscountAmount - Model.SuitUpgradeAmount;
                        Model.DiscountAmountUse = Model.SuitUpgradeAmount;
                        //viewModel.SuitUpgradeAmount = 0.0000M;//20150924更改：套餐升级费及套餐外费用显示固有金额，不必减折现费用
                    }
                }
                else
                {//
                    Model.DiscountAmountUse = 0.0000M;
                    Model.DiscountAmountLeft = Model.DiscountAmount;
                }

                #endregion

                Model.Amount[9] = new Dictionary<decimal, decimal>() { { Model.DiscountAmount, Model.CityFee.SupplyPriceDicountRate } };
                #endregion

                #region 变更费用
                //（变更人工费+变更辅料费+变更主材费+变更面积费用）*【1+（1+远程费比例）*（管理费比例+（1+管理费比例）*税金比例）】+（变更人工费+变更辅料费+变更主材费+变更面积费用）*远程费比例  
                Model.ChangeAmount =
                    Model.ListOrderChangeInfo.Sum(p => (p.TechAmount + p.AuxiliaryAmount + p.MaterialAmount + p.AreaAmount) * (1 + (1 + yuanchengRate) * (Model.CityFee.FeeRate + (1 + Model.CityFee.FeeRate) * Model.CityFee.TaxRate)) + (p.TechAmount + p.AuxiliaryAmount + p.MaterialAmount + p.AreaAmount) * yuanchengRate);
                Model.Amount[7] = new Dictionary<decimal, decimal>() { { Model.ChangeAmount, 1.0000M } };
                #endregion

                #region 变更税金
                //变更税金=（人工变更金额+辅料变更金额+主材变更金额+面积变更金额）*（1+远程费比例）*（1+管理费比例）*税金比例；
                Model.ChangeTaxAmount = Model.ListOrderChangeInfo.Sum(p => (p.TechAmount + p.AuxiliaryAmount + p.MaterialAmount + p.AreaAmount) * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * Model.CityFee.TaxRate);
                Model.Amount[8] = new Dictionary<decimal, decimal>() { { Model.ChangeTaxAmount, 1.0000M } };
                #endregion

                #region 合同总金额
                //合同总金额=【（套餐费+套餐升级费用+套餐外费用+包的费用）*（1+远程费比例）*（1+管理费比例）*（1+税金比例）】；
                Model.ContractAmount = (Model.SuitAmount + Model.SuitUpgradeAmount + Model.SuitAttachAmount + Model.PackageAmount) * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * (1 + Model.CityFee.TaxRate);
                #endregion

                #region 订单总金额
                //订单总金额=合同总金额+变更费用-使用的折现费用+设计签约的设计费
                Model.OrderAmount = Model.ContractAmount + Model.ChangeAmount - Model.DiscountAmountUse + Model.OrderQuoteInfo.DesignAmount;
                #endregion
            }

            #endregion

            #region 个性化结算
            else if (Model.OrderQuoteInfo.SuitID == 0)
            {//个性化

                #region 特殊处理 套餐独有结算类型对应金额 赋值0
                Model.Amount[0] = new Dictionary<decimal, decimal>() { { 0.0000M, 1.0000M } };
                Model.Amount[2] = new Dictionary<decimal, decimal>() { { 0.0000M, 1.0000M } };
                Model.Amount[3] = new Dictionary<decimal, decimal>() { { 0.0000M, 1.0000M } };
                Model.Amount[9] = new Dictionary<decimal, decimal>() { { 0.0000M, Model.CityFee.SupplyPriceDicountRate } };
                #endregion

                #region 主材费
                //MaterialType == 0
                //201510121032更改 主材费=单品主材+包内主材升级费用
                //Model.MaterialAmount = Model.ListOrderMaterial.Where(p => p.MaterialType == 0 && p.SuitID==-1 && p.PackageID == 0 && p.IsUpgraded == 0).Sum(p => p.Price * p.Num);
                Model.MaterialAmount = Model.ListOrderMaterial.Where(p => p.MaterialType == 0 && p.SuitID == -1 && p.PackageID == 0 && p.UpgradedSourceID == 0).Sum(p => p.Price * p.Num);
                #region 包内主材升级费
                var listAfterUpgrade = Model.ListOrderMaterial.Where(p => p.SuitID == -1 && p.MaterialType == 0 && p.UpgradedSourceID != 0 && p.PackageID == 0);
                foreach (var item in listAfterUpgrade)
                {
                    //找到升级之前的数据
                    var beforeUpgrade = EBS.Interface.Data.DBOper.N_Order_Material.Get(" Id=@Id ", "", new object[] { item.UpgradedSourceID }, true);
                    Model.MaterialAmount += (item.Price - beforeUpgrade.Price) * item.Num;
                }
                #endregion
                Model.Amount[10] = new Dictionary<decimal, decimal>() { { Model.MaterialAmount, 1.0000M } };
                #endregion

                #region 人工费
                //MaterialType = 2
                Model.TechAmount = Model.ListOrderMaterial.Where(p => p.MaterialType == 2 && p.SuitID == -1 && p.PackageID == 0 && p.IsUpgraded == 0).Sum(p => p.Price * p.Num);
                Model.Amount[11] = new Dictionary<decimal, decimal>() { { Model.TechAmount, 1.0000M } };
                #endregion

                #region 辅料费
                //MaterialType = 1
                Model.AuxiliaryAmount = Model.ListOrderMaterial.Where(p => p.MaterialType == 1 && p.SuitID == -1 && p.PackageID == 0 && p.IsUpgraded == 0).Sum(p => p.Price * p.Num);
                Model.Amount[12] = new Dictionary<decimal, decimal>() { { Model.AuxiliaryAmount, 1.0000M } };
                #endregion

                #region 包的费用
                //（基础包不收费，p.PackageType = 1,3为基础包）
                Model.PackageAmount = Model.ListOrderPackage.Where(p => p.PackageType != 1 && p.PackageType != 3).Sum(p => p.PackageAmount);
                Model.Amount[1] = new Dictionary<decimal, decimal>() { { Model.PackageAmount, 1.0000M } };
                #endregion

                #region (主材费+人工费+辅料费+包的费用) temp
                var temp = Model.MaterialAmount + Model.TechAmount + Model.AuxiliaryAmount + Model.PackageAmount;
                #endregion

                #region 远程费
                // 远程费=（主材费+人工费+辅料费+包的费用）*远程费比例；
                Model.RemoteAmount = temp * yuanchengRate;
                //if (viewModel.Amount.ContainsKey(4))
                //{//手动输入远程费率后，将已经存在的远程费记录清空
                //    viewModel.Amount.Remove(4);
                //}
                Model.Amount[4] = new Dictionary<decimal, decimal>() { { Model.RemoteAmount, yuanchengRate } };
                #endregion

                #region 管理费
                //管理费=（主材费+人工费+辅料费+包的费用）*（1+远程费比例）*管理费比例；
                Model.FeeAmount = temp * (1 + yuanchengRate) * Model.CityFee.FeeRate;
                Model.Amount[5] = new Dictionary<decimal, decimal>() { { Model.FeeAmount, Model.CityFee.FeeRate } };
                #endregion

                #region 税金
                //税金=（主材费+人工费+辅料费+包的费用）*（1+远程费比例）*（1+管理费比例）*税金比例；
                Model.TaxAmount = temp * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * Model.CityFee.TaxRate;
                Model.Amount[6] = new Dictionary<decimal, decimal>() { { Model.TaxAmount, Model.CityFee.TaxRate } };
                #endregion

                #region 折现费用
                //个性化结算暂时不涉及折现费用
                //Model.DiscountAmount = Model.ListOrderMaterial.Where(p => p.MaterialType == 0 && p.IsUpgraded == 0 && p.IsDiscounted == 1).Sum(p => p.Price * p.Num * 0.8000M);
                //Model.Amount[9] = new Dictionary<decimal, decimal>() { { Model.DiscountAmount, 1.0000M } };
                //Model.DiscountAmount = 0M;//折现费用为0
                //Model.DiscountAmountUse = 0M;
                //Model.Amount[9] = new Dictionary<decimal, decimal>() { { Model.DiscountAmount, 1.0000M } };
                #endregion

                #region 变更费用
                //变更的施工费*（1+远程费比例）*（1+管理费比例）*（1+税金比例） + 变更的主材费*（1+远程费比例）
                //Model.ChangeAmount = Model.ListOrderChangeInfo.Sum(p => p.TechAmount + p.AuxiliaryAmount) * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * (1 + Model.CityFee.TaxRate) + Model.ChangeAmount + Model.ListOrderChangeInfo.Sum(p => p.MaterialAmount);
                Model.ChangeAmount = Model.ListOrderChangeInfo.Sum(p => p.TechAmount + p.AuxiliaryAmount) * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * (1 + Model.CityFee.TaxRate) + Model.ListOrderChangeInfo.Sum(p => p.MaterialAmount) * (1 + yuanchengRate);
                Model.Amount[7] = new Dictionary<decimal, decimal>() { { Model.ChangeAmount, 1.0000M } };
                #endregion

                #region 变更税金
                //变更税金=（人工变更金额+辅料变更金额）*（1+远程费比例）*（1+管理费比例）*税金比例；
                Model.ChangeTaxAmount = Model.ListOrderChangeInfo.Sum(p => p.TechAmount + p.AuxiliaryAmount) * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * Model.CityFee.TaxRate;
                Model.Amount[8] = new Dictionary<decimal, decimal>() { { Model.ChangeTaxAmount, 1.0000M } };
                #endregion

                #region 个性化合同总金额
                //合同总金额=【（主材费+人工费+辅料费+包的费用）*（1+远程费比例）*（1+管理费比例）*（1+税金比例）】；
                Model.ContractAmount = temp * (1 + yuanchengRate) * (1 + Model.CityFee.FeeRate) * (1 + Model.CityFee.TaxRate);
                #endregion

                #region 订单总金额
                //订单总金额=合同总金额+中期变更费用+尾期变更费用+设计签约的设计费
                //201510131724更改 订单总金额需要加上设计费
                Model.OrderAmount = Model.ContractAmount + Model.ChangeAmount + Model.OrderQuoteInfo.DesignAmount;
                #endregion

            }
            #endregion
            return Model;
        }
        #endregion
    }
}