using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComponent
{
    public class RemoteFile
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string remoteFileUrl { get; set; }

        public FileWebRequest remoteFileRequest { get; set; }

        public FileWebResponse remoteFileResponse { get; set; }

        public Stream remoteFileStream { get; set; }

        public RemoteFile()
        { 
            this.remoteFileUrl = string.Empty;
        }

        /// <summary>
        /// 万能方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public void DownLoadFtpFile(string url, string path)
        {
            try
            {
                //var response = this.UriHandler(url);
                var response = this.FtpRequest(url);
                if (response.StatusCode == FtpStatusCode.OpeningData)
                {
                    this.WriteRemoteFile(response, path);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReadRemoteFile(string url, string path)
        {
            try
            {
                //建立一个HttpWebRequest实例，我们不用使用HttpWebRequest类的构造函数，
                //而是使用WebRequest类提供的静态方法Create
                //直接将网址传进去，如果协议为http或者https，它会返回一个HttpWebRequest实例
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.fileURL);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                //zxbapp_log_2015-11-04.txt
                //string uri = @"ftp://124.251.47.122/trains_logs/speed/";
                //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                //request.Credentials = new NetworkCredential("tianhe", "sdfaf.1x");
                //request.Method = WebRequestMethods.Ftp.DownloadFile;
                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();


                //if (response.StatusCode == HttpStatusCode.OK)
                if (response.ContentLength > 0)
                {
                    this.WriteRemoteFile(response, path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private WebResponse UriHandler(string uri)
        {
            try
            { 
                var request = WebRequest.Create(uri);
                request.PreAuthenticate = true;
                request.Credentials = new NetworkCredential("tianhe", "sdfaf.1x");
                //request.Method = WebRequestMethods.Ftp.DownloadFile;
                var response = request.GetResponse();
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private FtpWebResponse FtpRequest(string uri)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Credentials = new NetworkCredential("tianhe", "sdfaf.1x");
                //request.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取相应消息中的流对象，将其写入指定路径
        /// </summary>
        /// <param name="response">响应消息</param>
        /// <param name="path">要写入的文件路径</param>
        private void WriteRemoteFile(WebResponse response, string path)
        {
            // 获取接收到的流
            Stream stream = response.GetResponseStream();
            // 建立一个流读取器，可以设置流编码，不设置则默认为UTF-8
            Encoding encoding = Encoding.GetEncoding("GB2312");
            System.IO.StreamReader streamReader = new StreamReader(stream, encoding);
            // 读取流字符串内容
            string content = string.Empty;
            StreamWriter streamWriter = new StreamWriter(path, false, encoding);
            try
            {
                while ((content = streamReader.ReadLine()) != null)
                {
                    // 记得readline之后此时的content中不包含换行符；
                    //streamWriter.Write(content+"\r\n");
                    // TODO:解析日志文件到对象数组中去；
                    streamWriter.Write(content+"\r\n");
                    streamWriter.Flush();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // 资源清理代码：关闭相关对象
                streamWriter.Close();
                streamWriter.Dispose();
                streamReader.Close();
                response.Close();
            }
        }

    }
}
