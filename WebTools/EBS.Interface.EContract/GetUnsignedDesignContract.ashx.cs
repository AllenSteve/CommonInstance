// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-22-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-23-2015
// ***********************************************************************
// <copyright file="GetUnsignedDesignContract.ashx.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EBS.Interface.EContract.Method;
using EBS.Interface.EContract.Model;

namespace EBS.Interface.EContract
{
    /// <summary>
    /// 获取待签设计合同的摘要说明
    /// </summary>
    public class GetUnsignedDesignContract : IHttpHandler
    {
        private BaseMethod method = new BaseMethod();

        private BaseDesignContractModel contract { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            // OrderID，SoufunID
            string orderId = context.Request["OrderID"];
            string soufunId = context.Request["SoufunID"];
            int isSandbox = FunLayer.Transform.Int(context.Request["isSandbox"]);
            if (isSandbox == 1)
            {
                if (HttpContext.Current.Items["IsTestUser"] != null)
                {
                    HttpContext.Current.Items["IsTestUser"] = 1;
                }
                else
                {
                    HttpContext.Current.Items.Add("IsTestUser", 1);
                }
            }
            // 获取合同模板Id
            string templateId = method.GetContractTemplateID(orderId,(int)EBS.BLL.EnumBLL.ContractType.设计合同);
            // 获取印章Id
            //string stampId = method.GetStampId(templateId);

            if (!string.IsNullOrEmpty(templateId)&&!string.IsNullOrEmpty(orderId))
            {
                // 解析模板
                string html = method.GetTemplateHtml(templateId);
                // 初始化参数
                contract = new BaseDesignContractModel(orderId);
                // 变更签名参数
                contract.SignatureA = contract.GetUnsignedContractSignature();
                // 变更日期参数
                contract.SetUnsignedDesignContractDate();
                // 删除印章标签
                html = method.RemoveStampImage(html);
                // 删除打印标签
                html = method.RemovePrintImage(html);
                // 附加待签设置脚本
                html = method.AppendUnsignedDesignContract(html);
                // 注意放在最后去进行参数替换
                html = method.ReplaceHtmlWithModel(contract, html);
                // 输出模板
                method.WriteHtml(context, html);
                //method.WritePlainText(context, html);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}