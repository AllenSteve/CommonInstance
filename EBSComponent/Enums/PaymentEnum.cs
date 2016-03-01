// ***********************************************************************
// Assembly         : EBS.Service.Model.PaymentModel
// Author           : 仇士龙
// Created          : 02-02-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-02-2016
// ***********************************************************************
// <copyright file="PaymentStatus.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBS.Service.Enum
{
    /// <summary>
    /// 发起支付流程返回状态信息
    /// </summary>
    public enum PaymentStatus
    {
        发起支付成功 = 100,
        系统异常未知原因 = -1,
        存在未支付成功的前向节点不发起支付 = 1,
        当前节点应付金额小于已付金额不发起支付 = 2,
        当前节点应付金额等于已付金额不发起支付 = 3,
        获取当前节点支付信息失败不发起支付 = 4,
        当前节点支付金额写入记录失败不发起支付 = 5,
        符合发起节点支付流程验证条件 = 6,
        参数校验失败不发起支付 = 7,
        订单报价查询失败不发起支付 = 8,
        当前节点费率为零不发起支付 = 9,
        节点类型与支付类型不匹配不发起支付 = 10,
        当前节点已支付不发起支付 = 11
    }

    public enum OrderQuoteType
    {
        订单报价查询失败 = -1,
        未报价 = 0,
        V1报价套餐=1,
        V1报价自定义=2,
        V2报价套餐=3,
        V2报价自定义=4
    }

    public enum NodeStatus
    {
        签约 = 20001,
        开工交底 = 30010,
        房屋拆改 = 30020,
        水电验收 = 30030,
        防水验收 = 30035,
        瓦工验收 = 30040,
        木工验收 = 30045,
        油漆验收 = 30050,
        竣工验收 = 30060
    }
}
