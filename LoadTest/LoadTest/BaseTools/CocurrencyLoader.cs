using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private Process p;
        private StreamWriter input;
        public CocurrencyLoader()
        {
            InitializeComponent();

            cocurrencyList.Items.Add(10);
            cocurrencyList.Items.Add(100);
            cocurrencyList.Items.Add(1000);
            cocurrencyList.Items.Add(10000);
            cocurrencyList.RightToLeft = RightToLeft.No;

            requestList.Items.Add(10);
            requestList.Items.Add(100);
            requestList.Items.Add(1000);
            requestList.Items.Add(10000);
            requestList.RightToLeft = RightToLeft.No;
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
                ConsoleInterface.Text += msg;
                ConsoleInterface.SelectionStart = ConsoleInterface.Text.Length - 1;
                ConsoleInterface.ScrollToCaret();
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

            int requestCount = this.GetAmount(cocurrencyList.Text);
            int concurrencyCount = this.GetAmount(requestList.Text);

            if (requestCount>0 && concurrencyCount>0)
            {
                string driverName = @"c: ";
                string apacheDirectory = @"cd C:\Program Files (x86)\Apache Software Foundation\Apache2.2\bin";
                string request = string.Empty;

                if (string.IsNullOrEmpty(requestConn.Text.Trim()))
                {
                    request = ConfigurationManager.AppSettings["RequestURL"];
                    requestConn.Text = ConfigurationManager.AppSettings["RequestURL"];
                }
                else
                {
                    request = requestConn.Text.Trim();
                }
                string execString = string.Format("ab.exe -n {0} -c {1} \"{2}\"", requestCount, concurrencyCount, requestConn.Text);
                input.WriteLine(driverName);//先转到系统盘下
                input.WriteLine(apacheDirectory);//再转到CMD所在目录下
                input.WriteLine(execString);
            }
        }

        private int GetAmount(string text)
        {
            return string.IsNullOrEmpty(text.Trim()) ? 0 : int.Parse(text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.cocurrencyList.Text=string.Empty;
            this.ConsoleInterface.Clear();
            string conn  = ConfigurationManager.ConnectionStrings["OrderReadOnly"].ConnectionString;

            MessageBox.Show (conn);

            //this.requestList.Text=string.Empty;
            //this.requestConn.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.requestConn.Clear();
            TransactionServiceBusinessModel model = null;

            string return_url = ConfigurationManager.AppSettings["return_url"];
            string soufunId = ConfigurationManager.AppSettings["soufunId"];
            string tradeType = ConfigurationManager.AppSettings["tradeType"];

            string paidAmount=ConfigurationManager.AppSettings["paidAmount"];
            string tradeAmount = ConfigurationManager.AppSettings["tradeAmount"];
            string price = ConfigurationManager.AppSettings["price"];

            string quantity = ConfigurationManager.AppSettings["quantity"];

            string title = ConfigurationManager.AppSettings["title"];
            string subject = ConfigurationManager.AppSettings["subject"];
            string invoker = ConfigurationManager.AppSettings["invoker"];
            string extra_param = ConfigurationManager.AppSettings["extra_param"];
            string platform = ConfigurationManager.AppSettings["platform"];
            string origin = ConfigurationManager.AppSettings["origin"];
            string charset = ConfigurationManager.AppSettings["charset"];




            model = new TransactionServiceBusinessModel(return_url,
                                                                                      soufunId,
                                                                                      decimal.Parse(paidAmount),
                                                                                      decimal.Parse(tradeAmount),
                                                                                      decimal.Parse(price),
                                                                                      title,
                                                                                      subject,
                                                                                      extra_param
                                                                                      );
            var query = model.ToQueryString();

            this.ConsoleInterface.Text = query;
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
