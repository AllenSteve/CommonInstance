using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent
{
    public class CallCMD
    {
        public CallCMD()
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
            p.StandardInput.WriteLine(@"ipconfig /flushdns ");
            p.StandardInput.WriteLine("exit");
            p.WaitForExit();
            p.Close();
            p.Dispose();
        }
    }
}
