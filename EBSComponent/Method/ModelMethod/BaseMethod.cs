// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-22-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-22-2015
// ***********************************************************************
// <copyright file="BaseMethod.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EBS.Interface.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using EBS.Interface.EContract.Model;

namespace EBS.Interface.EContract.Method
{
    public class BaseMethod
    {
        public BaseMethod()
        { 
        }

        /// <summary>
        /// 获取合同模板ID
        /// </summary>
        /// <param name="key">合同模板名称</param>
        /// <returns>合同模板ID</returns>
        public string GetContractTemplateID(string key)
        {
            IDictionary<string,string> dict = new Dictionary<string,string>();
            dict.Add("北京施工（4.0）", "108dacc9-7f22-46ea-8e11-3f10eb2986e6");
            dict.Add("北京设计", "9b5a9f60-1cb2-4211-b194-729e1369fd66");

            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return string.Empty;
        }

        public N_Order_QuoteEx GetOwnerInfo(string orderId)
        {
            N_Order_QuoteEx ret = EBS.Interface.Data.DBOper.N_Order_QuoteEx.Get("IsDel=0 and OrderID=@orderid", "CreateTime desc", new object[] { orderId });
            return ret;
        }

        public Admin_UserInfo GetDesignInfo(string orderId)
        {
            // 设计师soufunId
            long DesignerSoufunID = EBS.Interface.Data.DBOper.N_Order_Service.Get("IsDel=0 and OrderID=@orderid and FunctionID=2", "CreateTime", new object[] { orderId }).SoufunId;
            // 设计师信息
            Admin_UserInfo DesignInfo = EBS.Interface.Data.DBOper.Admin_UserInfo.Get("IsDel=0 and Status=1 and SoufunID=@soufunid", "CreateTime", new object[] { DesignerSoufunID });

            return DesignInfo;
        }

        public N_Order_QuoteInfo GetContractInfo(string orderId)
        {
            N_Order_QuoteInfo ContractInfo = EBS.Interface.Data.DBOper.N_Order_QuoteInfo.Get("IsDel=0 and OrderID=@orderid", "CreateTime", new object[] { orderId });
            return ContractInfo;
        }

        /// <summary>
        /// 将金额改为大写
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public string GetAmountInWords(decimal Amount = 0.0m)
        {
            string money = Microsoft.International.Formatters.InternationalNumericFormatter.FormatWithCulture("Lc", Amount, null, new System.Globalization.CultureInfo("zh-CHS"));
            try
            {
                string[] SpMoney = money.Split('点');
                if (SpMoney.Length == 1)
                {
                    if (SpMoney[0] != "零")
                    {
                        return money + "元整";
                    }
                    else
                    {
                        return "零元整";
                    }
                }
                else// if (SpMoney.Length == 2)
                {
                    string m = "";
                    if (SpMoney[0] != "零")
                    {
                        m += SpMoney[0] + "元";
                    }
                    string Jiao = SpMoney[1].Substring(0, 1);
                    string Fen = SpMoney[1].Length == 1 ? "零" : SpMoney[1].Substring(1, 1);
                    if (Jiao != "零")
                    {
                        m += Jiao + "角";
                    }
                    if (Fen != "零")
                    {
                        m += Fen + "分";
                    }
                    return m;
                }
            }
            catch
            {
                return money;
            }
        }

        public string GetTemplateHtml(string templateId)
        {
            string interfaceURL = "http://pact.light.fang.com/pactModel/getPactModelContentByModelId.do";
            string param = "modelId=" + templateId;
            Encoding encoding = Encoding.GetEncoding("utf-8");
            var a = EBS.Common.Common.RequestInterface(interfaceURL, param, false, encoding);
            var b = JsonConvert.DeserializeAnonymousType(a, new { data = "" });
            string html = b.ToString();
            int startIndex = html.IndexOf("﻿<!DOCTYPE")+1;
            return html.Substring(startIndex, html.LastIndexOf('}')-startIndex);
        }

        public IDictionary<string, string> GetParam(string html)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            string[] array = Regex.Split(html, "#<", RegexOptions.IgnoreCase);
            for (int index = 0; index < array.Length - 1; ++index)
            {
                array[index] = Regex.Split(array[index], "#", RegexOptions.IgnoreCase).Last();
                if (!dict.ContainsKey(array[index]))
                {
                    dict.Add(array[index], array[index]);
                }
            }
            return dict;
        }

        public string ReplaceHtmlWithDictionary(IDictionary<string, string> dictionary, string html)
        {
            string replaceStr = string.Empty;
            foreach (var item in dictionary)
            {
                replaceStr = string.Format("#{0}#", item.Key);
                html = html.Replace(replaceStr, item.Value);
            }
            return html;
        }

        public string RemovePrintImage(string html)
        {
            string replaceStr = @"<img src=""http://img2.soufunimg.com/home/ebs/images/icon49.png"" style=""position: absolute;
        right: 10%; top: 30px; cursor: pointer;"" />";
            return html.Replace(replaceStr, string.Empty);
        }

        public string GetImgBase64(string url, string param)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            hwr = (HttpWebRequest)WebRequest.Create(url + "?" + param);

            hwr.Timeout = 120000;

