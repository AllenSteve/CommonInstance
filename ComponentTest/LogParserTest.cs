using DocumentParser;
using NetworkComponent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest
{
    public class LogParserTest
    {
        public LogParserTest()
        { 
        }

        public void Run()
        {
            LogParser log = new LogParser();
            string path = @"e://fzxapp_log_2015-11-03.txt";

            //path = @"e://sample.txt";
            //path = @"e://errorlog.txt";
            path = @"e://error.txt";
            path = @"E:\01.Doc\02.工作日志\02.2015-11\02-CallInterfaceLog日志解析\02-目标日志\zxbapp_log_2015-11-04.txt";


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            log.LoadLogFile(path);

            Console.WriteLine("读取文件路径：" + elapsedTime);
            Console.WriteLine("解析日志文件：" + elapsedTime);
            log.ParseLogToList();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            Console.WriteLine("解析日志文件完成：" + elapsedTime);
            Console.WriteLine("日志文件行数："+log.logModelList.Count);

            Console.WriteLine("开始分布式批量插入..." + elapsedTime);
            log.DistributeBulkInsert(log.logModelList);
        }

        public void ReadFtpFile()
        {
            LogParser log = new LogParser();
            string path = string.Empty;
            path = @"E:\01.Doc\02.工作日志\02.2015-11\02-CallInterfaceLog日志解析\09-Download-Log\";
            path = string.Format("{0}log-{1}.txt", path, DateTime.Now.ToString("yyyyMMddhhmmss"));

            string url = @"ftp://124.251.47.122/trains_logs/speed/zxbapp_log_2015-11-04.txt";
            //url = @"ftp://124.251.47.122/trains_logs/err/err2015-10-20.txt";
            // 1MB
            url = @"ftp://124.251.47.122/trains_logs/speed/crmapp_log_2015-11-05.txt";
            // 197.25MB--zxbapp_log_2015-11-06
            url = @"ftp://124.251.47.122/trains_logs/speed/zxbapp_log_2015-11-04.txt";
            ///trains_logs/huoyue
            //url = @"ftp://124.251.47.122/trains_logs/huoyue/log_Huoyue_2015-11-04.txt";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}",ts.Hours,ts.Minutes,ts.Seconds,ts.Milliseconds);

            // 创建远程文件对象
            RemoteFile ftpFile = new RemoteFile();
            FtpWebResponse resopnse = ftpFile.FtpRequest(url);
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("链接Ftp服务器..." + elapsedTime);
            // 获取接收到的流
            Stream stream = ftpFile.GetFtpFileStream(url);
            // 建立一个流读取器，可以设置流编码，不设置则默认为UTF-8

            if (resopnse.StatusCode == FtpStatusCode.OpeningData)
            {
                stream  = resopnse.GetResponseStream();
            }
            Encoding encoding = Encoding.GetEncoding("gb2312");
            // 读取流对象
            StreamReader streamReader = new StreamReader(stream, encoding);
            try
            {
                streamReader.ReadLineAsync();
                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine("开始读取文件流..." + elapsedTime);
                string line = string.Empty;
                //string[] lineArray = streamReader.ReadToEnd().Split('\n');
                while ((line = streamReader.ReadLine()) != null)
                {
                    log.ParseLogRecordToList(line);
                    ts = stopWatch.Elapsed;
                    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                    Console.WriteLine("时间：" + elapsedTime);
                    Console.WriteLine("文件流中的日志行数：" + log.logModelList.Count);
                }
                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine("文件流读取完毕..." + elapsedTime);
                Console.WriteLine("文件流中的日志行数："+log.logModelList.Count);
                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine("开始写入数据库..." + elapsedTime);

                log.BulkInsertList(log.logModelList);

                stopWatch.Stop();
                ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine("数据库写入完毕..." + elapsedTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // 资源清理代码：关闭相关对象
                streamReader.Close();
                streamReader.Dispose();
                resopnse.Close();
            }
        }
    }
}
