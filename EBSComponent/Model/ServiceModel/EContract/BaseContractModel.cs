// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-24-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-24-2015
// ***********************************************************************
// <copyright file="ContractModel.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EBS.Interface.EContract.Method;
using System.Reflection;
using System.Text;
using EBS.Interface.Model;

namespace EBS.Interface.EContract.Model
{
    public class BaseContractModel
    {
        protected BaseMethod method { get; set; }

        protected string stringFormat { get; set; }

        public BaseContractModel()
        {
            this.method = new BaseMethod();
            this.stringFormat = @"f2";
        }

        public IDictionary<string, string> ToDictionary()
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties = type.GetProperties();
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            object value = null;
            for (int i = 0; i < fields.Length; ++i)
            {
                value = fields[i].GetValue(this);
                if (value != null && !string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    dictionary.Add(properties[i].Name, value.ToString());
                }
                else
                {
                    dictionary.Add(properties[i].Name, string.Empty);
                }
            }
            return dictionary;
        }

        public string GetUnsignedContractSignature()
        {
            return "<span id=\"jiafangqianzi\" onclick=\"ReMoveKeyWord('jiafangqianzi')\"><a href=\"javascript:void(0)\" style=\"color:white;height:100px; display:inline-block;line-height:100px;width:100px;\"><b>#jiafangqianzi#</b></a></span>";
        }

        protected string GetHouseType(string orderId)
        {
            List<N_Order_Room> roomList = this.method.GetRoomList(orderId);
            StringBuilder sb = new StringBuilder();
            foreach (var item in roomList.GroupBy(p => p.RoomID))
            {
                var firstElement = item.FirstOrDefault();
                sb.Append(firstElement.RoomNum);
                sb.Append(firstElement.RoomName);
            }
            return sb.ToString();
        }

        public string GetPreviewContractSignature(string orderId)
        {
            CityContractLog cityContractLog = this.method.GetCityContractLog(orderId);
            return method.GetBaseImage(cityContractLog);
        }

        protected virtual string GetProjectAddress(N_Order_QuoteEx OwnerInfo)
        {
            StringBuilder address = new StringBuilder();
            address.Append(OwnerInfo.CityName);
            address.Append(OwnerInfo.DistrictName);
            address.Append(OwnerInfo.EstateName);

            if (!string.IsNullOrEmpty(OwnerInfo.BuildingNO))
            {
                address.Append(OwnerInfo.BuildingNO);
                address.Append("号楼");
            }

            if (!string.IsNullOrEmpty(OwnerInfo.UnitNO))
            {
                address.Append(OwnerInfo.UnitNO);
                address.Append("单元");
            }

            address.Append(OwnerInfo.RoomNO);
            return address.ToString();
        }

        protected string GetContractValidity(N_Order_QuoteInfo ContractInfo)
        {
            DateTime endDate = ContractInfo.DesignContractingTime.Year == 1900 ? DateTime.Parse("1900-01-01") : ContractInfo.DesignContractingTime.AddMonths(2);
            StringBuilder ContractValidity = new StringBuilder();
            ContractValidity.Append(ContractInfo.DesignContractingTime.ToLongDateString());
            ContractValidity.Append("至");
            ContractValidity.Append(endDate.ToLongDateString());
            return ContractValidity.ToString();
        }

        protected IDictionary<string, string> GetPaymentDictionary(List<Payment> paymentList, string stringFormat = @"f2")
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var payment in paymentList)
            {
                if (!dictionary.ContainsKey(payment.StateName))
                {
                    dictionary.Add(payment.StateName, payment.Money.ToString(stringFormat));
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 获取工程款列表
        /// </summary>
        protected List<Payment> GetPaymentList(N_Order_QuoteInfo ContractInfo, N_Order_QuoteEx OwnerInfo, decimal ProCost)
        {
            List<Payment> PaymentList = new List<Payment>();

            string sqlWhere = " and SRCTAB.IsDel=0 and psd.IsDel=0 and SRCTAB.SchemeCategory=1 and SRCTAB.status=1 and psi.IsDel=0  ";
            if (ContractInfo.SuitID == 0)
            {
                sqlWhere += " and SRCTAB.SchemeType=1";
            }
            if (ContractInfo.SuitID > 0)
            {
                sqlWhere += " and SRCTAB.SchemeType=0";
            }

            List<ORM.Base.QueryResult> qres = new List<ORM.Base.QueryResult>();
            EBS.Interface.Model.N_Payment_SchemeInfo psi = new Interface.Model.N_Payment_SchemeInfo();
            qres = psi.LEFT_JOIN(EBS.Interface.Model.N_Payment_SchemeDetail.TableInfo, "psd", "SRCTAB.id = psd.SchemeId").LEFT_JOIN(EBS.Interface.Model.N_Payment_SchemeItem.TableInfo, "psi", "psd.ID = psi.DetailId").Where("SRCTAB.CityID=@cityid " + sqlWhere, new object[] { OwnerInfo.CityID }).List("psi.OrderStateCode,psi.sort asc");
            //若无该城市收款配置，则使用全国收款配置  cityid=0
            if (qres.Count <= 0)
            {
                psi = new Interface.Model.N_Payment_SchemeInfo();
                qres = new List<ORM.Base.QueryResult>();
                qres = psi.LEFT_JOIN(EBS.Interface.Model.N_Payment_SchemeDetail.TableInfo, "psd", "SRCTAB.id = psd.SchemeId ").LEFT_JOIN(EBS.Interface.Model.N_Payment_SchemeItem.TableInfo, "psi", "psd.ID = psi.DetailId").Where("SRCTAB.CityID=@cityid " + sqlWhere, new object[] { 0 }).List("psi.OrderStateCode,psi.sort asc");
            }
            foreach (var item in qres)
            {
                var psiinfo = item.Get<EBS.Interface.Model.N_Payment_SchemeItem>();
                PaymentList.Add(new Payment()
                {
                    StateCode = psiinfo.OrderStateCode,
                    StateName = psiinfo.OrderStateName,
                    Rate = psiinfo.Rate,
                    Money = ProCost * psiinfo.Rate
                });
            }
            return PaymentList;
        }
    }
}