using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Enums
{
	/// <summary>
	/// 支付类型(DealerPayRecord表TradeTypeID字段)
	/// </summary>
	public enum TradeTypeIDEnum 
	{
		信息费扣款 = 1,
		信息费退款 = 2,
		质保金冻结 = 3,
		预存款充值 = 4,
		//V2期
	}
	/// <summary>
	/// 支付状态(DealerPayRecord表PayStatus字段)
	/// </summary>
	public enum PayStatusType 
	{
		待审核 = 1,
		审核通过 = 2,
		已驳回 = 3,
		支付成功 = 4,
		支付失败 = 5,
		运营通过 = 6,
	}
	/// <summary>
	/// 支付方式（DealerPayRecord表ChargeType字段）
	/// </summary>
	public enum ChargeType
	{
		线上支付=1,
		现金支付=2,
	}

    // 金融返回的参数中的交易号类型
    public enum TradeNoType
    {
        Out_Trade_NO =1,
        Trade_NO =2
    }

    public enum RechargeType
    {
        质保金充值=1,
        预存款充值=2
    }
}
