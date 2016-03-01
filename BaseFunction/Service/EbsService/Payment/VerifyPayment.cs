// ***********************************************************************
// Assembly         : EBS.Service.Bussiness.PaymentService
// Author           : 仇士龙
// Created          : 02-03-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-03-2016
// ***********************************************************************
// <copyright file="VerfiyPayment.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Web;
using BaseFunction.ServiceInterface.IEbsServcie;
using EBS.Service.Enum;
using ComponentModels.EbsDBModel;
using EBS.Interface.EContract.Method.EBSExtension;

namespace BaseFunction.Service.EbsService
{
    public partial class PaymentService : IPayment
    {
        /// <summary>
        /// 根据订单号获取订单报价类型
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns>报价类型</returns>
        protected OrderQuoteType GetQuoteType(string orderId)
        {
            try
            {     
                OrderQuoteType quoteType = OrderQuoteType.未报价;
                object conditionParam = new { OrderId = orderId, IsDel = 0 };
                string query = SQL.Query<N_Order_Operation>(conditionParam);
                N_Order_Operation record = this.ReadConnection.Query<N_Order_Operation>(query)
                                                                                              .FirstOrDefault();

                if (record == null || record.ID <= 0)
                {
                    quoteType = OrderQuoteType.订单报价查询失败;
                }
                else if (record.QuoteMark == 0)
                {
                    quoteType = OrderQuoteType.未报价;
                }
                else if (record.QuoteMark == 1 && record.IsSuit > 0)
                {
                    quoteType = OrderQuoteType.V1报价套餐;
                }
                else if (record.QuoteMark == 1 && record.IsSuit == 0)
                {
                    quoteType = OrderQuoteType.V1报价自定义;
                }
                else if (record.QuoteMark == 2 && record.IsSuit > 0)
                {
                    quoteType = OrderQuoteType.V2报价套餐;
                }
                else if (record.QuoteMark == 2 && record.IsSuit == 0)
                {
                    quoteType = OrderQuoteType.V2报价自定义;
                }
                else
                {
                    quoteType = OrderQuoteType.未报价;
                }
                return quoteType;
            }
            catch (Exception ex)
            {
                return OrderQuoteType.订单报价查询失败;
            }
        }

        /// <summary>
        /// 验证是否符合发起条件
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>
        /// <returns>发起状态</returns>
        public int IsQualified(string orderId, int nodeType, int payType)
        {
            try
            {
                int status = (int)PaymentStatus.符合发起节点支付流程验证条件;
                this.paymentInfo.OrderInfo = orderId;
                this.paymentInfo.NodeType = nodeType;
                this.paymentInfo.PayType = payType;

                // 01-获取订单报价类型，检查订单报价
                OrderQuoteType quoteType = this.GetQuoteType(orderId); 
                // 02-根据订单获取节点列表
                this.paymentNodeList = this.GetOrderPaymentNodeList(orderId, (int)quoteType);
                if (quoteType == OrderQuoteType.订单报价查询失败 || quoteType == OrderQuoteType.未报价)
                {
                    status = (int)PaymentStatus.订单报价查询失败不发起支付;
                }
                else if (this.paymentNodeList == null || this.paymentNodeList.Count() == 0 || this.paymentNodeList.Count(o=>o.OrderStateCode==nodeType)==0)
                {
                    status = (int)PaymentStatus.获取当前节点支付信息失败不发起支付;
                }
                // 03-获取当前节点费率
                // 自定义套餐签约费率为零时不发起支付（单独发起）在此处进行判断
                else if (this.IsRateZeroNode(orderId, nodeType, (int)quoteType))
                {
                    status = (int)PaymentStatus.当前节点费率为零不发起支付;
                }
                // 自定义订单在签约节点不发起支付
                else if (!this.IsSuitContract((int)quoteType) && this.IsSignContractNode(nodeType))
                {
                    status = (int)PaymentStatus.节点类型与支付类型不匹配不发起支付;
                }
                // 判断支付类型是否为节点支付
                else if (!IsNotePayment(payType))
                {
                    status = (int)PaymentStatus.节点类型与支付类型不匹配不发起支付;
                }
                // 04-获取前向节点支付状态-注意排除当前节点为签约节点（首节点）的情况
                else if (!this.IsForewordNodePaid(orderId, nodeType, (int)quoteType))
                {
                    status = (int)PaymentStatus.存在未支付成功的前向节点不发起支付;
                }
                else if (this.IsNodePaid(orderId, nodeType, (int)quoteType))
                {
                    status = (int)PaymentStatus.当前节点已支付不发起支付;
                }
                else
                {
                    // TODO:计算应付金额与已付金额的差值并验证是否符合条件
                    // 01-应付金额
                    decimal Amount = this.GetNodeTotalAmount(orderId, nodeType, (int)quoteType);
                    // 02-已付金额
                    decimal PaidAmount = this.GetPaidAmount(orderId, (int)quoteType);
                    if (Amount < PaidAmount)
                    {
                        status = (int)PaymentStatus.当前节点应付金额小于已付金额不发起支付;
                    }
                    else if (Amount == PaidAmount)
                    {
                        status = (int)PaymentStatus.当前节点应付金额等于已付金额不发起支付;
                    }
                    else
                    {
                        // CHECK条件，TODO:
                        // 1.上一节点支付状态为【支付成功】
                        // 2.发起支付条件：（应付金额-已付金额）>0，
                        //    应付金额为（当期节点费用+变更所需费用）
                        // 3.若自定义签约时金额为0，设计费为0，
                        //    则不自动发起支付，由服务人员填写金额支付
                        this.paymentInfo.Amount = Amount;
                        this.paymentInfo.PaidAmount = PaidAmount;
                        this.paymentInfo.PaymentAmount = Amount - PaidAmount;
                        status = (int)PaymentStatus.符合发起节点支付流程验证条件;
                    }
                }
                return this.paymentInfo.PayStatus = status;
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("查询施工合同金额失败"))
                {
                    return this.paymentInfo.PayStatus = (int)PaymentStatus.获取当前节点支付信息失败不发起支付;
                }
                return this.paymentInfo.PayStatus = (int)PaymentStatus.系统异常未知原因;
            }
        }

