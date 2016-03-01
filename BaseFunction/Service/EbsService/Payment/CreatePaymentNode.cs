// ***********************************************************************
// Assembly         : EBS.Service.Bussiness.PaymentService
// Author           : 仇士龙
// Created          : 02-03-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-03-2016
// ***********************************************************************
// <copyright file="CreatePayment.cs" company="Microsoft">
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

namespace BaseFunction.Service.EbsService
{
    public partial class PaymentService : IPayment
    {
        /// <summary>
        /// 发起支付流程
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验信息类型</param>
        /// <returns>发起支付信息对象</returns>
        public PaymentInfo CreatePaymentNode(string orderId, int nodeType, int payType, string sign, int signType)
        {
            try
            {
                // 验证校验信息
                if (this.IsSignChecked(sign, signType))
                {
                    if (this.isDebugMode)
                    {
                        this.debug.WriteMail("参数列表", string.Format("orderId={0},nodeType={1},payType={2},sign={3},signType={4}", orderId, nodeType, payType, sign, signType));
                    }

                    // 验证是否符合发起支付条件
                    int qualified = this.IsQualified(orderId, nodeType, payType);

                    if (qualified == (int)PaymentStatus.符合发起节点支付流程验证条件)
                    {
                        // 获取当前节点支付信息
                        this.paymentInfo = this.GetPaymentInfo(orderId, nodeType, payType);

                        // 判断获取当前节点支付信息是否成功
                        if (this.paymentInfo.PayStatus != (int)PaymentStatus.获取当前节点支付信息失败不发起支付)
                        {
                            // 当前节点支付信息获取成功，记录该信息到数据库
                            if (this.AddPaymentInfo(this.paymentInfo) == -1)
                            {
                                this.paymentInfo.PayStatus = (int)PaymentStatus.当前节点支付金额写入记录失败不发起支付;
                            }
                        }
                    }
                }
                
                this.paymentInfo.TimeConsuming = DateTime.Now - this.startTime;

                if (this.isDebugMode)
                {
                    this.debug.WriteMail("返回结果");
                    this.debug.WriteMail(this.paymentInfo);
                }
               
                return this.paymentInfo;
            }
            catch(Exception ex)
            {
                // 由PayStatus值的变化引起的返回Status值的变化应写在属性当中，不应在程序中出现。
                this.paymentInfo.PayStatus = (int)PaymentStatus.系统异常未知原因;
                this.paymentInfo.TimeConsuming = DateTime.Now - this.startTime;
                if (this.isDebugMode)
                {
                    this.debug.WriteMail("返回结果");
                    this.debug.WriteMail(this.paymentInfo);
                }

                return this.paymentInfo;
            }
            finally
            {
                if (this.isDebugMode)
                {
                    this.debug.WriteMail("网站地址", System.Web.HttpContext.Current.Request.Url.OriginalString);
                    this.debug.SendEmail("跟踪信息");
                }
            }
        }


        /// <summary>
        /// 发起定金支付流程
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="payType">发起的支付类型</param>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验信息类型</param>
        /// <returns>发起支付信息对象</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public PaymentInfo CreateMarginPayment(string orderId, int payType, string sign, int signType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 模拟发起验收节点支付流程
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验信息类型</param>
        /// <returns>发起支付信息对象</returns>
        public PaymentInfo CreateMockPaymentNode(string orderId, int nodeType, int payType, string sign, int signType)
        {
            try
            {
                // 验证校验信息
                if (this.IsSignChecked(sign, signType))
                {
                    this.paymentInfo.OrderInfo = orderId;
                    this.paymentInfo.NodeType = nodeType;
                    this.paymentInfo.PayType = payType;
                    if (this.IsSignContractNode(nodeType))
                    {
                        this.paymentInfo.PaymentAmount = 1000M;
                    }
                    else
                    {
                        this.paymentInfo.PaymentAmount = 10000M;
                    }

                    this.paymentInfo.PayStatus = (int)PaymentStatus.发起支付成功;
                }
                this.paymentInfo.TimeConsuming = DateTime.Now - this.startTime;
                return this.paymentInfo;
            }
            catch(Exception ex)
            {
                // 由PayStatus值的变化引起的返回Status值的变化应写在属性当中，不应在程序中出现。
                this.paymentInfo.PayStatus = (int)PaymentStatus.系统异常未知原因;
                this.paymentInfo.TimeConsuming = DateTime.Now - this.startTime;
                return this.paymentInfo;
            }
        }
    }
}
