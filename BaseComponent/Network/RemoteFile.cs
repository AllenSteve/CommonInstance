using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.Network
{
    public class RemoteFile
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string remoteFileUrl { get; set; }

        public FtpWebRequest ftpRequest { get; set; }

        public FtpWebResponse ftpResponse { get; set; }

        public Stream remoteFileStream { get; set; }

        public RemoteFile()
        { 
            this.remoteFileUrl = string.Empty;
        }

        /// <summary>
        ///  下载FTP文件到指定位置
        /// </summary>
        /// <param name="url">文件链接地址</param>
        /// <param name="path">下载路径:文件写入位置</param>
        /// <param name="encode">文件编码方式</param>
        public void DownLoadFtpFile(string url, string path, string encode = "UTF-8")
        {
            // 获取接收到的流
            Stream stream = this.GetFtpFileStream(url) ;
            // 建立一个流读取器，可以设置流编码，不设置则默认为UTF-8
            Encoding encoding = Encoding.GetEncoding(encode);
            // 读取流对象
            StreamReader streamReader = new StreamReader(stream, encoding);
            // 读取流字符串内容
            StreamWriter streamWriter = new StreamWriter(path, false, encoding);
            try
            {
                string content = string.Empty;
                while ((content = streamReader.ReadLine()) != null)
                {
                    // 记得readline之后此时的content中不包含换行符；
                    //streamWriter.Write(content+"\r\n");
                    // TODO:解析日志文件到对象数组中去；
                    streamWriter.Write(content + "\r\n");
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
                streamReader.Dispose();
            }
        }

        /// <summary>
        /// 返回Response中的流对象对象
        /// </summary>
        /// <param name="url">FTP链接字符串</param>
        public Stream GetFtpFileStream(string url)
        {
            try
            {
                this.ftpResponse = this.FtpRequest(url);
                if (this.ftpResponse.StatusCode == FtpStatusCode.OpeningData)
                {
                    return this.ftpResponse.GetResponseStream();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("错误信息：{0}", ex.Message));
                this.ftpResponse.Close();
            }
            finally
            {
                // 资源清理代码：关闭相关对象
                //this.ftpResponse对象在此处关闭后返回的stream对象不可读
                //记得在返回的stream读取完成之后进行资源释放stream.Close()
                //stream对象释放完之后相应的响应对象会自动标记为不可用
            }
            return null;
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

        public FtpWebResponse FtpRequest(string uri)
        {
            try
            {
                this.ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
                this.ftpRequest.Credentials = new NetworkCredential("tianhe", "sdfaf.1x");
                this.ftpRequest.KeepAlive = true;
                // 默认方式为：RETR即下载文件
                this.ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                return this.ftpResponse = (FtpWebResponse)this.ftpRequest.GetResponse();
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
