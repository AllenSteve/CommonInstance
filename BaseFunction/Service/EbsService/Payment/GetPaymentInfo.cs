// ***********************************************************************
// Assembly         : EBS.Service.Bussiness.PaymentService
// Author           : 仇士龙
// Created          : 02-03-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-03-2016
// ***********************************************************************
// <copyright file="GetPaymentInfo.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseFunction.ServiceInterface.IEbsServcie;
using EBS.Interface.Payment.Model;
using EBS.Service.Enum;
using ComponentModels.EbsDBModel;
using EBS.Interface.EContract.Model;
using Dapper;
using EBS.Interface.EContract.Method.EBSExtension;

namespace BaseFunction.Service.EbsService
{
    public partial class PaymentService : IPayment
    {
        /// <summary>
        /// 获取当前节点支付信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>  
        /// <param name="quoteType">报价类型</param>
        /// <returns>PaymentInfo.</returns>
        protected PaymentInfo GetPaymentInfo(string orderId, int nodeType, int payType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            try
            {
                N_Payment_SchemeItem paymentItem = this.paymentNodeList.FirstOrDefault(o => o.OrderStateCode == nodeType);
                this.paymentInfo.PayMobile = this.GetOwnerPhone(orderId);
                this.paymentInfo.ContractName = "";
                this.paymentInfo.NodeName = paymentItem == null ? string.Empty : paymentItem.OrderStateName;
                this.paymentInfo.PayName = this.IsSignContractNode(nodeType) ? "首期收款" : ((NodeStatus)nodeType).ToString();
                this.paymentInfo.PayStatus = (int)PaymentStatus.发起支付成功;

                // 获取支付信息（四舍五入前）
                if (this.isDebugMode)
                {
                    this.debug.WriteMail("获取支付信息（四舍五入前）");
                    this.debug.WriteMail(this.paymentInfo);
                }


                if(!this.IsLastPay(orderId,nodeType,quoteType))
                {
                        this.paymentInfo.PaymentAmount = this.GetRoundPayAmount(this.paymentInfo.PaymentAmount);
                }

                // 获取支付信息（四舍五入后）
                if (this.isDebugMode)
                {
                    this.debug.WriteMail("获取支付信息（四舍五入后）");
                    this.debug.WriteMail(this.paymentInfo);
                }

                return this.paymentInfo;
            }
            catch(Exception ex)
            {
                this.paymentInfo.PayStatus = (int)PaymentStatus.获取当前节点支付信息失败不发起支付;
                return this.paymentInfo;
            }
        }

