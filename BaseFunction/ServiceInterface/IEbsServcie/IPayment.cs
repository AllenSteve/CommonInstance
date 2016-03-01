// ***********************************************************************
// Assembly         : EBS.Service.IPayment
// Author           : 仇士龙
// Created          : 02-02-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-02-2016
// ***********************************************************************
// <copyright file="IPayment.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBS.Interface.Payment.Model;
using ComponentModels.EbsDBModel;
using EBS.Service.Enum;

namespace BaseFunction.ServiceInterface.IEbsServcie
{
    /// <summary>
    /// 支付接口定义
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// 发起验收节点支付流程
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验信息类型</param>
        /// <returns>发起支付信息对象</returns>
        PaymentInfo CreatePaymentNode(string orderId, int nodeType, int payType, string sign, int signType);

        /// <summary>
        /// 发起定金支付流程
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="payType">发起的支付类型</param>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验信息类型</param>
        /// <returns>发起支付信息对象</returns>
        PaymentInfo CreateMarginPayment(string orderId, int payType, string sign, int signType);

        /// <summary>
        /// 验证是否符合发起条件
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>
        /// <returns>发起状态</returns>
        int IsQualified(string orderId, int nodeType, int payType);

        /// <summary>
        /// 模拟发起验收节点支付流程
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="nodeType">验收节点类型</param>
        /// <param name="payType">发起的支付类型</param>
        /// <param name="sign">校验信息</param>
        /// <param name="signType">校验信息类型</param>
        /// <returns>发起支付信息对象</returns>
        PaymentInfo CreateMockPaymentNode(string orderId, int nodeType, int payType, string sign, int signType);

        IEnumerable<N_Payment_SchemeItem> GetOrderPaymentNodeList(string orderId, int quoteType = (int)OrderQuoteType.V2报价套餐);
    }
}
