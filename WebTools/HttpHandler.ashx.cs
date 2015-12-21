using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseFunction.ServiceInterface;
using BaseFunction.Service;
using BaseFunction.Service.EbsService;
using System.Net;
using System.IO;

namespace EOP.Web.Ajax
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class PaymentHandler : IHttpHandler
    {
        public int issuccess { get; set; }
        public string msg { get; set; }

        private IServiceFactory factory { get; set; }

        public PaymentHandler()
        {
            this.issuccess = 0;
            this.msg = string.Empty;
        }

        //1. 同步通知
        //2. 渠道派单/电商派单
        public void ProcessRequest(HttpContext context)
        {
            factory = new ServiceFactory();
            var service = factory.Create<ParseCookie>();
            string sfut = service.ParseSfutCookie(context);
            // 执行下载-成功
            //this.Download();
            // 执行下载-文件不可读
            string URLAddress = @"http://img7.soufunimg.com/static/jiaju/2015_09/23/M01/0E/2F/wKgEK1YCYJKILG7LAAKDghj6K1EAAWKWwE9o6EAAoOa543.zip";
            this.DownloadPDFContract(context, URLAddress);
        }

        private void DownloadPDFContract(HttpContext context, string downloadURL, string encoding = "UTF-8")
        {
            string extension = "pdf";
            string fileName = System.IO.Path.GetFileNameWithoutExtension(downloadURL);
            WebClient wc = new WebClient();
            byte[] zipBytes = wc.DownloadData(downloadURL);
            MemoryStream zipStream = new MemoryStream(zipBytes);
            byte[] fileBytes = default(byte[]);
            fileBytes = EBS.Interface.ESignature.Method.ZipUtility.UnZipFile(zipStream);
            context.Response.Expires = 0;
            context.Response.AddHeader("Pragma", "No-cache");
            context.Response.AddHeader("Cache-Control", "No-cache");
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = HttpUtility.UrlEncode(System.Text.Encoding.GetEncoding(encoding).GetBytes(fileName));
            }
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + (string.IsNullOrEmpty(fileName) ? "1." : (fileName + ".")) + extension + "");//设置文件名
            context.Response.AddHeader("Content-Length", fileBytes.Length.ToString());//设置下载文件大小
            string contentType = "application/pdf";
            context.Response.ContentType = contentType;
            context.Response.BinaryWrite(fileBytes);
        }

        private void Download()
        {
            WebClient client = new WebClient();
            string URLAddress = @"http://img7.soufunimg.com/static/jiaju/2015_09/23/M01/0E/2F/wKgEK1YCYJKILG7LAAKDghj6K1EAAWKWwE9o6EAAoOa543.zip";

            string receivePath = @"E:\01.Doc\02.工作日志\02.2015-12\06-合同改修\01.PDF下载\";

            client.DownloadFile(URLAddress, receivePath + System.IO.Path.GetFileName(URLAddress));
        }

        private  void BytesToFile(byte[] buffer, string fileName)
        {
            MemoryStream ms = new MemoryStream(buffer);
            this.StreamToFile(ms, fileName);
            
            //// 把 byte[] 写入文件
            //FileStream fs = new FileStream(fileName, FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);
            //bw.Write(buffer);
            //bw.Close();
            //fs.Close();
        }

        public void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
