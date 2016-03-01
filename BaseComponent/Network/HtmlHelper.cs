using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace BaseComponent.Network
{
        /// <summary>
        /// 来自网络：http://blog.csdn.net/htsnoopy/article/details/7094224
        /// </summary>
        public class HTMLHelper
        {
            /// <summary>
            /// 获取post之后的CooKie
            /// </summary>
            /// <param name="loginUrl"></param>
            /// <param name="postdata"></param>
            /// <param name="header"></param>
            /// <returns></returns>
            public static CookieContainer GetCookie(string loginUrl, string postdata, HttpHeader header, string encoding = "UTF-8")
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
                    CookieContainer cc = new CookieContainer();
                    request = (HttpWebRequest)WebRequest.Create(loginUrl);
                    request.Method = header.method;
                    request.ContentType = header.contentType;
                    byte[] postdatabyte = Encoding.GetEncoding(encoding).GetBytes(postdata);
                    request.ContentLength = postdatabyte.Length;
                    request.AllowAutoRedirect = false;
                    request.CookieContainer = cc;
                    request.KeepAlive = true;


                    //提交请求
                    Stream stream;
                    stream = request.GetRequestStream();
                    stream.Write(postdatabyte, 0, postdatabyte.Length);
                    stream.Close();

                    //接收响应
                    response = (HttpWebResponse)request.GetResponse();
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                    CookieCollection cook = response.Cookies;
                    //Cookie字符串格式
                    string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);

                    return cc;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }



            /// <summary>
            /// 获取html
            /// </summary>
            /// <param name="getUrl"></param>
            /// <param name="cookieContainer"></param>
            /// <param name="header"></param>
            /// <returns></returns>
            public static string GetHtml(string getUrl, CookieContainer cookieContainer, HttpHeader header, string encoding = "UTF-8")
            {
                Thread.Sleep(1000);
                HttpWebRequest httpWebRequest = null;
                HttpWebResponse httpWebResponse = null;
                try
                {
                    httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);
                    httpWebRequest.CookieContainer = cookieContainer;
                    httpWebRequest.ContentType = header.contentType;
                    httpWebRequest.ServicePoint.ConnectionLimit = header.maxTry;
                    httpWebRequest.Referer = getUrl;
                    httpWebRequest.Accept = header.accept;
                    httpWebRequest.UserAgent = header.userAgent;
                    httpWebRequest.Method = "GET";
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(encoding));
                    string html = streamReader.ReadToEnd();
                    streamReader.Close();
                    responseStream.Close();
                    httpWebRequest.Abort();
                    httpWebResponse.Close();
                    return html;
                }
                catch (Exception e)
                {
                    if (httpWebRequest != null) httpWebRequest.Abort();
                    if (httpWebResponse != null) httpWebResponse.Close();
                    return string.Empty;
                }
            }

            public static List<Cookie> GetAllCookies(CookieContainer cc)
            {
                List<Cookie> lstCookies = new List<Cookie>();

                Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                    System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

                foreach (object pathList in table.Values)
                {
                    SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                        | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                    foreach (CookieCollection colCookies in lstCookieCol.Values)
                        foreach (Cookie c in colCookies) lstCookies.Add(c);
                }

                return lstCookies;
            }

            // 在生成的代理类中添加RemoteCertificateValidate函数
            private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
            {
                //System.Console.WriteLine("Warning, trust any certificate");
                //MessageBox.Show("Warning, trust any certificate");
                //为了通过证书验证，总是返回true
                return true;
            }

        }

        public class HttpHeader
        {
            public string contentType { get; set; }

            public string accept { get; set; }

            public string userAgent { get; set; }

            public string method { get; set; }

            public int maxTry { get; set; }
        }
}