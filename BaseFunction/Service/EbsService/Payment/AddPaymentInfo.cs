// ***********************************************************************
// Assembly         : EBS.Service.Bussiness.PaymentService
// Author           : 仇士龙
// Created          : 02-03-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-03-2016
// ***********************************************************************
// <copyright file="AddPaymentInfo.cs" company="Microsoft">
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
using BaseFunction.ServiceInterface.IEbsServcie;
using EBS.Interface.Payment.Model;
using ComponentModels.EbsDBModel;
using EBS.Interface.EContract.Method.EBSExtension;

namespace BaseFunction.Service.EbsService
{
    public partial class PaymentService : IPayment
    {
        /// <summary>
        /// 解析支付Model信息，更新支付信息到数据库
        /// </summary>
        /// <param name="paymentInfo">The payment information.</param>
        /// <returns>System.Int32.</returns>
        protected int AddPaymentInfo(PaymentInfo paymentInfo)
        {
            try
            {
                 // 解析支付信息
                Order_PayNote orderPayNote = this.CreateOrderPayNoteByPayment(paymentInfo);
                // 发送插入到数据库的记录邮件
                if(this.isDebugMode)
                {
                    this.debug.WriteMail("插入到数据库的记录");
                    this.debug.WriteMail(orderPayNote);
                }
                // 将支付信息记录到数据库
                return this.WriteConnection.Execute(PaymentService.SQL.Insert<Order_PayNote>(), orderPayNote);
            }
            catch(Exception ex)
            {
                // 返回插入异常标记
                return -1;
            }
        }

        /// <summary>
        /// 根据支付信息创建数据库记录信息
        /// </summary>
        /// <param name="paymentInfo">支付信息</param>
        /// <returns>数据库记录对象</returns>
        private Order_PayNote CreateOrderPayNoteByPayment(PaymentInfo paymentInfo)
        {
            return new Order_PayNote() 
            {
                OrderID = paymentInfo.OrderInfo,
                PayTypeID = paymentInfo.PayType,
                PayTypeName = ((EBS.BLL.EnumBLL.PayNote_PayType)this.paymentInfo.PayType).ToString(),
                PayName = paymentInfo.PayName,
                PayAmount = paymentInfo.PaymentAmount,
                NewPayAmount = paymentInfo.PaymentAmount,
                ShigongStageID = paymentInfo.NodeType,
                ShigongStageName = paymentInfo.NodeName,
                PayDesc = paymentInfo.PayName.Contains("首期收款") ? "发起首期收款" : string.Empty,
                PayMobile = paymentInfo.PayMobile,
                Createtime = DateTime.Now,
                AddAmount = 0,
                GjSoufunID = 0L,
                GjRealName = string.Empty,
                OpIp = string.Empty,
                PayState = -1,
                RealPayTime = DateTime.Parse("1900/01/01"),
                IsDel = 0,
                IsLastPay = this.IsLastPay(paymentInfo.OrderInfo, paymentInfo.NodeType) == true ? 1 : 0,
                chargetype = 0,
                RealPayAmount =0,
                //timestamp 时间戳字段不用管自动生成
                DingJinID=0,
                DingJinAmount =0,
                ConsumeTime=DateTime.Parse("1900/01/01"),
                BackAmount =0,
                DesignFeeAmount = paymentInfo.DesignFeeAmount
            };
        }
    }
}
