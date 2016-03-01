// ***********************************************************************
// Assembly         : EBS.Service.Model.PaymentModel
// Author           : 仇士龙
// Created          : 02-02-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-02-2016
// ***********************************************************************
// <copyright file="PaymentInfo.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBS.Interface.BaseService.Model;
using EBS.Service.Enum;

namespace EBS.Interface.Payment.Model
{
    /// <summary>
    /// 支付信息Model类
    /// </summary>
    public class PaymentInfo : BaseServiceModel
    {
        private int payStatus { get; set; }

        /// <summary>
        /// 订单信息（订单号）
        /// </summary>
        /// <value>The order information.</value>
        public string OrderInfo { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        /// <value>The paid amount.</value>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 需付款
        /// </summary>
        /// <value>The payment amount.</value>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// 施工合同名称
        /// </summary>
        /// <value>The name of the construction contract.</value>
        public string ContractName { get; set; }

        /// <summary>
        /// 支付节点名称
        /// </summary>
        /// <value>The name of the construction node.</value>
        public string NodeName { get; set; }

        /// <summary>
        /// 支付发起状态
        /// </summary>
        /// <value>The pay status.</value>
        public int PayStatus
        {
            get
            {
                return this.payStatus;
            }

            set
            {
                this.payStatus = value;
                this.Description = ((PaymentStatus)this.PayStatus).ToString();

                if (this.payStatus == (int)PaymentStatus.参数校验失败不发起支付)
                {
                    this.Status = (int)ServiceStatus.参数有误;
                }
                else if (this.payStatus == (int)PaymentStatus.发起支付成功)
                {
                    this.Status = (int)ServiceStatus.请求成功;
                }
                else
                {
                    this.Status = (int)ServiceStatus.内部异常;
                }
                this.Message = ((ServiceStatus)this.Status).ToString();
            }
        }

        /// <summary>
        /// 支付类型
        /// </summary>
        /// <value>The type of the pay.</value>
        public int PayType { get; set; }

        /// <summary>
        /// 支付类型名称
        /// </summary>
        /// <value>The name of the pay.</value>
        public string PayName { get; set; }

        /// <summary>
        /// 支付节点类型
        /// </summary>
        /// <value>The type of the node.</value>
        public int NodeType { get; set; }

        /// <summary>
        /// 业主手机号
        /// </summary>
        /// <value>The pay mobile.</value>
        public string PayMobile { get; set; }

        /// <summary>
        /// 设计费
        /// </summary>
        /// <value>The design fee amount.</value>
        public decimal DesignFeeAmount { get; set; }
    }
}
