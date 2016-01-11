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
        //后添加，zjl
        public string AgentMobile { get; set; }

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

        // 木工施工
        public string MGSGAmount { get; set; }
        // 尾期施工
        public string WQSGAmount { get; set; }

        /// <summary>
        /// 签名信息
        /// </summary>
        public string SignatureA { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Date { get; set; }
        public string EYear { get; set; }
        public string EMonth { get; set; }
        public string EDate { get; set; }
        public string SignatureB { get; set; }

        // 2016/1/4
        public string DesignerName { get; set; }

        public string DesignerMobile { get; set; }

        public string PlanStartBuildDate { get; set; }

        public string PlanEndBuildDate { get; set; }

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

            //AmountCalculationModel paymentAmount = new AmountCalculationModel(orderId, false);

            // 承包方信息
            //CityContractConfig cityContractConfig = method.GetCityContractConfig(OwnerInfo.CityID.ToString());

            this.ContractNo = ContractInfo.ConstructContractNO;
            //this.ContractNo = orderId;
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
            this.AgentMobile = string.Empty;

            /// <summary>
            /// 承包方信息
            /// </summary>
            this.ProDesignerName = DesignerInfo.SoufunId == 0 ? string.Empty : DesignerInfo.TrueName;
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
            IDictionary<string, decimal> dictionary = this.GetPaymentDictionary(ContractInfo, OwnerInfo, decimal.Parse(this.ProCost), base.stringFormat);
            this.SignAmount = this.CheckDictionary(dictionary,"签约");
            this.KGJDAmount = this.CheckDictionary(dictionary, "开工交底");
            this.FWCQAmount = this.CheckDictionary(dictionary, "房屋拆改");
            this.SDYSAmount = this.CheckDictionary(dictionary, "水电验收"); 
            this.FSYSAmount = this.CheckDictionary(dictionary, "防水验收"); 
            this.WGYSAmount = this.CheckDictionary(dictionary, "瓦工验收"); 
            this.MGYSAmount = this.CheckDictionary(dictionary, "木工验收"); 
            this.YQYSAmount = this.CheckDictionary(dictionary, "油漆验收");
            // 竣工验收
            this.JGYSAmount = this.GetLastAmount(orderId,dictionary);
            this.MGSGAmount = this.CheckDictionary(dictionary, "木工施工");
            this.WQSGAmount = this.CheckDictionary(dictionary, "尾期施工");

            /// <summary>
            /// 签名信息
            /// </summary>
            //this.SignatureA = string.Empty;
            this.SignatureB = DesignerInfo.SoufunId == 0 ? string.Empty : DesignerInfo.TrueName;
            this.SetPreviewConstructionContractDate(ContractInfo);

            this.DesignerName = DesignerInfo.SoufunId == 0 ? string.Empty : DesignerInfo.TrueName;
            this.DesignerMobile = DesignerInfo.SoufunId == 0 ? string.Empty : DesignerInfo.Mobile;
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

        private string GetProjectCost(string orderId,string stringFormat = @"f2")
        {
            string projectCost = string.Empty;
            PaymentModel payment = PaymentModel.GetPaymentList(orderId);
            projectCost = payment.ContractAmount.ToString(stringFormat);

            //AmountCalculationModel paymentAmount = new AmountCalculationModel(orderId, false);
            //projectCost = paymentAmount.ContractAmount.ToString(stringFormat);
            return string.IsNullOrEmpty(projectCost) ? decimal.Zero.ToString() : projectCost;
        }

        private IDictionary<string, decimal> GetPaymentDictionary(N_Order_QuoteInfo ContractInfo, N_Order_QuoteEx OwnerInfo, decimal ProCost, string stringFormat = @"f2")
        {
            List<Payment> PaymentList = base.GetPaymentList(ContractInfo, OwnerInfo, decimal.Parse(this.ProCost));
            IDictionary<string, decimal> dictionary = base.GetPaymentDictionary(PaymentList);
            return dictionary;
        }

        private string CheckDictionary(IDictionary<string, decimal> dictionary, string key, string stringFormat = @"f2")
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key].ToString(stringFormat);
            }
            return @"0.00";
        }

        private string GetLastAmount(string orderId,IDictionary<string, decimal> dictionary, string key ="竣工验收")
        {
            if (dictionary.ContainsKey(key))
            {
                decimal amount = decimal.Parse(this.GetProjectCost(orderId,@"f6"));
                decimal sum = dictionary.Where(o=>!o.Key.Equals(key)).Sum(d=>d.Value);
                return (amount-sum).ToString(base.stringFormat);
            }
            return @"0.00";
        }
    }
}