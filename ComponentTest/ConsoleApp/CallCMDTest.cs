using BaseComponent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest
{
    public class CocurrencyLoader
    {
        public CocurrencyLoader()
        {
            
        }

        public void Run()
        {
            Process p = new Process();
            p.StartInfo.FileName = @"C:\WINDOWS\system32\cmd.exe ";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(@"c: ");　　//先转到系统盘下
            p.StandardInput.WriteLine(@"cd C:\WINDOWS\system32 ");　　//再转到CMD所在目录下
            //p.StandardInput.WriteLine(@"shutdown -s -t 3600");
            p.StandardInput.WriteLine(@"cd C:\Program Files (x86)\Apache Software Foundation\Apache2.2\bin\");
            p.StandardInput.WriteLine(@"ab -n 10 -c 10 http://interface.ebs.home.test.fang.com/Gethandler.ashx?timestamp=2015/11/11%2016:50:32&version=v2.2.0&Method=IndexInfo&isPost=0");
            p.StandardInput.WriteLine(@"exit");
            p.WaitForExit();
            p.Close();
            p.Dispose();
        }
    }
}