            hwr.Method = "get";
            hwr.ContentType = "";
            HttpWebResponse res = (HttpWebResponse)hwr.GetResponse();
            Stream stream = res.GetResponseStream();
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return Convert.ToBase64String(ms.ToArray());
        }

        public CityContractLog GetCityContractLog(string orderId)
        {
            CityContractLog cityContractLog = EBS.Interface.Data.DBOper.CityContractLog.Get("IsDel=0 AND SignatureUrl<>'' AND ContractType=0 AND OrderId=@OrderId", "", new object[] { orderId });
            return cityContractLog;
        }

        public List<N_Order_Room> GetRoomList(string orderId)
        {
            List<N_Order_Room> roomList = EBS.Interface.Data.DBOper.N_Order_Room.GetList(" IsDel=0 and OrderID=@orderID ", "", new object[] { orderId }, true);
            return roomList;
        }

        public string GetBaseImage(CityContractLog cityContractLog)
        {
            string downloadURL = ConfigurationManager.AppSettings["ESignature_DownloadFileURL"] ?? "http://interface.ebs.home.fang.com/ESignature/DownloadFile.ashx";
            string binaryImg = string.Empty;
            string imgSection = string.Empty;
            if ((cityContractLog != null || cityContractLog.ID > 0) && !string.IsNullOrEmpty(cityContractLog.SignatureUrl))
            {
                binaryImg = this.GetImgBase64(downloadURL, "downloadUrl=" + cityContractLog.SignatureUrl + "&extension=png");
                imgSection = string.Format(@"<img src=""{0}"">", binaryImg);
            }
            return imgSection;
        }

        public void WriteHtml(HttpContext context, string html)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write(html);
        }

        public void WritePlainText(HttpContext context, string html)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(html);
        }

        public string GetStampLogoURL(string orderId)
        {
            List<ORM.Base.QueryResult> result = new EBS.Interface.Model.N_Order_Base().INNER_JOIN(EBS.Interface.Model.Order_Form.TableInfo, "OF1", "SRCTAB.OrderID=OF1.OrderID and SRCTAB.IsDel=0 and OF1.IsDel=0").Where("SRCTAB.OrderID=@orderid", new object[] { orderId }).List();
            CityQuoteStamp stamp = null;
            if (result != null && result.Count > 0)
            {
                N_Order_Base orderBase = result.First().Get<EBS.Interface.Model.N_Order_Base>();
                stamp = EBS.Interface.Data.DBOper.CityQuoteStamp.Get(" Isdel=0 and CityId=@CityId", " CreateTime Desc", new object[] { orderBase.CityId }, true);
                return string.IsNullOrEmpty(stamp.StampLogoUrl)?string.Empty:stamp.StampLogoUrl;
            }
            return string.Empty;
        }

        public string ReplaceHtmlWithModel(BaseContractModel designContractModel, string html)
        {
            IDictionary<string, string> dictionary = designContractModel.ToDictionary();
            return this.ReplaceHtmlWithDictionary(dictionary, html);
        }

        public string RemoveStampImage(string html)
        {
            string imageSection = "<img src=#ImgUrl#  id=\"SealImg\" style=\" width:160px;position:absolute;left:70px;top:-10px;display:none;\">";
            return html.Replace(imageSection, string.Empty);
        }

        public string ShowStampImage(string html)
        {
            string imageSectionHide = "<img src=#ImgUrl#  id=\"SealImg\" style=\" width:160px;position:absolute;left:70px;top:-10px;display:none;\">";
            string imageSectionShow = "<img src=#ImgUrl#  id=\"SealImg\" style=\" width:160px;position:absolute;left:70px;top:-10px;\">";
            return html.Replace(imageSectionHide, imageSectionShow);
        }

        public string AppendStampLogo(string html, string orderId)
        {
            BaseMethod method = new BaseMethod();
            html = this.ShowStampImage(html);
            string stampURL = "\"" + method.GetStampLogoURL(orderId) + "\"";
            string replaceStr = @"#ImgUrl#";
            return html.Replace(replaceStr, stampURL);
        }

        public string AppendUnsignedDesignContract(string html)
        {
            string appendStr = "<script type=\"text/javascript\"> function ReMoveKeyWord(kw) { var temp = document.getElementById(kw).innerText.replace(\"#\", \"\").replace(\"#\", \"\").trim(); if (temp == kw) { try { androidsignature.executeSignature(kw); } catch (e) { iphonesingnature(kw);}} } function iphonesingnature(log) { window.location.href = \"hm://clickSign/\" + log; }</script>";
            return new StringBuilder(html).Append(appendStr).ToString();
        }

        public Admin_UserInfo GetJianliInfo(string orderId)
        {
            long JianliSoufunID = EBS.Interface.Data.DBOper.N_Order_Service.Get("IsDel=0 and OrderID=@orderid and FunctionID=4", "CreateTime", new object[] { orderId }).SoufunId;
            Admin_UserInfo JianliInfo = EBS.Interface.Data.DBOper.Admin_UserInfo.Get("IsDel=0 and Status=1 and SoufunID=@soufunid", "CreateTime", new object[] { JianliSoufunID });
            return JianliInfo;
        }

        public CityContractConfig GetCityContractConfig(string cityId)
        {
            CityContractConfig cityContractConfig = EBS.Interface.Data.DBOper.CityContractConfig.Get(" Isdel=0 and CityId=@CityId", " CreateTime Desc", new object[] { cityId }, true);
            return cityContractConfig;
        }

        public string HideUnsignedContractSignatureTag(string html)
        {
            string replaceStr = "<b style=\"color:#FFF\">#jiafangqianzi#</b>";
            return html.Replace("<b>#jiafangqianzi#</b>", replaceStr);
        }
        
    }
    
}