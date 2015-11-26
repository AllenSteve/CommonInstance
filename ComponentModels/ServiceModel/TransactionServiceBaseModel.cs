using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.ServiceModel
{
    /// <summary>
    /// 金融接口参数信息-基本参数
    /// </summary>
    public class TransactionServiceBaseModel
    {
        public TransactionServiceBaseModel(string url,DateTime callTime)
        {
            this.service = "cashier_order_create_for_web";
            this.version = "1.0";
            this.biz_id = "747420150428100001";
            this.sign_type = "MD5";
            // 询问张俊超
            this.sign = null;
            // 格式转换("yyyy-MM-dd hh:mm:ss.fff")
            this.call_time = callTime.ToString("yyyy-MM-dd hh:mm:ss.fff");
            // 交易完成后的跳转地址
            this.return_url = url;
            // 不设置异步地址-置空即可
            this.notify_url = null;
        }

        public IDictionary<string,string> ToDictionary()
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties = this.GetType().GetProperties();
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            
            for (int i = 0; i < fields.Length; ++i)
            {
                if (fields[i].GetValue(this)!=null)
                {
                    dictionary.Add(properties[i].Name, fields[i].GetValue(this).ToString()); 
                }
            }
            return dictionary;
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
    }
}
