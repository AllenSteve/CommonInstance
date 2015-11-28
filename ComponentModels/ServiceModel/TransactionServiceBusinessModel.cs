using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;

namespace ComponentModels.ServiceModel
{
    /// <summary>
    /// 金融接口参数信息-业务参数
    /// </summary>
    public class TransactionServiceBusinessModel : TransactionServiceBaseModel
    {
        public TransactionServiceBusinessModel(string return_url,
                                                                        string soufunId,
                                                                        decimal paidAmount,
                                                                        decimal tradeAmount,
                                                                        decimal price,
                                                                        string title,
                                                                        string subject,
                                                                        string extra_param) : base(return_url)
        {
            this.trade_no = null;// "T_TRADE_20150302_0000000001";
            this.out_trade_no = "123456789";
            this.user_id = soufunId;
            // 固定值问陶虹宇
            this.trade_type = "30003";
            this.paid_amount = paidAmount;
            this.trade_amount = tradeAmount;
            this.price = price;
            this.quantity = 1;
            this.title = title;
            this.subject = subject;
            this.invoker = null;
            // 额外参数格式： 商家ID|支付类型|backurl跳转的地址（由用户传入）
            this.extra_param = extra_param;
            this.platform = "PC";
            this.origin = "EOP";
            this.charset = "GB2312";
        }

        public string trade_no { get; set; }
        // 业务线唯一交易编号-请确保该编号在各业务线为唯一标示
        public string out_trade_no { get; set; }
        // 用户编号-用户搜房通行证ID
        public string user_id { get; set; }
        // 交易类型-业务交易类型
        public string trade_type { get; set; }
        // 实付金额-单位为：RMB Yuan。精确到小数点后两位
        public decimal paid_amount { get; set; }
        // 订单金额-单位为：RMB Yuan。精确到小数点后两位
        public decimal trade_amount { get; set; }
        // 商品单价-商品单价(金额量不超过1000000)
        public decimal price { get; set; }
        // 商品数量-商品数量
        public int quantity { get; set; }
        // 业务名称-业务名称/活动名称
        public string title { get; set; }
        // 订单描述-订单描述/订单关键字
        public string subject { get; set; }
        // IMEI号-用户在创建交易时，该用户当前所使用移动设备的IMEI串号
        public string invoker { get; set; }
        // 回传参数-如果请求时传递了该参数，则返回给商户时会回传该参数
        public string extra_param { get; set; }
        // 平台-平台-APP,HD,PC,WAP其中之一
        public string platform { get; set; }
        // 来源-业务来源
        public string origin { get; set; }
        // 编码方式-业务部门编码方式-Utf-8(默认),gb2312等
        public string charset { get; set; }

        public IDictionary<string, string> GetDictionary()
        {
            TransactionServiceBaseModel baseModel = new TransactionServiceBaseModel(this);
            IDictionary<string, string> baseDictionary = baseModel.ToDictionary();
            IDictionary<string, string> dictionary = this.ToDictionary();
            foreach (var item in baseDictionary)
            {
                dictionary.Add(item.Key, item.Value);
            }
            return dictionary;
        }

        public string ToQueryString()
        {
            string encrypt = null;
            List<KeyValuePair<string, string>> kvList = this.GetDictionary().OrderBy(p => p.Key, StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true)).ToList();
            string EncryptParam = string.Join("&", kvList.FindAll(p => !string.IsNullOrEmpty(p.Value)).Select(p => p.Key + "=" + p.Value));
            string param = string.Join("&", kvList.Select(p => p.Key + "=" + p.Value));
            param += "&sign=" + encrypt.CouponEncrypt(EncryptParam, "8c05ccff20b34ba5a9c55a9a002a37c5");
            //return "https://payment.test.fang.com/cashiernew/cashierordercreateforweb.html?" + param;
            return param;
        }

        public string ToHtmlString()
        {
            StringBuilder line = new StringBuilder();
            StringBuilder html = new StringBuilder();
            html.Append("<HTML>\n<BODY>\n<form method=\"post\" action=\"https://payment.test.fang.com/cashiernew/cashierordercreateforweb.html\">\n");

            string query = this.ToQueryString();
            var queryParam = query.Split('&');
            foreach (var param in queryParam)
            {
                line.Clear();
                var row = param.Split('=');
                line.Append("<input name=\""+row[0]+"\" ");
                line.Append("value=\""+row[1]+"\" ");
                line.Append("type=\"hidden\"");
                line.Append("/>\n");
                html.Append(line);
            }

            html.Append("<input type=\"submit\" value=\"提交\">");
            html.Append("</form>\n</BODY>\n</HTML>");
            return html.ToString();
        }

    }
}
