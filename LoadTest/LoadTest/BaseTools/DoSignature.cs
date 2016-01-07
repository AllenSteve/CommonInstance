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
        private IDictionary<string, string> cityMobileDictionary { get; set; }
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
            this.CreateCityMobileDictionary();
            contractTypeDictionary = new Dictionary<string, int>();
            contractTypeDictionary.Add("设计合同", 1);
            contractTypeDictionary.Add("施工合同", 0);

            KeyValuePair<int, string> item = new KeyValuePair<int, string>(1, "签字");
            //this.CB_Type.Items.Add(item);
            item = new KeyValuePair<int, string>(3, "签章");
            //this.CB_Type.Items.Add(item);
            //this.CB_CityList.Items.Add(1);
            //this.CB_CityList.Items.Add(3);

            
            foreach (var contractType in contractTypeDictionary)
            {
                this.CB_ContractType.Items.Add(contractType.Key);
            }

            foreach (var city in cityList)
            {
                if(!city.CityName.Equals("中山"))
                this.CB_CityList.Items.Add(city.CityName);
            }
            this.CB_CityList.Text = this.CB_CityList.Items[0].ToString();

        }

        private void CreateParam()
        {
            this.ClearText();
            this.CB_Mobile.Text = string.Empty;
            this.CB_Mobile.Items.Clear();
            foreach(var mobile in this.cityMobileDictionary)
            {
                if (this.CB_CityList.Text.Equals(mobile.Value))
                {
                    this.CB_Mobile.Items.Add(mobile.Key);
                }
            }
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
                    this.T_encdata.Text = method.GetTemplateHtml(this.T_ContractTemplateId.Text);
                    
                    if (string.IsNullOrEmpty(this.T_ContractStampNo.Text))
                    {
                        string msg = string.Format("获取印章号失败\n订单号：{0}\n合同名：{1}\n手机号：{2}", orderId, this.CB_ContractName.Text, this.CB_Mobile.Text); 
                        EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", msg);
                        EBS.SMSManager.SendEmail.Send("linye@fang.com", "电子合同", msg);
                    }
                    if (string.IsNullOrEmpty(this.T_ContractTemplateId.Text))
                    {
                        string msg = string.Format("获取合同模板Id\n订单号：{0}\n合同名：{1}\n手机号：{2}", orderId, this.CB_ContractName.Text, this.CB_Mobile.Text);
                        EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", msg);
                        EBS.SMSManager.SendEmail.Send("linye@fang.com", "电子合同", msg);
                    }


                }
            }
            catch(Exception ex)
            {
                EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", "解析失败");
                throw ex;
            }
        }


        private void CreateCityMobileDictionary()
        {
            this.cityMobileDictionary = new Dictionary<string, string>();
            //北京
            cityMobileDictionary.Add("13244444401", "北京");
            cityMobileDictionary.Add("13244444402", "北京");
            cityMobileDictionary.Add("13800001111", "北京");//注意局改报价检查为零的原因
            cityMobileDictionary.Add("13811112222", "北京");
            //重庆
            cityMobileDictionary.Add("13244444403", "重庆");
            cityMobileDictionary.Add("13244444452", "重庆");
            //郑州
            cityMobileDictionary.Add("13244444405", "郑州");
            cityMobileDictionary.Add("13244444406", "郑州");
            cityMobileDictionary.Add("13244444453", "郑州");
            //广州
            cityMobileDictionary.Add("13244444407", "广州");
            cityMobileDictionary.Add("13244444454", "广州");
            //青岛
            cityMobileDictionary.Add("13244444409", "青岛");
            cityMobileDictionary.Add("13244444410", "青岛");
            //上海
            cityMobileDictionary.Add("13244444411", "上海");
            cityMobileDictionary.Add("13244444455", "上海");
            //成都
            cityMobileDictionary.Add("13244444413", "成都");
            cityMobileDictionary.Add("13244444414", "成都");
            cityMobileDictionary.Add("13244444443", "成都");
            cityMobileDictionary.Add("13244444444", "成都");
            //深圳
            cityMobileDictionary.Add("13244444415", "深圳");
            cityMobileDictionary.Add("13244444456", "深圳");
            //无锡
            cityMobileDictionary.Add("13244444417", "无锡");
            cityMobileDictionary.Add("13244444457", "无锡");
            //苏州
            cityMobileDictionary.Add("13244444420", "苏州");
            cityMobileDictionary.Add("13244444445", "苏州");
            //武汉
            cityMobileDictionary.Add("13244444421", "武汉");
            cityMobileDictionary.Add("13244444422", "武汉");
            cityMobileDictionary.Add("13244444446", "武汉");
            cityMobileDictionary.Add("13244444447", "武汉");
            //天津
            cityMobileDictionary.Add("13244444423", "天津");
            cityMobileDictionary.Add("13244444458", "天津");
            cityMobileDictionary.Add("13244444459", "天津");
            //西安
            cityMobileDictionary.Add("13244444425", "西安");
            cityMobileDictionary.Add("13244444426", "西安");
            //杭州
            cityMobileDictionary.Add("13244444427", "杭州");
            cityMobileDictionary.Add("13244444428", "杭州");
            cityMobileDictionary.Add("13244444448", "杭州");
            //长沙
            cityMobileDictionary.Add("13244444429", "长沙");
            cityMobileDictionary.Add("13244444430", "长沙");
            //石家庄
            cityMobileDictionary.Add("13244444431", "石家庄");
            cityMobileDictionary.Add("13244444432", "石家庄");
            cityMobileDictionary.Add("13244444449", "石家庄");
            cityMobileDictionary.Add("13244444450", "石家庄");
            //宁波
            cityMobileDictionary.Add("13244444433", "宁波");
            cityMobileDictionary.Add("13244444434", "宁波");
            //济南
            cityMobileDictionary.Add("13244444435", "济南");
            cityMobileDictionary.Add("13244444460", "济南");
            cityMobileDictionary.Add("13244444461", "济南");
            cityMobileDictionary.Add("13244444462", "济南");
            cityMobileDictionary.Add("13244444463", "济南");
            cityMobileDictionary.Add("13244444464", "济南");
            cityMobileDictionary.Add("13244444465", "济南");
            cityMobileDictionary.Add("13244444466", "济南");
            cityMobileDictionary.Add("13244444467", "济南");
            //沈阳
            cityMobileDictionary.Add("13244444437", "沈阳");
            cityMobileDictionary.Add("13244444468", "沈阳");
            cityMobileDictionary.Add("13244444469", "沈阳");
            cityMobileDictionary.Add("13244444451", "沈阳");
            //大连
            cityMobileDictionary.Add("13244444439", "大连");
            cityMobileDictionary.Add("13244444470", "大连");
            cityMobileDictionary.Add("13244444471", "大连");
            cityMobileDictionary.Add("13244444472", "大连");
            //南京
            cityMobileDictionary.Add("13244444441", "南京");
            cityMobileDictionary.Add("13244444473", "南京");
            cityMobileDictionary.Add("13244444474", "南京");
            cityMobileDictionary.Add("13244444475", "南京");
        }

        private void CB_Mobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearText();
            string mobile = this.CB_Mobile.Text;
            string orderId = string.Empty;
            var orderInfo = query.Query<N_Order_QuoteEx>().Find(n => n.Mobile == mobile);
            if (orderInfo != null && !string.IsNullOrEmpty(orderInfo.OrderID))
            {
                orderId = orderInfo.OrderID;
            }
            this.T_OrderId.Text = orderId;
        }

        private void ClearText()
        {
            this.T_OrderId.Clear();
            this.CB_ContractType.Text = string.Empty;
            this.CB_ContractName.Text = string.Empty;
            this.T_ContractStampNo.Clear();
            this.T_ContractTemplateId.Clear();
            this.T_encdata.Clear();
        }
    }
}
