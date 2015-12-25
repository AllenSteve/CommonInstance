// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-23-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-24-2015
// ***********************************************************************
// <copyright file="BaseConstructionContractModel.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using EBS.Interface.EContract.Method;
using EBS.Interface.Model;
using System.Text;

namespace EBS.Interface.EContract.Model
{
    public class BaseConstructionContractModel : BaseContractModel
    {
        /// <summary>
        /// 合同号，这里取订单编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 业主信息
        /// </summary>
        public string PrincipalA { get; set; }
        public string AgentName { get; set; }
        public string Nation { get; set; }
        public string Residence { get; set; }
        public string IDNumber { get; set; }
        public string TelephoneA { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 承包方信息
        /// </summary>
        public string ProDesignerName { get; set; }
        public string JobTitle { get; set; }
        public string CertID { get; set; }
        public string ContactMobile { get; set; }
        public string ConstructLeader { get; set; }
        public string CLJobTitle { get; set; }
        public string CLCertID { get; set; }
        public string CLContactMobile { get; set; }

        /// <summary>
        /// 工程概况
        /// </summary>
        public string ProAddress { get; set; }
        public string DecorateArea { get; set; }
        public string ProHouseType { get; set; }
        public string ProTimeLimit { get; set; }
        public string TotalArea { get; set; }
        public string SittingRooms { get; set; }
        public string Bathrooms { get; set; }
        public string Kitchens { get; set; }
        public string Bedrooms { get; set; }
        public string Balconys { get; set; }
        public string BeyondCost { get; set; }
        public string AddItem { get; set; }
        public string ProCost { get; set; }
        public string CostAmount { get; set; }

        /// <summary>
        /// 工程款支付方式
        /// </summary>
        public string SignAmount { get; set; }
        public string KGJDAmount { get; set; }
        public string FWCQAmount { get; set; }
        public string SDYSAmount { get; set; }
        public string FSYSAmount { get; set; }
        public string WGYSAmount { get; set; }
        public string MGYSAmount { get; set; }
        public string YQYSAmount { get; set; }
        public string JGYSAmount { get; set; }

        /// <summary>
        /// 签名信息
        /// </summary>
        public string SignatureA { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Date { get; set; }

        public BaseConstructionContractModel()
            : base()
        { 
        }

        public BaseConstructionContractModel(string orderId)
            : base()
        {
            // 业主信息
            N_Order_QuoteEx OwnerInfo = base.method.GetOwnerInfo(orderId);
            // 设计师信息
            Admin_UserInfo DesignerInfo = base.method.GetDesignInfo(orderId);
            // 合同信息
            N_Order_QuoteInfo ContractInfo = base.method.GetContractInfo(orderId);
            // 监理信息
            Admin_UserInfo JianliInfo = base.method.GetJianliInfo(orderId);

            // 承包方信息
            //CityContractConfig cityContractConfig = method.GetCityContractConfig(OwnerInfo.CityID.ToString());

            ////this.ContractNo = ContractInfo.DesignContractNO;
            this.ContractNo = orderId;
            /// <summary>
            /// 业主信息
            /// </summary>
            this.PrincipalA = OwnerInfo.TrueName;
            this.AgentName = string.Empty;
            this.Nation = string.Empty;
            this.Residence = string.Empty;
            this.IDNumber = OwnerInfo.IdentityNum;
            this.TelephoneA = OwnerInfo.Mobile;
            this.Email = string.Empty;

            /// <summary>
            /// 承包方信息
            /// </summary>
            this.ProDesignerName = DesignerInfo.SoufunId == 0 ? string.Empty : DesignerInfo.TrueName; ;
            this.JobTitle = string.Empty;
            this.CertID = string.Empty;
            this.ContactMobile = DesignerInfo.SoufunId == 0 ? string.Empty : DesignerInfo.Mobile;
            this.ConstructLeader = JianliInfo.SoufunId == 0 ? string.Empty : JianliInfo.TrueName; ;
            this.CLJobTitle = string.Empty;
            this.CLCertID = string.Empty;
            this.CLContactMobile = JianliInfo.SoufunId == 0 ? string.Empty : JianliInfo.Mobile;

            /// <summary>
            /// 工程概况
            /// </summary>
            this.ProAddress = base.GetProjectAddress(OwnerInfo);
            this.DecorateArea = base.AppendAreaUnit(OwnerInfo.RealArea);
            this.ProHouseType = base.GetHouseType(orderId);
            this.ProTimeLimit = ContractInfo.PlanDayCount.ToString();
            this.TotalArea = string.Empty;
            this.SittingRooms = string.Empty;
            this.Bathrooms = string.Empty;
            this.Kitchens = string.Empty;
            this.Bedrooms = string.Empty;
            this.Balconys = string.Empty;
            this.BeyondCost = string.Empty;
            this.AddItem = string.Empty;
            this.ProCost = this.GetProjectCost(orderId, base.stringFormat);//OwnerInfo;//此处查询比较复杂
            this.CostAmount = method.GetAmountInWords(decimal.Parse(this.ProCost));

            /// <summary>
            /// 工程款支付方式
            /// </summary>
            IDictionary<string, string> dictionary = this.GetPaymentDictionary(ContractInfo, OwnerInfo, decimal.Parse(this.ProCost), base.stringFormat);
            this.SignAmount = dictionary["签约"];
            this.KGJDAmount = dictionary["开工交底"];
            this.FWCQAmount = dictionary["房屋拆改"];
            this.SDYSAmount = dictionary["水电验收"];
            this.FSYSAmount = dictionary["防水验收"];
            this.WGYSAmount = dictionary["瓦工验收"];
            this.MGYSAmount = dictionary["木工验收"];
            this.YQYSAmount = dictionary["油漆验收"];
            this.JGYSAmount = dictionary["竣工验收"];

            /// <summary>
            /// 签名信息
            /// </summary>
            //this.SignatureA = string.Empty;
            this.SetPreviewConstructionContractDate(ContractInfo);
        }

        /// <summary>
        /// 待签合同，取当前时间
        /// </summary>
        public void SetUnsignedConstructionContractDate()
        {
            this.SetConstructionContractDate(DateTime.Now);
        }

        /// <summary>
        /// 合同预览，取数据表时间
        /// </summary>
        /// <param name="ContractInfo">The contract information.</param>
        public void SetPreviewConstructionContractDate(N_Order_QuoteInfo ContractInfo)
        {
            if (ContractInfo != null && !ContractInfo.ConstructionContractingTime.Equals(DateTime.Parse("1900/1/1")))
            {
                this.SetConstructionContractDate(ContractInfo.ConstructionContractingTime);
            }
        }

        private void SetConstructionContractDate(DateTime date)
        {
            this.Year = date.Year.ToString();
            this.Month = date.Month.ToString();
            this.Date = date.Day.ToString();
        }

        private string GetProjectCost(string orderId,string stringFormat = @"f2")
        {
            string projectCost = string.Empty;
            PaymentModel payment = PaymentModel.GetPaymentList(orderId);
            projectCost = payment.ContractAmount.ToString(stringFormat);
            return string.IsNullOrEmpty(projectCost) ? decimal.Zero.ToString() : projectCost;
        }

        private IDictionary<string, string> GetPaymentDictionary(N_Order_QuoteInfo ContractInfo, N_Order_QuoteEx OwnerInfo, decimal ProCost, string stringFormat = @"f2")
        {
            List<Payment> PaymentList = base.GetPaymentList(ContractInfo, OwnerInfo, decimal.Parse(this.ProCost));
            IDictionary<string, string> dictionary = base.GetPaymentDictionary(PaymentList, stringFormat);
            return dictionary;
        }

    }
}