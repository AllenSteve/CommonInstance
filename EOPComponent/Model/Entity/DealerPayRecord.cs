#region
/*************************************************/
/***** Create by zoujiliang 2015/11/24 11:37:08****/
/*************************************************/
using System;
using System.Collections.Generic;
using System.Text;
namespace EOPComponent.Model.Entity
{
    public partial class DealerPayRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CityID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DealerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DealerName { get; set; }
        /// <summary>
        /// 订单号，若非信息费的话，为空
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public int TradeTypeID { get; set; }
        /// <summary>
        /// 交易类型名称：信息费扣款/退款/质保金冻结/预存款充值
        /// </summary>
        public string TradeTypeName { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TradeAmount { get; set; }
        /// <summary>
        /// 金融的交易流水
        /// </summary>
        public string OutTradeNO { get; set; }
        /// <summary>
        /// 支付成功/失败/待审核/运营通过/审核通过/驳回
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 家居的流水号
        /// </summary>
        public string InTradeNO { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 业主手机号
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 业主楼盘地址
        /// </summary>
        public string DistrictAddress { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int ChargeType { get; set; }

    }
}
#endregion
