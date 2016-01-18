using ExtensionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.ServiceModel
{
    /// <summary>
    /// 金融接口参数信息-基本参数
    /// </summary>
    public class TransactionServiceBaseModel
    {
        public TransactionServiceBaseModel(TransactionServiceBaseModel baseModel)
        {
            this.service = baseModel.service;
            this.version = baseModel.version;
            this.biz_id = baseModel.biz_id;
            this.sign_type = baseModel.sign_type;
            this.sign = baseModel.sign;
            this.call_time = baseModel.call_time;
            this.return_url = baseModel.return_url;
            this.notify_url = baseModel.notify_url;
        }

        public TransactionServiceBaseModel(string return_url, string notify_url = null)
        {
            this.service = "cashier_order_create_for_web";
            this.version = "1.0";
            this.biz_id = "747420150428100001";
            this.sign_type = "MD5";
            // 通过算法赋值
            this.sign = null;
            // 格式转换("yyyy-MM-dd hh:mm:ss.fff")
            this.call_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            // 交易完成后的跳转地址
            this.return_url = return_url;
            // 不设置异步地址-置空即可
            this.notify_url = notify_url;
        }

        // 接口名称-接口名称（固定值：cashier_order_create_for_web）
        public string service { get; set; }
        // 版本号-接口版本-1.0
        public string version { get; set; }
        // 商户编号-业务线商户号-通过页面获取的商户ID
        public string biz_id { get; set; }
        // 签名类型-签名类型--例如MD5
        public string sign_type { get; set; }
        // 签名-签名值--与sign_type对应的签名信息
        public string sign { get; set; }
        // 请求时间-请求发起时间（精确到毫秒）
        public string call_time { get; set; }
        // 同步通知地址-交易完成后，页面主动跳转地址
        public string return_url { get; set; }
        // 服务器异步通知地址-服务器主动通知的路径
        public string notify_url { get; set; }

        public IDictionary<string, string> ToDictionary()
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties = type.GetProperties();
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            object value = null;
            for (int i = 0; i < fields.Length; ++i)
            {
                value = fields[i].GetValue(this);
                if (value != null && !string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    dictionary.Add(properties[i].Name, value.ToString().ConvertToUrlEncode());
                }
            }
            return dictionary;
        }
    }
}