        /// <summary>
        /// 检查校验信息，校验失败直接返回，不发起支付
        /// </summary>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验类型</param>
        /// <returns>检查结果</returns>
        protected bool IsSignChecked(string sign, int signType)
        {
            bool checkRet = true;
            if (checkRet==true)
            {
                string flag = HttpContext.Current.Items["IsTestUser"].ToString();
                if(!string.IsNullOrEmpty(flag) && flag.Equals("1"))
                {
                    this.read_connection = PaymentService.CreateConnection(2);
                    this.write_connection = PaymentService.CreateConnection(3);
                    this.isDebugMode = true;
                }
                else
                {
                    this.isDebugMode = false;
                }
            }
            else
            {
                this.paymentInfo.PayStatus = (int)PaymentStatus.参数校验失败不发起支付;
            }
            return checkRet;
        }

        /// <summary>
        /// 根据施工节点判断是否是最后一个支付节点
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="nodeType">施工节点ID与N_Payment_SchemeItem中的OrderStateCode对应</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>是否是最后一个支付节点</returns>
        private bool IsLastPay(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            try
            {
                // 获取订单下的节点列表中的最后一个节点
                N_Payment_SchemeItem lastNode = this.paymentNodeList.LastOrDefault();
                // OrderStateCode约束为不可空，所以不必对该字段加验证
                bool isLastPayNode = lastNode == null ? false : lastNode.OrderStateCode == nodeType;
                return isLastPayNode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断上一节点是否已经支付
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="nodeType">节点ID</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>是否支付</returns>
        protected bool IsForewordNodePaid(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            // 获取该订单下已支付的记录
            IEnumerable<Order_PayNote> payNodeList = this.GetPayNoteList(orderId, quoteType);
            // 判断前向节点是否均为已支付状态，true：前向节点均已支付，false：前向节点均未支付
            bool isPaid = payNodeList.Count(o => o.PayState != 1) == 0;
            return isPaid;
        }

        /// <summary>
        /// 判断当前节点是否零费率
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">当前节点类型</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>零费率</returns>
        protected bool IsRateZeroNode(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            // 获取费率大于0的节点列表
            IEnumerable<N_Payment_SchemeItem> nodeList = this.paymentNodeList.Where(o => o.Rate > decimal.Zero);
            // 判断当前节点是否在列表当中，true：当前节点费率为0，false：当前节点费率大于0
            bool isRateZero = nodeList.Count(o=>o.OrderStateCode==nodeType)==0;
            return isRateZero;
        }

        /// <summary>
        /// 判断是否是签约节点-是否为首节点
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        protected bool IsSignContractNode(int nodeType)
        {
            return nodeType == 20001;
        }

        /// <summary>
        /// 判断是否为套餐合同
        /// </summary>
        /// <param name="quoteType">报价类型</param>
        /// <returns>是否为套餐合同</returns>
        protected bool IsSuitContract(int quoteType)
        {
            return (quoteType == (int)OrderQuoteType.V1报价套餐) || (quoteType == (int)OrderQuoteType.V2报价套餐);
        }

        /// <summary>
        /// 判断支付类型是否为节点支付
        /// </summary>
        /// <param name="payType">Type of the pay.</param>
        /// <returns><c>true</c> if [is note payment] [the specified pay type]; otherwise, <c>false</c>.</returns>
        protected bool IsNotePayment(int payType)
        {
            return payType == (int)EBS.BLL.EnumBLL.PayNote_PayType.节点支付;
        }

        /// <summary>
        /// 当前节点是否已经支付
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">当前节点类型</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>是否已支付</returns>
        protected bool IsNodePaid(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            int paystate = this.GetNodePayState(orderId,nodeType,quoteType);
            return paystate == 1;
        }

         /// <summary>
        /// 判断是否为自定义
        /// </summary>
        /// <param name="quoteType">报价类型</param>
        /// <returns>是否为自定义</returns>
        protected bool IsCustomContract(int quoteType)
        {
            return (quoteType == (int)OrderQuoteType.V1报价自定义) || (quoteType == (int)OrderQuoteType.V2报价自定义);
        }

        /// <summary>
        /// 判断是否为第一个非签约支付节点
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">当前节点类型</param>
        /// <param name="quoteType">订单报价类型</param>
        /// <returns>是否已支付</returns>
        protected bool IsFirstNotSignContractPayNode(string orderId, int nodeType, int quoteType = (int)OrderQuoteType.V2报价套餐)
        {
            IEnumerable<N_Payment_SchemeItem> payNodeList = this.GetOrderPaymentNodeList(orderId, quoteType);
            N_Payment_SchemeItem node = payNodeList.First(o => !this.IsSignContractNode(o.OrderStateCode) && o.Rate > decimal.Zero);
            return node.OrderStateCode==nodeType;
        }
    }
}
