using BaseComponent.DocumentParser;
using BaseFunction.ServiceInterface.IEbsServcie;
using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service.EbsService
{
    public class ParseCookie : IParseCookie
    {
        public string ParseSfutCookie()
        {
            string sfut="903FF1EAC865A085FB599C9C5A86029F0A9B624F0A66D490330E73CACCCC6B7C60EA79AC4A24BBE65876B8CB218EAB49296B8F40C1528AD7747A62DE1D7CCBCAD836C5E4AEACF8E34B2CB9D49AEB4070";
            return sfut;
        }

        public string DecryptSfut2Url(string sfutCookie=null)
        {
            string sfut="903FF1EAC865A085FB599C9C5A86029F0A9B624F0A66D490330E73CACCCC6B7C60EA79AC4A24BBE65876B8CB218EAB49296B8F40C1528AD7747A62DE1D7CCBCAD836C5E4AEACF8E34B2CB9D49AEB4070";
            string result = Soufun.Passport.Config.GetUrlValidSfutCookie(sfut);
            return result;
        }

        public object ParseUserInfo(string url,string encoding = "UTF-8")
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                response = (HttpWebResponse)request.GetResponse();
                if (response.ContentLength > 0)
                {
                    var userXML =  this.ParseResponse(response, encoding);
                    return this.ParseXML2Object(userXML);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ParseResponse(HttpWebResponse response, string encoding = "UTF-8")
        {
            // 获取接收到的流
            Stream stream = response.GetResponseStream();
            // 设置流编码，默认为UTF-8
            Encoding encode = Encoding.GetEncoding(encoding);
            return new StreamReader(stream, encode).ReadToEnd();
        }

        private object ParseXML2Object(string xml)
        {
            XMLParser xmlParser = new XMLParser();
            XMLNode xn = xmlParser.Parse(xml);
            string userId = xn.GetValue("soufun_passport>0>common>0>userid>0>_text");
            string userName = xn.GetValue("soufun_passport>0>common>0>username>0>_text");
            string userType = xn.GetValue("soufun_passport>0>common>0>usertype>0>_text");
            string isValid = xn.GetValue("soufun_passport>0>common>0>isvalid>0>_text");
            string return_result = xn.GetValue("soufun_passport>0>common>0>return_result>0>_text");
            string error_reason = xn.GetValue("soufun_passport>0>common>0>error_reason>0>_text");
            string interfacename = xn.GetValue("soufun_passport>0>common>0>interfacename>0>_text");
            object userInfo = new { UserId = userId, UserName = userName, UserType = userType, IsValid = isValid, Result = return_result, Reason = error_reason, Interface = interfacename };
            return userInfo;
        }
    }
}
