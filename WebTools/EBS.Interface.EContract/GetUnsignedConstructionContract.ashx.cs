﻿// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-22-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-22-2015
// ***********************************************************************
// <copyright file="GetUnsignedConstructionContract.ashx.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBS.Interface.EContract
{
    /// <summary>
    /// GetUnsignedConstructionContract 的摘要说明
    /// </summary>
    public class GetUnsignedConstructionContract : IHttpHandler
    {
        private BaseMethod method = new BaseMethod();

        private BaseConstructionContractModel contract { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            // OrderID，SoufunID
            string orderId = context.Request["OrderID"];
            string soufunId = context.Request["SoufunID"];

            // 获取合同模板Id
            string templateId = method.GetContractTemplateID("北京施工（4.0）");
            if (!string.IsNullOrEmpty(templateId) && !string.IsNullOrEmpty(orderId))
            {
                // 解析模板
                string html = method.GetTemplateHtml(templateId);
                // 初始化参数
                contract = new BaseConstructionContractModel(orderId);
                // 变更签名参数
                contract.SignatureA = contract.GetUnsignedContractSignature();
                // 变更日期参数
                contract.SetUnsignedConstructionContractDate();
                // 删除印章标签
                html = method.RemoveStampImage(html);
                // 删除打印标签
                html = method.RemovePrintImage(html);
                // 附加待签设置脚本
                html = method.AppendUnsignedDesignContract(html);
                // 注意放在最后去进行参数替换
                html = method.ReplaceHtmlWithModel(contract, html);
                // 隐藏在页面上隐藏签字标签
                html = method.HideUnsignedContractSignatureTag(html);
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