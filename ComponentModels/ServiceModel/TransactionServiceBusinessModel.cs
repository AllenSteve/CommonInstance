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
                                                                        string tradeType,
                                                                        decimal paidAmount,
                                                                        decimal tradeAmount,
                                                                        decimal price,
                                                                        int quantity,
                                                                        string title,
                                                                        string subject,
                                                                        string invoker,
                                                                        string extra_param,
                                                                        string platform,
                                                                        string origin,
                                                                        string charset = "Utf-8"
                                                                            )
            : base(return_url)
        {
            this.out_trade_no = out_trade_no;
            this.user_id = soufunId;
            this.trade_type = tradeType;
            this.paid_amount = paidAmount;
            this.trade_amount = tradeAmount;
            this.price = price;
            this.quantity = quantity;
            this.title = title;
            this.subject = subject;
            this.invoker = invoker;
            this.extra_param = extra_param;
            this.platform = platform;
            this.origin = origin;
            this.charset = charset;
        }

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
            return "https://payment.fang.com/cashiernew/cashierordercreateforweb.html?"+param;
        }

    }
}
