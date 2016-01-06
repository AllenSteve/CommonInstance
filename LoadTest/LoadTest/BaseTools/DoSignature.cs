using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadTest.BaseTools
{
    public partial class DoSignature : Form
    {
        private string param { get; set; }

        public DoSignature()
        {
            InitializeComponent();
            this.InitParam();
        }

        private void InitParam()
        {
            param = string.Empty;
            KeyValuePair<int, string> item = new KeyValuePair<int, string>(1, "签字");
            //this.CB_Type.Items.Add(item);
            item = new KeyValuePair<int, string>(3, "签章");
            //this.CB_Type.Items.Add(item);
            this.CB_Type.Items.Add(1);
            this.CB_Type.Items.Add(3);

            this.T_OrderId.Text = "J01201601040341006";
            this.T_stampno.Text = "1200";
            this.CB_Type.Text = this.CB_Type.Items[0].ToString();
        }

        private string CreateParam()
        {
            string encdata = this.T_encdata.Text.Replace(@"\""", @"""");

            if (this.CB_Type.Text.Equals("1"))
            {
                this.T_stampno.Clear();
                return string.Format("type={0}&wono={1}&encdata={2}", this.CB_Type.Text, this.T_OrderId.Text, encdata);
            }
            else
            {
                this.T_stampno.Text = "1200";
            }

            return string.Format("type={0}&wono={1}&encdata={2}&templateno={3}", this.CB_Type.Text, this.T_OrderId.Text, encdata, this.T_stampno.Text);
            
        }

        private void Btn_createQueryString_Click(object sender, EventArgs e)
        {
            this.param = this.CreateParam();
        }

        private void Btn_postRequest_Click(object sender, EventArgs e)
        {
            this.param = this.CreateParam();
            string url = "http://bjcajk.light.fang.com/esignature";
            string DownloadUrl = string.Empty;
            string UploadKey = string.Empty;
            long SoufunID = 77613994L;
            string Ip = "192.168.179.237";
            string wono = this.T_OrderId.Text;
            string UploadUrl = ConfigurationManager.AppSettings["UploadUrl"] ?? "http://img1u.fang.com/upload/filesrv2";
            //Stream pdfStream = EBS.Interface.ESignature.Method.WebUtility.RequestUrl(EBS.Interface.ESignature.Method.Config.ESignatureUrl, param, true, 300000);
            Stream pdfStream = this.RequestUrl(url, this.param, true, 300000);
            if (pdfStream != null)
            {
                 #region 上传合同
                MemoryStream ms = new MemoryStream();
                pdfStream.CopyTo(ms);
                //EBS.SMSManager.SendEmail.Send("zhangjunchao@soufun.com", "电子签章", "内存流大小" + ms.Length.ToString());
                if (ms.Length > 0)
                {
                    UploadKey = EBS.Interface.ESignature.Method.Security.getAuthKey(SoufunID.ToString(), Ip);
                    //上传合同
                    MemoryStream zipStream = EBS.Interface.ESignature.Method.ZipUtility.ZipFile(wono + ".pdf", ms.ToArray());
                    DownloadUrl = EBS.Interface.ESignature.Method.UploadUtility.HttpUploadFile(UploadUrl + "?i=&city=&channel=jiaju.ziliao&t=" + UploadKey + "&uid=" + SoufunID + "&isflash=y", wono + ".zip", "multipart/form-data", zipStream);
                    EBS.SMSManager.SendEmail.Send("zhangjunchao@soufun.com", "电子签章", "下载地址" + DownloadUrl);
                    EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子签章", "下载地址" + DownloadUrl);
                }
                #endregion
            }
        }

        private Stream RequestUrl(string url, string param, bool isPost, int timeout = 5000, Encoding encoding = null)
        {
            try
            {
                HttpWebRequest hwr = isPost ? (HttpWebRequest)WebRequest.Create(url) : (HttpWebRequest)WebRequest.Create(url + "?" + param);
                hwr.Timeout = timeout;
                hwr.Method = isPost ? "post" : "get";
                hwr.ContentType = isPost ? "application/x-www-form-urlencoded" : "";
                if (isPost)
                {
                    byte[] requestbytes = System.Text.Encoding.ASCII.GetBytes(param);
                    Stream requeststream = hwr.GetRequestStream();
                    requeststream.Write(requestbytes, 0, requestbytes.Length);
                    requeststream.Close();
                }
                HttpWebResponse res = (HttpWebResponse)hwr.GetResponse();
                if (!res.ContentType.Contains("pdf"))
                {
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("GB2312"));
                    string backstr = sr.ReadToEnd();
                    if (param.Split('=')[1].Split('&')[0].Equals("1"))
                    {
                        EBS.SMSManager.SendEmail.Send("zhangjunchao@soufun.com", "电子签章", "签字失败" + backstr);
                        EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子签章", "签字失败" + backstr);
                    }
                    else
                    {
                        EBS.SMSManager.SendEmail.Send("zhangjunchao@soufun.com", "电子签章", "签章失败" + backstr);
                        EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子签章", "签章失败" + backstr);
                    }
                    return null;
                }

                //StreamReader sr = new StreamReader(res.GetResponseStream(), encoding ?? System.Text.Encoding.Default);
                return res.GetResponseStream();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