        /// <summary>
        /// 根据订单号获取业主手机号
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>业主手机号</returns>
        protected string GetOwnerPhone(string orderId)
        {
            try
            {
                object conditionParam = new { OrderID = orderId };
                object columnParam = new { Phone = 0 };
                string phone = this.ReadConnection.ExecuteScalar<string>(SQL.Query<N_Order_Base>(conditionParam, columnParam));
                return phone;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public IEnumerable<N_Payment_SchemeItem> GetOrderPaymentNodeList(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            IEnumerable<N_Payment_SchemeItem> nodeList = this.ReadConnection.Query<N_Payment_SchemeItem>(SQL.QueryNodeList(orderId));
            IEnumerable<ConstructAtaccepnce_InfoSortDetails> nodeSortList = this.GetPayNodeSortList(orderId, quoteType);
            if (nodeSortList.Count() > 0)
            {
                for (int index = 1; index < nodeList.Count(); ++index)
                {
                    nodeList.ElementAt(index).Sort = nodeSortList.First(o => o.StateCode == nodeList.ElementAt(index).OrderStateCode).Sort + 1;
                }
            }
            return nodeList;
        }

        protected IEnumerable<Order_PayNote> GetPayNoteList(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            object conditionParam = new { OrderID = orderId, IsDel = 0 };
            IEnumerable<Order_PayNote> payNoteList = this.ReadConnection.Query<Order_PayNote>(SQL.Query<Order_PayNote>(conditionParam));
            return payNoteList;
        }

        /// <summary>
        /// 计算已付金额
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="quoteType">订单报价</param>
        /// <returns>已付金额</returns>
        protected decimal GetPaidAmount(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            // 获取该订单下已支付的记录
            object conditionParam = new { OrderId = orderId, IsDel = 0 };
            object columnParam = new { Paid = 0 };
            decimal paidAmount = this.ReadConnection.ExecuteScalar<decimal>(SQL.Query<N_Order_Operation>(conditionParam, columnParam));
            return paidAmount;
        }

        /// <summary>
        /// 计算从起始节点到当期节点的费用之和+变更费用
        /// 1：应付金额 = 设计费+施工费（乘以截止至当前节点的费率百分比之和）
        /// 2：变更费用 = 合同金额-设计费-施工费
        /// 原始公式：应付金额 + 变更费用
        /// 简化公式：合同金额-（1-截止至当前节点的费率百分比之和）施工费
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">当前节点类型</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>当前节点发起的支付金额</returns>
        protected decimal GetNodeTotalAmount(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            decimal contractAmount = this.GetContractAmount(orderId);
            decimal constructionAmount = this.GetConstructionContractAmount(orderId);
            decimal totalRate = this.GetTotalRate(orderId, nodeType, quoteType);
            if (this.IsSignContractNode(nodeType))
            {
                this.paymentInfo.DesignFeeAmount =  this.GetDesignContractAmount(orderId);
            }
            // 自定义订单，第一个非签约收款节点收取设计费
            else if (this.IsCustomContract(quoteType) && this.IsFirstNotSignContractPayNode(orderId,nodeType,quoteType))
            {
                this.paymentInfo.DesignFeeAmount = this.GetDesignContractAmount(orderId);
            }
            return contractAmount - (1 - totalRate) * constructionAmount;
        }

        /// <summary>
        /// 获取合同金额
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>合同金额</returns>
        protected decimal GetContractAmount(string orderId)
        {
            object conditionParam = new { OrderId = orderId, IsDel = 0 };
            object columnParam = new { Amount = 0 };
            decimal amount = this.ReadConnection.ExecuteScalar<decimal>(SQL.Query<N_Order_Operation>(conditionParam, columnParam));
            return amount;
        }

        /// <summary>
        /// 获取设计合同金额
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>合同金额</returns>
        protected decimal GetDesignContractAmount(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            object conditionParam = new { OrderId = orderId, IsDel = 0 };
            object columnParam = new { DesignAmount = 0 };
            decimal designAmount = this.ReadConnection.ExecuteScalar<decimal>(SQL.Query<N_Order_QuoteInfo>(conditionParam, columnParam));
            return designAmount;
        }

        /// <summary>
        /// 获取施工合同金额
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>合同金额</returns>
        protected decimal GetConstructionContractAmount(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            try
            {
                AmountCalculationModel paymentAmount = new AmountCalculationModel(orderId, false);
                return paymentAmount.ContractAmount;
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException("ErrorMessage", "查询施工合同金额失败");
            }
        }

        /// <summary>
        /// 当前节点支付状态
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">当前节点类型</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>支付状态</returns>
        protected int GetNodePayState(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            object conditionParam = new { OrderId = orderId, ShigongStageID = nodeType, IsDel = 0 };
            object columnParam = new { PayState = 0 };
            int state = this.ReadConnection.ExecuteScalar<int>(SQL.Query<Order_PayNote>(conditionParam, columnParam));
            return state;
        }

        /// <summary>
        /// 支付金额取整
        /// </summary>
        /// <param name="payAmount">The pay amount.</param>
        /// <returns>System.Decimal.</returns>
        protected decimal GetRoundPayAmount(decimal payAmount)
        {
            return decimal.Ceiling(payAmount * 100) / 100M;
        }

        protected IEnumerable<ConstructAtaccepnce_InfoSortDetails> GetPayNodeSortList(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            IEnumerable<ConstructAtaccepnce_InfoSortDetails> nodeList = this.ReadConnection.Query<ConstructAtaccepnce_InfoSortDetails>(SQL.QueryNodeSortList(orderId));
            return nodeList;
        }

        protected decimal GetTotalRate(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            decimal totalRate = 0M;
            if(this.paymentNodeList.Any(o=>o.Sort>0))
            {
                int nodeSort = this.paymentNodeList.First(o => o.OrderStateCode == nodeType).Sort;
                totalRate = this.paymentNodeList.Where(o => o.Sort <= nodeSort).Sum(o => o.Rate);
            }
            else
            {
                totalRate = this.paymentNodeList.Where(o => o.OrderStateCode <= nodeType).Sum(o => o.Rate);
            }
            return totalRate;
        }
    }
}
