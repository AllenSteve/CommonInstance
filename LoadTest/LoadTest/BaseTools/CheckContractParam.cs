using BaseComponent.Encryption;
using BaseFunction.Service.EbsService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadTest.BaseTools
{
    public partial class CheckContractParam : Form
    {
        public CheckContractParam()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cryptograph.Text))
            {
                this.plaintext.Text = AdvancedEncryption.AESDecrypt(cryptograph.Text.Replace("%2B", "+"));
                var array = this.plaintext.Text.Split('&');
                this.timestamp.Text = array[0].Split('=')[1];
                this.soufunid.Text = array[1].Split('=')[1];
                this.currenttime.Text = TokenMethod.GetTimestampDate(this.timestamp.Text).ToString();
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;
            string timeStamp = TokenMethod.ConvertDateTime2Timestamp(current).ToString();
            string soufunId = this.soufunid.Text;
            this.CreateCryptograph.Text = TokenMethod.CreateCryptograph(timeStamp,soufunId);
            this.NewDateTime.Text = current.ToString();
        }
    }
}
