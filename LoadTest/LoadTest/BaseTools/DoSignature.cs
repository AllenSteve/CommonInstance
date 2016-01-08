using EBS.Interface.EContract.Method;
using EBS.Interface.EContract.Model;
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
        private List<N_Order_QuoteEx> orderList { get; set; }
        private List<CityInfo> cityList { get; set; }
        private BaseMethod method { get; set; }
        private QueryEBS entity { get; set; }

        public DoSignature()
        {
            InitializeComponent();
            this.InitParam();
        }

        private void InitParam()
        {
            this.method = new BaseMethod();
            this.entity = new QueryEBS();
            this.cityList = entity.Query<CityInfo>().All().ToList();
            this.orderList = new List<N_Order_QuoteEx>();
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

            this.CB_ContractType.Items.AddRange(contractTypeDictionary.Select(o=>o.Key).ToArray());
            var citys = this.cityList.Except(this.cityList.FindAll(o => o.CityName.Equals("中山")));
            this.CB_CityList.Items.AddRange(citys.Select(o=>o.CityName).ToArray());
            this.CB_CityList.Text = this.CB_CityList.Items[0].ToString();
        }

        private void CreateParam()
        {
            this.ClearText();
            this.CB_Mobile.Text = string.Empty;
            this.CB_Mobile.Items.Clear();
            var mobileList = this.cityMobileDictionary.Where(o=>o.Value.Equals(this.CB_CityList.Text));
            this.CB_Mobile.Items.AddRange(mobileList.Select(o => o.Key).ToArray());
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
            string html = this.T_encdata.Text;
            string contractType = this.CB_ContractType.Text;
            if (contractType.Equals("设计合同"))
            {
                html = this.GetSetUnsignedDesignContractPage(html);
            }
            else if (contractType.Equals("施工合同"))
            {
                html = this.GetSetUnsignedConstructionContractPage(html);
            }

            if (html.Trim() != string.Empty)
                Clipboard.SetDataObject(html);
        }

        private void CB_ContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string orderId = this.CB_OrderId.Text.Trim();
                if (!string.IsNullOrEmpty(orderId))
                {
                    this.T_ContractName.Clear();
                    string contractName = method.GetContractName(orderId, contractTypeDictionary[this.CB_ContractType.Text]);
                    this.T_ContractName.Text = contractName;
                    this.T_ContractStampNo.Text = this.GetStampNo(orderId, contractTypeDictionary[this.CB_ContractType.Text]);
                    this.T_ContractTemplateId.Text = method.GetContractTemplateID(orderId, contractTypeDictionary[this.CB_ContractType.Text]);
                    this.T_encdata.Text = method.GetTemplateHtml(this.T_ContractTemplateId.Text);
                    
                    if (string.IsNullOrEmpty(this.T_ContractStampNo.Text))
                    {
                        string msg = string.Format("获取印章号失败\n订单号：{0}\n合同名：{1}\n手机号：{2}", orderId, this.T_ContractName.Text, this.CB_Mobile.Text); 
                        EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", msg);
                        //EBS.SMSManager.SendEmail.Send("linye@fang.com", "电子合同", msg);
                        //EBS.SMSManager.SendEmail.Send("zhangjunchao@fang.com", "电子合同", msg);
                    }
                    if (string.IsNullOrEmpty(this.T_ContractTemplateId.Text))
                    {
                        string msg = string.Format("获取合同模板Id失败\n订单号：{0}\n合同名：{1}\n手机号：{2}", orderId, this.T_ContractName.Text, this.CB_Mobile.Text);
                        EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", msg);
                        //EBS.SMSManager.SendEmail.Send("linye@fang.com", "电子合同", msg);
                        //EBS.SMSManager.SendEmail.Send("zhangjunchao@fang.com", "电子合同", msg);
                    }
                }
            }
            catch(Exception ex)
            {
                EBS.SMSManager.SendEmail.Send("qiushilong@fang.com", "电子合同", "解析失败");
                throw ex;
            }
        }

        /// <summary>
        /// 读取配置文件中城市对应的手机号
        /// </summary>
        private void CreateCityMobileDictionary()
        {
            this.cityMobileDictionary = new Dictionary<string, string>();
            var citys = this.cityList.Where(o => !o.CityName.Equals("中山")).Select(o=>o.CityName).ToArray();
            foreach (var cityName in citys)
            {
                string[] mobiles = ConfigurationManager.AppSettings[cityName].Split(',');
                foreach (var mobile in mobiles)
                {
                    if (!this.cityMobileDictionary.ContainsKey(mobile.Trim()))
                    {
                        this.cityMobileDictionary.Add(mobile.Trim(), cityName);
                    }
                    else
                    {
                        string errorMsg = string.Format("城市：{0}，手机号：{1}\n存在多条重复记录，请检查配置文件！", cityName, mobile.Trim());
                        string captionMsg = "错误信息";
                        MessageBox.Show(errorMsg, captionMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CreateOrderList()
        {
            foreach (var mobile in this.cityMobileDictionary)
            {
                var orderInfo = entity.Query<N_Order_QuoteEx>().Find(n => n.Mobile == mobile.Key);
                if (orderInfo != null && !string.IsNullOrEmpty(orderInfo.OrderID))
                {
                    this.orderList.Add(orderInfo);
                }
            }
        }

        private void CB_Mobile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearText();
            string mobile = this.CB_Mobile.Text;
            string orderId = string.Empty;
            N_Order_QuoteEx orderInfo;
            if(this.orderList.Count==0)
            {
                orderInfo = entity.Query<N_Order_QuoteEx>().Find(n => n.Mobile == mobile);
            }
            else
            {
                orderInfo = this.orderList.Find(n => n.Mobile == mobile);         
            }
            
            if (orderInfo != null && !string.IsNullOrEmpty(orderInfo.OrderID))
            {
                orderId = orderInfo.OrderID;
            }
            this.CB_OrderId.Text = orderId;
        }

        private void ClearText()
        {
            this.CB_OrderId.Text = string.Empty;
            this.CB_ContractType.Text = string.Empty;
            this.T_ContractName.Text = string.Empty;
            this.T_ContractStampNo.Clear();
            this.T_ContractTemplateId.Clear();
            this.T_encdata.Clear();
        }

        private string GetSetUnsignedDesignContractPage(string pageHtml)
        {
            BaseDesignContractModel contract;
            string orderId = this.CB_OrderId.Text;
            string html = pageHtml;
            // 获取合同模板Id
            string templateId = this.T_ContractTemplateId.Text;
            if (!string.IsNullOrEmpty(templateId) && !string.IsNullOrEmpty(orderId))
            {
                // 解析模板
                //html = method.GetTemplateHtml(templateId);
                // 初始化参数
                contract = new BaseDesignContractModel(orderId);
                // 变更签名参数
                contract.SignatureA = contract.GetUnsignedContractSignature();
                // 变更日期参数
                contract.SetUnsignedDesignContractDate();
                // 删除印章标签
                html = method.RemoveStampImage(html);
                // 删除打印标签
                html = method.RemovePrintImage(html);
                // 附加待签设置脚本
                html = method.AppendUnsignedDesignContract(html);
                // 注意放在最后去进行参数替换
                html = method.ReplaceHtmlWithModel(contract, html);
            }
            return html;
        }

        private string GetSetUnsignedConstructionContractPage(string pageHtml)
        {
            BaseConstructionContractModel contract;
            string orderId = this.CB_OrderId.Text;
            string html = pageHtml;

            // 获取合同模板Id
            string templateId = this.T_ContractTemplateId.Text;

            if (!string.IsNullOrEmpty(templateId) && !string.IsNullOrEmpty(orderId))
            {
                // 解析模板
                //html = method.GetTemplateHtml(templateId);
                // 初始化参数
                contract = new BaseConstructionContractModel(orderId);
                // 变更签名参数
                contract.SignatureA = contract.GetUnsignedContractSignature();
                // 变更日期参数
                contract.SetUnsignedConstructionContractDate();
                // 删除印章标签
                html = method.RemoveStampImage(html);
                // 删除打印标签
                html = method.RemovePrintImage(html);
                // 附加待签设置脚本
                html = method.AppendUnsignedDesignContract(html);
                // 注意放在最后去进行参数替换
                html = method.ReplaceHtmlWithModel(contract, html);
                // 隐藏在页面上隐藏签字标签
                html = method.HideUnsignedContractSignatureTag(html);
            }
            return html;
        }

        private void Btn_GetTemplatePage_Click(object sender, EventArgs e)
        {
            string html = this.T_encdata.Text;
            if (html.Trim() != string.Empty)
                Clipboard.SetDataObject(html);
        }

        private void Btn_GetOrderList_Click(object sender, EventArgs e)
        {
            this.CreateOrderList();
            this.CB_OrderId.Items.AddRange(this.orderList.Select(o => o.OrderID).ToArray());
        }

        /// <summary>
        /// 通用测试按钮，用于一键覆盖测试所有测试用例
        /// 准备工作：
        /// 1-城市列表
        /// 2-城市列表相关手机号
        /// 3-订单号列表
        /// 4-合同类型，0-施工合同，1-设计合同
        /// 测试步骤：
        /// 1-根据订单号和合同类型查询
        /// 2-对得到的查询结果进行验证，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TestAll_Click(object sender, EventArgs e)
        {
            
        }
    }
}
