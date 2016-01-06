using EBS.Interface.EContract.Method;
using EBS.Interface.Model;
using EbsComponent.Method.DatabaseMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadTest.BaseTools
{
    public partial class DoSignature : Form
    {
        private IDictionary<string, int> contractTypeDictionary { get; set; }
        private BaseMethod method { get; set; }
        private QueryEBS query { get; set; }

        public DoSignature()
        {
            InitializeComponent();
            this.InitParam();
        }

        private void InitParam()
        {
            method = new BaseMethod();
            query = new QueryEBS();

            List<CityInfo> cityList = query.Query<CityInfo>().All().ToList();

            this.T_OrderId.Text = "J01201601040341006";
            KeyValuePair<int, string> item = new KeyValuePair<int, string>(1, "签字");
            //this.CB_Type.Items.Add(item);
            item = new KeyValuePair<int, string>(3, "签章");
            //this.CB_Type.Items.Add(item);
            //this.CB_CityList.Items.Add(1);
            //this.CB_CityList.Items.Add(3);

            contractTypeDictionary = new Dictionary<string, int>();
            contractTypeDictionary.Add("设计合同",1);
            contractTypeDictionary.Add("施工合同",0);
            foreach (var contractType in contractTypeDictionary)
            {
                this.CB_ContractType.Items.Add(contractType.Key);
            }

            foreach (var city in cityList)
            {
                this.CB_CityList.Items.Add(city.CityName);
            }
            this.CB_CityList.Text = this.CB_CityList.Items[0].ToString();

        }

        private string CreateParam()
        {
            string encdata = this.T_encdata.Text.Replace(@"\""", @"""");
            

            if (this.CB_CityList.Text.Equals("1"))
            {
                this.CB_Mobile.Text = string.Empty;
                return string.Format("type={0}&wono={1}&encdata={2}", this.CB_CityList.Text, this.T_OrderId.Text, encdata);
            }
            else
            {
                this.CB_Mobile.Text = "1200";
            }

            return string.Format("type={0}&wono={1}&encdata={2}&templateno={3}", this.CB_CityList.Text, this.T_OrderId.Text, encdata, this.CB_Mobile.Text);
            
        }

        private void CB_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CreateParam();
        }

        private string GetStampNo(string orderId,int ContractType)
        {
            // 仇士龙，20151229
            // 获取合同模板Id
            string templateId = method.GetContractTemplateID(orderId, ContractType);
            // 获取印章Id
            string stampId = method.GetStampId(templateId);
            return stampId;
        }

        private void Btn_GetContractPage_Click(object sender, EventArgs e)
        {
            

        }

        private void CB_ContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string orderId = this.T_OrderId.Text.Trim();
                if (!string.IsNullOrEmpty(orderId))
                {
                    this.CB_ContractName.Items.Clear();
                    string contractName = method.GetContractName(orderId, contractTypeDictionary[this.CB_ContractType.Text]);
                    this.CB_ContractName.Items.Add(contractName);
                    this.CB_ContractName.Text = contractName;
                    this.T_ContractStampNo.Text = this.GetStampNo(orderId, contractTypeDictionary[this.CB_ContractType.Text]);
                    this.T_ContractTemplateId.Text = method.GetContractTemplateID(orderId, contractTypeDictionary[this.CB_ContractType.Text]);
                }
            }
            catch(Exception ex)
            {
                EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", "解析失败");
                throw ex;
            }
        }
    }
}
