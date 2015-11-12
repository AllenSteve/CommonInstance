using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocurrencyTest
{
    public partial class CocurrencyLoader : Form
    {
        Process p;
        StreamWriter input;
        public CocurrencyLoader()
        {
            InitializeComponent();

            p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            input = p.StandardInput;
            p.BeginOutputReadLine();
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            update(e.Data + Environment.NewLine);
        }

        delegate void updateDelegate(string msg);

        void update(string msg)
        {
            if (this.InvokeRequired)
                Invoke(new updateDelegate(update), new object[] { msg });
            else
            {
                textBox2.Text += msg;
                textBox2.SelectionStart = textBox2.Text.Length - 1;
                textBox2.ScrollToCaret();
            }
        }

        //关闭GUI窗口
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            p.Close();
            p.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int requestCount = this.GetAmount(textBox1);
            int concurrency = this.GetAmount(textBox3);
            string driverName = @"c: ";
            string apacheDirectory = @"cd C:\Program Files (x86)\Apache Software Foundation\Apache2.2\bin";
            string requestStr = this.textBox4.Text.Trim();
            string execString = string.Format("ab.exe -n {0} -c {1} {2}", requestCount,concurrency,requestStr);

            input.WriteLine(driverName);//先转到系统盘下
            input.WriteLine(apacheDirectory);//再转到CMD所在目录下
            input.WriteLine(execString);

        }

        private int GetAmount(TextBox tb)
        {
            return string.IsNullOrEmpty(tb.Text) ? 0 : int.Parse(tb.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
        }

        //打开文件浏览对话框
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    this.openFileDialog1.ShowDialog();
        //}
        ////获取文件路径，将路径定位到文件所在位置
        //private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        //{
        //    string filepath = openFileDialog1.FileName;
        //    int fileposition = filepath.LastIndexOf("\\");
        //    textBox1.Text = "cd " + filepath.Substring(0, fileposition);
        //}
    }
}
