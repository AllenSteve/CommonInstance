using ComponentModels.ServiceModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace BaseFunction.BaseTools
{
    public partial class TransactionService : Form
    {
        private TransactionServiceBusinessModel model { get; set; }

        public TransactionService()
        {
            InitializeComponent();
            this.LoadConfig();
            model = null;
        }

        private void LoadConfig()
        {
            // 访问非安全的证书；
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;

            // 
            string return_url = ConfigurationManager.AppSettings["return_url"];
            this.CB_returnURL.Text = return_url;

            //
            string soufunId = ConfigurationManager.AppSettings["soufunId"];
            this.T_soufunID.Text = soufunId;
            //
            //string tradeType = ConfigurationManager.AppSettings["tradeType"];
            //this.T_tradeType.Text = tradeType;

            ///
            string paidAmount = ConfigurationManager.AppSettings["paidAmount"];
            string tradeAmount = ConfigurationManager.AppSettings["tradeAmount"];
            string price = ConfigurationManager.AppSettings["price"];
            this.T_payAmount.Text = paidAmount;
            this.T_tradeAmount.Text = tradeAmount;
            this.T_price.Text = price;

            //string quantity = ConfigurationManager.AppSettings["quantity"];
            string title = ConfigurationManager.AppSettings["title"];
            string title1 = ConfigurationManager.AppSettings["title1"];
            this.CB_title.Items.Add(title);
            this.CB_title.Items.Add(title1);
            this.CB_title.Text = this.CB_title.Items[0].ToString();

            string subject = ConfigurationManager.AppSettings["subject"];
            this.T_subject.Text = subject;

            //string invoker = ConfigurationManager.AppSettings["invoker"];
            string extra_param = ConfigurationManager.AppSettings["extra_param"];
            this.T_extraParam.Text = extra_param;
            //string platform = ConfigurationManager.AppSettings["platform"];
            //string origin = ConfigurationManager.AppSettings["origin"];
            //string charset = ConfigurationManager.AppSettings["charset"];
            this.CB_queryStringType.Items.Add("HTML");
            this.CB_queryStringType.Items.Add("GET");
            this.CB_queryStringType.Items.Add("POST");
            this.CB_queryStringType.Text = this.CB_queryStringType.Items[0].ToString();
        }

        // 在生成的代理类中添加RemoteCertificateValidate函数
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert,X509Chain chain, SslPolicyErrors error)
        {
            //System.Console.WriteLine("Warning, trust any certificate");
            //MessageBox.Show("Warning, trust any certificate");
            //为了通过证书验证，总是返回true
            return true;
        }

        private HttpWebResponse PostQueryParams(string paramStr,string requestUrl = "https://payment.test.fang.com/cashiernew/cashierordercreateforweb.html",string encoding = "GB2312")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
            try
            {
                byte[] buffer = Encoding.GetEncoding(encoding).GetBytes(paramStr);//Encoding.UTF8.GetBytes(postData); // 转化
                //request.CookieContainer = null;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = buffer.Length;

                // Send the data.//写入参数
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(buffer, 0, buffer.Length);
                    newStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (HttpWebResponse)request.GetResponse();
        }


        private void Btn_createQueryString_Click(object sender, EventArgs e)
        {
            this.model = new TransactionServiceBusinessModel(this.CB_returnURL.Text,
                                                                                      this.T_soufunID.Text,
                                                                                      decimal.Parse(this.T_payAmount.Text),
                                                                                      decimal.Parse(this.T_tradeAmount.Text),
                                                                                      decimal.Parse(this.T_price.Text),
                                                                                      this.CB_title.Text,
                                                                                      this.T_subject.Text,
                                                                                      this.T_extraParam.Text
                                                                                      );
            string queryType = this.CB_queryStringType.Text;
            if (queryType.Equals("HTML"))
            {
                this.T_queryString.Text = model.ToHtmlString();
            }
            else if (queryType.Equals("GET"))
            {
                this.T_queryString.Text = "https://payment.test.fang.com/cashiernew/cashierordercreateforweb.html?" + model.ToQueryString();
            }
            else if (queryType.Equals("POST"))
            {
                this.T_queryString.Text = model.ToQueryString();
            }
            
        }

        private void Btn_postRequest_Click(object sender, EventArgs e)
        {
            try
            {
                //HttpWebResponse response = this.PostQueryParams("https://payment.test.fang.com/cashiernew/cashierordercreateforweb.html?" + this.T_queryString.Text + "&txtusername=17701000011&password=451426");
                HttpWebResponse response = this.PostQueryParams( this.T_queryString.Text);

                //HttpWebResponse response = this.PostQueryParams( this.T_queryString.Text);

                //response  = this.PostQueryParams(this.T_queryString.Text,"http://www.renren.com/PLogin.do")

                StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB2312"));
                this.T_response.Text = sr2.ReadToEnd();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Btn_clearResult_Click(object sender, EventArgs e)
        {
            this.T_queryString.Clear();
            this.T_response.Clear();
        }

        private void Btn_copyQueryString_Click(object sender, EventArgs e)
        {
            if(this.T_queryString.Text.Trim() != string.Empty)
                Clipboard.SetDataObject(this.T_queryString.Text);
        }

        private void Btn_testConnection_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.AppSettings["localhostDB"];
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("数据库连接成功");
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}
