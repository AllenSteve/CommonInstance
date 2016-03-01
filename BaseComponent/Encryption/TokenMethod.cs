// ***********************************************************************
// Assembly         : EBS.Common
// Author             : 仇士龙
// Created           : 12-17-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-21-2015
// ***********************************************************************
// <copyright file="TokenMethod.cs" company="家居集团无线技术部-">
//     Copyright (c) Fang.com. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BaseComponent.Encryption
{
    public class TokenMethod
    {
        /// <summary>
        /// 生成加密字符串
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="soufunId">搜房Id</param>
        /// <returns>加密字符串</returns>
        public static string CreateCryptograph(string timestamp, string soufunId)
        {
            string origin = string.Format("timestamp={0}&soufunId={1}", timestamp, soufunId);
            return AdvancedEncryption.AESEncrypt(origin).Replace("+", "%2B");
        }

        /// <summary>
        /// 验证参数
        /// 规则：搜房ID相同，时间间隔不超过5分钟
        /// </summary>
        /// <param name="soufunId">搜房Id</param>
        /// <param name="cryptograph">密文字符串</param>
        /// <param name="timespan">时间间隔--例如：5分钟-new TimeSpan(TimeSpan.TicksPerMinute*5)</param>
        /// <returns>验证结果</returns>
        public bool VerifyParam(string soufunId, string cryptograph, TimeSpan timespan)
        {
            string origin = AdvancedEncryption.AESDecrypt(cryptograph);
            IDictionary<string, string> param = this.GetParam(origin);
            if (param.ContainsKey("soufunId") && param.ContainsKey("timestamp"))
            {
                string id = param["soufunId"];
                string timestamp = param["timestamp"];
                DateTime currentTime = DateTime.Now;
                DateTime time_stamp = GetTimestampDate(timestamp);
                if (soufunId.Equals(id) && (time_stamp.Add(timespan) > currentTime))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将时间值转换为时间戳
        /// </summary>
        /// <param name="time">日期类型</param>
        /// <returns>整型</returns>
        public static long ConvertDateTime2Timestamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 将时间戳字符串转换为日期格式
        /// </summary>
        /// <param name="timestamp">时间戳字符串</param>
        /// <returns>日期</returns>
        public static DateTime GetTimestampDate(string timestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return startTime.Add(toNow);
        }

        /// <summary>
        /// 验证请求参数
        /// http://www.cnblogs.com/chendaoyin/archive/2013/06/28/3161579.html
        /// </summary>
        /// <param name="request">http请求</param>
        /// <param name="timespan">超时时长</param>
        /// <returns>验证搜房ID和加密字符串</returns>
        public bool CheckParam(HttpRequest request,int timespan=5)
        {
            if (request["SoufunId"] == null || request["CheckCode"] == null)
            {
                return false;
            }
            else
            {
                string soufunId = request["SoufunId"].ToString();
                string cryptograph = request["CheckCode"].ToString().Replace("%2B", "+").Substring(0, 64);
                return this.VerifyParam(soufunId, cryptograph, new TimeSpan(TimeSpan.TicksPerMinute * timespan));
            }
        }

        /// <summary>
        /// 解密后的明文字符串
        /// </summary>
        /// <param name="origin">解密后的明文</param>
        /// <returns>参数字典</returns>
        private IDictionary<string, string> GetParam(string origin)
        {
            string[] paramList = origin.Split('&');
            IDictionary<string, string> param = new Dictionary<string, string>();
            foreach (var item in paramList)
            {
                string[] itemArray = item.Split('=');
                param.Add(itemArray[0], itemArray[1]);
            }
            return param;
        }
    }
}
