using BaseComponent.NetworkComponent;
using ComponentTest.ConsoleApp.CommonClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ComponentTest.ConsoleApp
{
    public class FileTest
    {
        public RemoteFile remoteFile{get;set;}

        public FileTest()
        {
            this.remoteFile = new RemoteFile();
        }

        public void RemoteFileTest()
        {
            string path = @"e:\log.txt";
            path = @"E:\01.Doc\02.工作日志\02.2015-11\02-CallInterfaceLog日志解析\09-Download-Log\";
            path = string.Format("{0}log-{1}.txt",path,DateTime.Now.ToString("yyyyMMddhhmmss"));
            string url = "ftp://tianhe:sdfaf.1x@124.251.47.122/trains_logs/speed/zxbapp_log_2015-11-04.txt"; 
            //this.remoteFile.ReadRemoteFile(string.Empty, path);
            //url = @"\\192.168.127.154\98.SharedFolder\fzxapp_log_2015-11-03.txt";
            //url = @"\\192.168.127.154\98.SharedFolder\sample.txt";
            //url = @"\\192.168.126.227\testFolder\fzxapp_log_2015-11-03.txt";
            //url = @"ftp://tianhe:sdfaf.1x@124.251.47.122/trains_logs/speed/zxbapp_log_2015-11-04.txt"
            //url = @"ftp://124.251.47.122/trains_logs/speed/zxbapp_log_2015-11-04.txt";
            url = @"ftp://124.251.47.122/trains_logs/err/err2015-10-20.txt";
            //url = @"ftp://124.251.47.122/trains_logs/speed/zxbapp_log_2015-11-04.txt";


            this.remoteFile.DownLoadFtpFile(url, path,"gb2312");
        }

        public string GetDir()
        {
            string str = System.Environment.CurrentDirectory;
            Console.WriteLine(str);
            return string.Empty;
        }

        public void readFile(string filepath)
        {
            byte[] byData = new byte[100];
            char[] charData = new char[1000];
            try
            {
                string path = "..\\..\\Data\\doc\\ID13531-201206.txt";

                FileStream file = new FileStream(path, FileMode.Open);
                file.Seek(0, SeekOrigin.Begin);
                //byData传进来的字节数组,用以接受FileStream对象中的数据,
                //第2个参数是字节数组中开始写入数据的位置,它通常是0,
                //表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                file.Read(byData, 0, 100);
                Decoder d = Encoding.Default.GetDecoder();
                d.GetChars(byData, 0, byData.Length, charData, 0);
                Console.WriteLine(charData);
                file.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        public void CreateFile<T>(string path, List<T> list)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            StreamWriter streamWriter = new StreamWriter(path, true, Encoding.GetEncoding("shift-jis"));
            string buffer = new ReflectionTest().ConvertToString(list);

            streamWriter.Write(buffer);
            streamWriter.Flush();
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
