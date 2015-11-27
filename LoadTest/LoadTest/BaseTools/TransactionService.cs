using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseFunction.BaseTools
{
    public partial class TransactionService : Form
    {
        public TransactionService()
        {
            InitializeComponent();
            this.LoadConfig();

        }

        private void LoadConfig()
        {
            // 
            string return_url = ConfigurationManager.AppSettings["return_url"];
            this.CB_returnURL.Text = return_url;

            //
            string soufunId = ConfigurationManager.AppSettings["soufunId"];
            this.T_soufunID.Text = soufunId;
            //
            string tradeType = ConfigurationManager.AppSettings["tradeType"];
            this.T_tradeType.Text = tradeType;

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
        }

        private void Btn_createQueryString_Click(object sender, EventArgs e)
        {
            TransactionServiceBusinessModel model = null;

            model = new TransactionServiceBusinessModel(this.CB_returnURL.Text,
                                                                                      this.T_soufunID.Text,
                                                                                      this.T_tradeType.Text,
                                                                                      decimal.Parse(this.T_payAmount.Text),
                                                                                      decimal.Parse(this.T_tradeAmount.Text),
                                                                                      decimal.Parse(this.T_price.Text),
                                                                                      this.CB_title.Text,
                                                                                      this.T_subject.Text,
                                                                                      this.T_extraParam.Text
                                                                                      );
            var query = model.ToQueryString();

            this.T_queryString.Text = query;
        }
    }
}
