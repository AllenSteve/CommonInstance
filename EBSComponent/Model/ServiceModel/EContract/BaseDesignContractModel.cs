// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-22-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-24-2015
// ***********************************************************************
// <copyright file="BaseDesignContractModel.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using EBS.Interface.Model;
using EBS.Interface.EContract.Method;
using System.Text;
using System.Configuration;

namespace EBS.Interface.EContract.Model
{
    /// <summary>
    /// 设计合同模板基类
    /// </summary>
    public class BaseDesignContractModel : BaseContractModel
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 业主信息
        /// </summary>
        public string PrincipalA { get; set; }
        public string TelephoneA { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        
        /// <summary>
        /// 设计师信息
        /// </summary>
        public string DesignerName { get; set; }
        public string DesignerMobile { get; set; }
        public string ShopTelephone { get; set; }
        public string DesignerEmail { get; set; }
        
        /// <summary>
        /// 项目信息
        /// </summary>
        public string Project { get; set; }
        public string ProjectAddress { get; set; }
        public string HouseType { get; set; }
        public string Area { get; set; }
        public string DesignCycle { get; set; }
        public string DesignFees { get; set; }
        public string RmbPower { get; set; }
        public string RmbLower { get; set; }
        public string ContractValidity { get; set; }
        public string SignatureA { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Date { get; set; }
        public string SignatureB { get; set; }
        public string EYear { get; set; }
        public string EMonth { get; set; }
        public string EDate { get; set; }

        public string OrderNo { get; set; }

        public string PlanStartBuildDate { get; set; }

        public string PlanEndBuildDate { get; set; }

        public BaseDesignContractModel()
            : base()
        { 
        }

        public BaseDesignContractModel(string orderId)
            : base()
        {
            // 业主信息
            N_Order_QuoteEx OwnerInfo = base.method.GetOwnerInfo(orderId);
            // 设计师信息
            Admin_UserInfo DesignInfo = base.method.GetDesignInfo(orderId);
            // 合同信息
            N_Order_QuoteInfo ContractInfo = base.method.GetContractInfo(orderId);

            this.ContractNo = ContractInfo.DesignContractNO;
            //this.ContractNo = orderId; 
            // 业主信息
            this.PrincipalA = OwnerInfo.TrueName;
            this.TelephoneA = string.Empty;
            this.Mobile = OwnerInfo.Mobile;
            this.Address = string.Empty;
            this.Email = string.Empty;

             // 设计师信息
            this.DesignerName = DesignInfo.SoufunId == 0 ? string.Empty : DesignInfo.TrueName;
            this.DesignerMobile = DesignInfo.SoufunId == 0 ? string.Empty : DesignInfo.Mobile;
            this.ShopTelephone = string.Empty;
            this.DesignerEmail = DesignInfo.SoufunId == 0 ? string.Empty : string.Empty;// DesignInfo.EMail;

            // 项目信息
            this.Project = OwnerInfo.EstateName;
            this.ProjectAddress = this.GetProjectAddress(OwnerInfo);
            this.HouseType = base.GetHouseType(orderId);
            this.Area = OwnerInfo.RealArea.ToString(base.stringFormat);
            this.DesignCycle = ContractInfo.DesignCycle.ToString(base.stringFormat);
            this.DesignFees = ContractInfo.DesignAmount.ToString(base.stringFormat);
            this.RmbPower = method.GetAmountInWords(ContractInfo.DesignAmount);
            this.RmbLower = ContractInfo.DesignAmount.ToString(base.stringFormat);
            this.ContractValidity = base.GetContractValidity(ContractInfo);

            // 签名信息
            //this.SignatureA = this.GetBaseImage(cityContractLog);
            this.SignatureB = DesignInfo.SoufunId == 0 ? string.Empty : DesignInfo.TrueName;
            // 设置时间信息
            this.SetPreviewDesignContractDate(ContractInfo);

            this.OrderNo = ContractInfo.DesignContractNO;

        }

        protected override string GetProjectAddress(N_Order_QuoteEx OwnerInfo)
        {
            string address=base.GetProjectAddress(OwnerInfo) + OwnerInfo.RemarkAddress;
            return address;
        }

        /// <summary>
        /// 待签合同，取当前时间
        /// </summary>
        public void SetUnsignedDesignContractDate()
        {
            this.SetDesignContractDate(DateTime.Now);
        }

        /// <summary>
        /// 合同预览，取数据表时间
        /// </summary>
        /// <param name="ContractInfo">The contract information.</param>
        public void SetPreviewDesignContractDate(N_Order_QuoteInfo ContractInfo)
        {
            if (ContractInfo != null && !ContractInfo.ConstructionContractingTime.Equals(DateTime.Parse("1900/1/1")))
            {
                this.SetDesignContractDate(ContractInfo.ConstructionContractingTime);
            }
        }

        private void SetDesignContractDate(DateTime date)
        {
            DateTime endDate = date.AddMonths(2);
            this.Year = date.Year.ToString();
            this.Month = date.Month.ToString();
            this.Date = date.Day.ToString();
            this.EYear = endDate.Year.ToString();
            this.EMonth = endDate.Month.ToString();
            this.EDate = endDate.Day.ToString();
            this.PlanStartBuildDate = date.ToLongDateString();
            this.PlanEndBuildDate = date.AddMonths(2).ToLongDateString();
        }
    }
}