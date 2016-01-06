// ***********************************************************************
// Assembly         : EBS.Interface.EContract
// Author           : 仇士龙
// Created          : 12-22-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-28-2015
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
        /// 根据订单号和合同类型获取模板Id
        /// </summary>
        /// <param name="key">订单号</param>
        /// <param name="contractType">合同类型：0-施工合同，1-设计合同</param>
        /// <returns>模板Id</returns>
        public string GetContractTemplateID(string key,int contractType=0)
        {
            //IDictionary<string,string> dict = new Dictionary<string,string>();
            //dict.Add("北京施工（4.0）", "108dacc9-7f22-46ea-8e11-3f10eb2986e6");
            //dict.Add("北京设计", "7a586e28-0b11-407c-bd4c-9f5a112993a4");

            //if (dict.ContainsKey(key))
            //{
            //    return dict[key];
            //}
            //return string.Empty;
            // 根据合同名称获取模板Id的方法
            //return this.GetTemplateIdByContractName(key);
            return this.GetTemplateIdByOrderId(key,contractType);
        }

        /// <summary>
        /// 根据订单号获取业主信息
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>N_Order_QuoteEx.</returns>
        public N_Order_QuoteEx GetOwnerInfo(string orderId)
        {
            N_Order_QuoteEx ret = EBS.Interface.Data.DBOper.N_Order_QuoteEx.Get("IsDel=0 and OrderID=@orderid", "CreateTime desc", new object[] { orderId });
            return ret;
        }

        /// <summary>
        /// 根据订单号获取设计师信息
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>Admin_UserInfo.</returns>
        public Admin_UserInfo GetDesignInfo(string orderId)
        {
            // 设计师soufunId
            long DesignerSoufunID = EBS.Interface.Data.DBOper.N_Order_Service.Get("IsDel=0 and OrderID=@orderid and FunctionID=2", "CreateTime", new object[] { orderId }).SoufunId;
            // 设计师信息
            Admin_UserInfo DesignInfo = EBS.Interface.Data.DBOper.Admin_UserInfo.Get("IsDel=0 and Status=1 and SoufunID=@soufunid", "CreateTime", new object[] { DesignerSoufunID });

            return DesignInfo;
        }

        /// <summary>
        /// 根据订单号获取联系人信息
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>N_Order_QuoteInfo.</returns>
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

        /// <summary>
        /// 根据模板Id获取模板html代码
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="encode">页面编码方式</param>
        /// <returns>模板html代码</returns>
        public string GetTemplateHtml(string templateId, string encode = "utf-8")
        {
            string interfaceURL = "http://pact.light.fang.com/pactModel/getPactModelContentByModelId.do";
            string param = "modelId=" + templateId;
            Encoding encoding = Encoding.GetEncoding(encode);
            var a = EBS.Common.Common.RequestInterface(interfaceURL, param, false, encoding);
            var b = JsonConvert.DeserializeAnonymousType(a, new { data = "" });
            string html = b.ToString();
            int startIndex = html.IndexOf("﻿<!DOCTYPE")+1;
            return html.Substring(startIndex, html.LastIndexOf('}')-startIndex);
        }

        /// <summary>
        /// 获取模板页面参数列表
        /// 有些情况不太好用，所以该方法进攻参考，
        /// 如果模板最终确定下来，可以通过调整解析方法最终实现准确的解析
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>参数列表</returns>
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

        /// <summary>
        /// 使用参数数值替换模板完成合同展示
        /// </summary>
        /// <param name="dictionary">由模板类生成的参数列表</param>
        /// <param name="html">合同Html页面</param>
        /// <returns>替换之后的Html页面</returns>
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

        /// <summary>
        /// 删除打印机图标
        /// </summary>
        /// <param name="html">页面代码</param>
        /// <returns>去掉打印机标签的页面代码</returns>
        public string RemovePrintImage(string html)
        {
            //html = this.RemoveNewPrintImage(html);
            //html = this.HidePrintImage(html);
            //string replaceStr = @"<img src=""http://img2.soufunimg.com/home/ebs/images/icon49.png"" style=""position: absolute;right: 10%; top: 30px; cursor: pointer;display:none;"" />";
            //html = html.Replace(replaceStr, string.Empty);
            return html.Replace("http://img2.soufunimg.com/home/ebs/images/icon49.png",string.Empty);
        }

        /// <summary>
        /// 获取签名图片的Base64位的字符串
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="param">请求参数包括签名图片下载地址</param>
        /// <returns>返回Base64格式的图片字符串</returns>
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

        /// <summary>
        /// 根据订单号获取合同日志信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>合同日志记录</returns>
        public CityContractLog GetCityContractLog(string orderId)
        {
            CityContractLog cityContractLog = EBS.Interface.Data.DBOper.CityContractLog.Get("IsDel=0 AND SignatureUrl<>'' AND ContractType=0 AND OrderId=@OrderId", "", new object[] { orderId });
            return cityContractLog;
        }

        /// <summary>
        /// 根据订单号获取房屋列表（用于计算房型）
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>房屋列表</returns>
        public List<N_Order_Room> GetRoomList(string orderId)
        {
            List<N_Order_Room> roomList = EBS.Interface.Data.DBOper.N_Order_Room.GetList(" IsDel=0 and OrderID=@orderID ", "", new object[] { orderId }, true);
            return roomList;
        }

        /// <summary>
        /// 根据合同日志记录获取Base64格式的图片地址
        /// </summary>
        /// <param name="cityContractLog">合同日志信息</param>
        /// <returns>Base64格式的图片地址</returns>
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

        /// <summary>
        /// 已Html的形式输出页面到浏览器
        /// </summary>
        /// <param name="context">Http上下文对象</param>
        /// <param name="html">页面代码</param>
        public void WriteHtml(HttpContext context, string html)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write(html);
        }

        /// <summary>
        /// 已文字的形式输出页面到浏览器
        /// </summary>
        /// <param name="context">Http上下文对象</param>
        /// <param name="html">页面代码</param>
        public void WritePlainText(HttpContext context, string html)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(html);
        }

        /// <summary>
        /// 根据订单号获取印章图片地址
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>印章图片地址</returns>
        public string GetStampLogoURL(string orderId)
        {
            N_Order_Base orderBase = EBS.Interface.Data.DBOper.N_Order_Base.Get(" Isdel=0 and OrderID=@OrderID", " CreateTime Desc", new object[] { orderId }, true);
            CityQuoteStamp stamp = null;
            if (orderBase != null && orderBase.CityId > 0)
            {
                stamp = EBS.Interface.Data.DBOper.CityQuoteStamp.Get(" Isdel=0 and CityId=@CityId", " CreateTime Desc", new object[] { orderBase.CityId }, true);
                return string.IsNullOrEmpty(stamp.StampLogoUrl)?string.Empty:stamp.StampLogoUrl;
            }
            return string.Empty;
        }

        /// <summary>
        /// 以合同模型对象替换页面关键字
        /// </summary>
        /// <param name="designContractModel">合同模型</param>
        /// <param name="html">页面代码</param>
        /// <returns>替换后的页面代码</returns>
        public string ReplaceHtmlWithModel(BaseContractModel designContractModel, string html)
        {
            IDictionary<string, string> dictionary = designContractModel.ToDictionary();
            return this.ReplaceHtmlWithDictionary(dictionary, html);
        }

        /// <summary>
        /// 去掉未签名的合同页面上的印章标签
        /// </summary>
        /// <param name="html">合同页面代码</param>
        /// <returns>去掉印章标签的合同页面代码</returns>
        public string RemoveStampImage(string html)
        {
            string imageSection = "<img src=#ImgUrl# id=\"SealImg\" style=\"width:160px;position:absolute;left:12%;top:-40px;display:none;\">";
            html = html.Replace(imageSection, string.Empty);
            html = this.AppendStampFlag(html);
            return html;
        }

        /// <summary>
        /// 显示印章图片
        /// </summary>
        /// <param name="html">隐藏印章图片的页面代码</param>
        /// <returns>显示印章图片的页面代码</returns>
        public string ShowStampImage(string html)
        {
            string imageSectionHide = "<img src=#ImgUrl# id=\"SealImg\" style=\"width:160px;position:absolute;left:12%;top:-40px;display:none;\">";
            string imageSectionShow = "<img src=#ImgUrl# id=\"SealImg\" style=\"width:160px;position:absolute;left:12%;top:-40px;\">";
            return html.Replace(imageSectionHide, imageSectionShow);
        }

        /// <summary>
        /// 附加印章图片到页面代码
        /// </summary>
        /// <param name="html">合同页面代码</param>
        /// <param name="orderId">订单号</param>
        /// <returns>附加印章图片后的页面代码</returns>
        public string AppendStampLogo(string html, string orderId)
        {
            BaseMethod method = new BaseMethod();
            html = this.ShowStampImage(html);
            string stampURL = "\"" + method.GetStampLogoURL(orderId) + "\"";
            string replaceStr = @"#ImgUrl#";
            return html.Replace(replaceStr, stampURL);
        }

        /// <summary>
        /// 附加脚本信息到未签名的设计合同
        /// </summary>
        /// <param name="html">页面代码</param>
        /// <returns>附加脚本后的页面代码</returns>
        public string AppendUnsignedDesignContract(string html)
        {
            string appendStr = "<script type=\"text/javascript\"> function ReMoveKeyWord(kw) { var temp = document.getElementById(kw).innerText.replace(\"#\", \"\").replace(\"#\", \"\").trim(); if (temp == kw) { try { androidsignature.executeSignature(kw); } catch (e) { iphonesingnature(kw);}} } function iphonesingnature(log) { window.location.href = \"hm://clickSign/\" + log; }</script>";
            return new StringBuilder(html).Append(appendStr).ToString();
        }

        /// <summary>
        /// 根据订单号获取监理人信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>监理人信息</returns>
        public Admin_UserInfo GetJianliInfo(string orderId)
        {
            long JianliSoufunID = EBS.Interface.Data.DBOper.N_Order_Service.Get("IsDel=0 and OrderID=@orderid and FunctionID=4", "CreateTime", new object[] { orderId }).SoufunId;
            Admin_UserInfo JianliInfo = EBS.Interface.Data.DBOper.Admin_UserInfo.Get("IsDel=0 and Status=1 and SoufunID=@soufunid", "CreateTime", new object[] { JianliSoufunID });
            return JianliInfo;
        }

        /// <summary>
        /// 根据城市号获取城市公司信息
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <returns>城市公司信息</returns>
        public CityContractConfig GetCityContractConfig(string cityId)
        {
            CityContractConfig cityContractConfig = EBS.Interface.Data.DBOper.CityContractConfig.Get(" Isdel=0 and CityId=@CityId", " CreateTime Desc", new object[] { cityId }, true);
            return cityContractConfig;
        }

        /// <summary>
        /// 隐藏未签名的施工合同签字标签
        /// </summary>
        /// <param name="html">隐藏前的页面代码</param>
        /// <returns>隐藏后的页面代码</returns>
        public string HideUnsignedContractSignatureTag(string html)
        {
            string replaceStr = "<b style=\"color:#FFF\">#jiafangqianzi#</b>";
            return html.Replace("<b>#jiafangqianzi#</b>", replaceStr);
        }

        /// <summary>
        /// 根据模板Id获取印章Id
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="encode">页面编码方式</param>
        /// <returns>印章Id</returns>
        public string GetStampId(string templateId, string encode = "utf-8")
        {
            string interfaceURL = "http://pact.light.fang.com/signature/getSignatureID.do";
            string param = "modelId=" + templateId;
            Encoding encoding = Encoding.GetEncoding(encode);
            string signatureId = string.Empty;
            string jsonStr = EBS.Common.Common.RequestInterface(interfaceURL, param, false, encoding);
            var dictionary = this.GetJsonContentDictionary(jsonStr, ':');
            if (dictionary.ContainsKey("signatureId"))
            {
                signatureId = dictionary["signatureId"];
            }
            return signatureId;
        }

        /// <summary>
        /// 将获取的json字符串转换为参数字典形式，方便参数获取
        /// </summary>
        /// <param name="jsonStrt">json字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>参数字典</returns>
        private IDictionary<string, string> GetJsonContentDictionary(string jsonStrt, params char[] separator)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] content = jsonStrt.Split(',');
            foreach (var item in content)
            {
                string[] nodeItem = item.Split(separator);
                string key = nodeItem[0].Substring(nodeItem[0].IndexOf("\"")).Replace("\"",string.Empty);
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, nodeItem[1].Replace("\"",string.Empty));
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 根据合同名获取合同模板Id
        /// </summary>
        /// <param name="contractName">合同名</param>
        /// <param name="encode">页面编码方式</param>
        /// <returns>合同模板Id</returns>
        private string GetTemplateIdByContractName(string contractName, string encode = "utf-8")
        {
            string interfaceURL = "http://pact.light.fang.com/pactModel/getModelIdByModelName.do";
            string param = "modelName=" + contractName;
            Encoding encoding = Encoding.GetEncoding(encode);
            string jsonStr = EBS.Common.Common.RequestInterface(interfaceURL, param, false, encoding);
            string modelId = string.Empty;
            var dictionary = this.GetJsonContentDictionary(jsonStr, ':');
            if (dictionary.ContainsKey("modelId"))
            {
               modelId = dictionary["modelId"];
            }
            return modelId;
        }

        /// <summary>
        /// 移除新格式模板的打印图片
        /// </summary>
        /// <param name="html">页面代码</param>
        /// <returns>移除打印图片后的页面代码</returns>
        private string RemoveNewPrintImage(string html)
        {
            string replaceStr = "<img src=\"http://img2.soufunimg.com/home/ebs/images/icon49.png\" style=\"position: absolute;right: 10%; top: 30px; cursor: pointer;\" />";
            return html.Replace(replaceStr, string.Empty);
        }

        /// <summary>
        /// 调整签字标签位置
        /// </summary>
        /// <param name="html">页面代码</param>
        /// <returns>调整后的代码</returns>
        private string AdjustSignaturePosition(string html)
        {
            string oldStyle = "color:white;";
            string newStyle = "margin-top:-22px;color:white;";
            return html.Replace(oldStyle,newStyle);
        }

        /// <summary>
        /// 根据订单号获取城市名称
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>System.String.</returns>
        public string GetContractCityName(string orderId)
        {
            N_Order_Base orderBase = EBS.Interface.Data.DBOper.N_Order_Base.Get("IsDel=0 AND OrderId=@OrderId", "", new object[] { orderId });
            return orderBase.CityName;
        }

        /// <summary>
        /// 根据订单号获取设计合同名称
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>设计合同名称</returns>
        public string GetDesignContractName(string orderId)
        {
            string cityName = this.GetContractCityName(orderId);
            string contractName = string.Format("{0}设计合同", cityName);
            return contractName;
        }

        /// <summary>
        /// 获取套餐信息
        /// </summary>
        /// <param name="suitId">套餐Id</param>
        /// <returns>套餐信息.</returns>
        private Suit_Info GetSuitInfo(int suitId)
        { 
            Suit_Info suitInfo = EBS.Interface.Data.DBOper.Suit_Info.Get("IsDel=0 AND ID=@ID", "", new object[] { suitId });
            return suitInfo;
        }

        /// <summary>
        /// 根据订单号获取施工合同名称
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>System.String.</returns>
        public string GetConstructionContractName(string orderId)
        {
            string cityName = this.GetContractCityName(orderId);
            N_Order_QuoteInfo contractInfo = this.GetContractInfo(orderId);
            int suitId = contractInfo.SuitID;
            string suitName = string.Empty;
            // 验证是否是套餐合同模板
            if (suitId == 0)
            {
                if (cityName.Equals("重庆") && contractInfo.ID <= 0)
                {
                    suitName = "自定义装修";
                }
                else
                {
                    suitName = "个性化装修";  
                }

            }
            else
            {
                suitName = this.GetSuitInfo(suitId).SuitName;
            }

            string contractName = string.Format("{0}施工合同{1}", cityName, suitName);
            return contractName;
        }

        /// <summary>
        /// 根据订单号和合同类获取合同模板Id
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="contractType">合同类型</param>
        /// <returns>合同模板Id</returns>
        private string GetTemplateIdByOrderId(string orderId,int contractType)
        {
            string constractName = string.Empty;
            if (contractType == 0)
            {
                constractName = this.GetConstructionContractName(orderId);
            }
            else if (contractType == 1)
            {
                constractName = this.GetDesignContractName(orderId);
            }
            else
            {
                // 以后的新类型合同在此追加
                throw new ArgumentOutOfRangeException("未处理的合同类型");
            }
            return this.GetTemplateIdByContractName(constractName);
        }

        /// <summary>
        /// 根据订单号获取合同日志信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>合同日志记录</returns>
        public CityContractQueue GetCityContractQueue(string orderId, int ContractType)
        {
            CityContractQueue cityContractQueue = EBS.Interface.Data.DBOper.CityContractQueue.Get("IsDel=0 AND ContractType=@ContractType AND OrderId=@OrderId", "", new object[] { ContractType, orderId });
            return cityContractQueue;
        }

        /// <summary>
        /// 根据合同日志记录获取Base64格式的图片地址
        /// </summary>
        /// <param name="cityContractQueue">合同日志信息</param>
        /// <returns>Base64格式的图片地址</returns>
        public string GetBaseImage(CityContractQueue cityContractQueue)
        {
            string downloadURL = ConfigurationManager.AppSettings["ESignature_DownloadFileURL"] ?? "http://interface.ebs.home.fang.com/ESignature/DownloadFile.ashx";
            string binaryImg = string.Empty;
            string imgSection = string.Empty;
            if ((cityContractQueue != null || cityContractQueue.ID > 0) && !string.IsNullOrEmpty(cityContractQueue.SignatureUrl))
            {
                binaryImg = this.GetImgBase64(downloadURL, "downloadUrl=" + cityContractQueue.SignatureUrl + "&extension=png");
                imgSection = string.Format(@"<img src=""{0}"">", binaryImg);
            }
            return imgSection;
        }

        private string AppendStampFlag(string html)
        {
            string StampFlag = "乙方（盖章）：";
            html = html.Replace("乙方代表（签字盖章）：", StampFlag);
            html = html.Replace("乙方代表（盖章）：", StampFlag);
            return html.Replace("乙方：", StampFlag);
        }
    }
    
}