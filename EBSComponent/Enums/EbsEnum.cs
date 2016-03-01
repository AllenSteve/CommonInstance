using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBS.BLL
{
    public enum ContractType
    {
        施工合同=0,
        设计合同 = 1,
    }

    public class EnumBLL
    {

        public enum Age
        {
            小于25岁 = 1,
            在25到34之间 = 2,
            在35到44之间 = 3,
            在45到55之间 = 4,
            大于55岁 = 5,
        }

        public enum ApplyZxdState
        {
            未申请 = 0,
            审核中 = 1,
            申请退回待补充 = 2,
            申请失败 = 3,
            申请成功待放款 = 4,
            申请成功待签约 = 5,
            放款中 = 6,
            已放款 = 7,
        }

        public enum AppPlatform
        {
            Android = 1,
            IOS = 2,
        }

        public enum AskType
        {
            家装顾问 = 1,
            设计师 = 2,
            项目经理 = 3,
            工程管家 = 4,
            主材顾问 = 5,
        }

        public enum AuditState
        {
            开工交底 = 20010,
            防水施工 = 20035,
            油漆施工 = 20050,
            尾期施工 = 20060,
        }

        public enum AuditStep
        {
            地方经理审核 = 10000,
            中心负责人审核 = 10010,
            补贴申请提交 = 10020,
            总经理审核 = 10030,
            财务专员审核 = 10040,
            财务总监审核 = 10050,
        }

        public enum BusinessType
        {
            家装顾问 = 1,
            工程管家 = 2,
            产品顾问 = 3,
            设计管家 = 4,
            工长 = 5,
            客服 = 6,
        }

        public enum ChangeLogProcess
        {
            中期 = 1,
            尾期 = 2,
        }

        public enum ChangeLogType
        {
            工艺变更 = 1,
            主材变更 = 2,
            辅材变更 = 3,
            促销变更 = 4,
        }

        public enum ChargeType
        {
            线上支付 = 0,
            现金支付 = 1,
            Pos刷卡 = 2,
            易宝Pos刷卡 = 3,
            装修贷 = 4,
        }

        public enum ChatCauseId
        {
            创建群 = 0,
            邀请成员 = 1,
            加入成员 = 2,
            退出群 = 3,
            解散 = 4,
            群基本信息更改 = 5,
        }

        public enum ChatIdentityType
        {
            普通游客 = 0,
            管家 = 1,
            设计师 = 2,
            工长 = 3,
            监理 = 4,
            业主 = 5,
            主材顾问 = 6,
        }

        public enum ChatLogType
        {
            群基本信息变更 = 0,
            群成员变更 = 1,
        }

        public enum ComplaintType
        {
            工程质量类 = 1,
            主材类 = 2,
            设计类 = 3,
            服务类 = 4,
        }

        public enum ContractType
        {
            施工合同 = 0,
            设计合同 = 1,
            借款合同 = 2,
            扣款授权委托书 = 3,
            开工交底验收单 = 4,
            水电验收单 = 5,
            防水验收单 = 6,
            中期验收单 = 7,
            竣工验收单 = 8,
            退补货变更单 = 9,
            升级变更单 = 10,
            保修单 = 11,
        }

        public enum CooperationType
        {
            搜房直销 = 0,
            合作伙伴 = 1,
            开放平台 = 2,
        }

        public enum CouponOprEnum
        {
            申请使用 = 1,
            使用成功 = 2,
            解冻 = 3,
            发放 = 4,
        }

        public enum CouponStatus
        {
            冻结 = 0,
            未使用 = 1,
            已使用 = 2,
            已过期 = 3,
            已作废 = 4,
        }

        public enum DcumentaryFollowStage
        {
            开工交底 = 100,
            房屋拆改 = 200,
            水电施工 = 300,
            防水施工 = 400,
            瓦工施工 = 500,
            木工施工 = 600,
            油漆施工 = 700,
            尾期施工 = 800,
        }

        public enum DecarationType
        {
            新房装修 = 1,
            旧房翻新 = 2,
            办公室装修 = 3,
            店铺装修 = 4,
            餐厅装修 = 5,
            酒店装修 = 6,
            其它类型 = 7,
        }

        public enum DesignerState
        {
            上门测量 = 1,
            初步方案 = 2,
            细化方案 = 3,
            开工交底 = 4,
        }

        public enum DesignStyle
        {
            现代简约 = 1,
            田园风格 = 2,
            中式古典 = 3,
            西式古典 = 4,
            欧美风情 = 5,
            东南亚风格 = 6,
            混合型风格 = 7,
            日韩风格 = 8,
            中式风格 = 9,
            简欧风格 = 10,
            新古典风格 = 11,
            混搭风格 = 12,
            地中海风格 = 13,
            其它 = 14,
        }

        public enum EC_DecorationType
        {
            新房装修 = 1,
            旧房翻修 = 2,
            办公室装修 = 3,
            店铺装修 = 4,
            餐厅装修 = 5,
            酒店装修 = 6,
            其他类型 = 7,
        }

        public enum EC_HouseReserve
        {
            今天 = 1,
            三天内 = 2,
            一周内 = 3,
            三个月内 = 4,
            六个月内 = 5,
            十二个月内 = 6,
            超过一年 = 7,
        }

        public enum EC_HouseType
        {
            一居室 = 1,
            二居室 = 2,
            三居室 = 3,
            四居室 = 4,
            复式 = 5,
            跃层 = 6,
            别墅 = 7,
            其他 = 8,
            五居室 = 9,
            六居室 = 10,
            六居室以上 = 11,
        }

        public enum FamilyType
        {
            单身贵族 = 1,
            两人世界 = 2,
            三口之家 = 3,
            四口之家 = 4,
            其他 = 5,
        }

        public enum FeedbackStatus
        {
            全部 = 0,
            待处理 = 1,
            已受理 = 2,
            已解决 = 3,
        }

        public enum GenjinStage
        {
            接洽 = 1,
            量房 = 2,
            出报价单 = 3,
            开工交底 = 4,
            房屋拆改 = 5,
            水电改造 = 6,
            瓦木工程 = 7,
            油漆工程 = 8,
            施工完成 = 9,
        }

        public enum GjPass
        {
            非通过 = 0,
            已经成为管家 = 1,
        }

        public enum GzService
        {
            新房装修 = 1,
            二手房装修 = 2,
            旧房改造 = 3,
            局部改造 = 4,
            装修别墅 = 5,
            工装 = 6,
        }

        public enum HandleType
        {
            业主申请 = 1,
            转交上级 = 2,
            提交方案 = 3,
            自动转交 = 4,
            确认已解决 = 5,
            确认重新投诉 = 6,
            评论回复 = 7,
        }

        public enum HandState
        {
            待提报 = 1,
            待审核 = 2,
            已确认 = 3,
            未通过 = 4,
            已转交 = 5,
            已过期 = 6,
            已退款 = 7,
            本月退款 = 8,
        }

        public enum HandStateTitle
        {
            待提报业绩 = 1,
            待审核业绩 = 2,
            已确认业绩 = 3,
            未通过业绩 = 4,
            已转交业绩 = 5,
            过期未提报 = 6,
            已退款业绩 = 7,
            本月退款业绩 = 8,
            累计提成 = 9,
        }

        public enum HouseStatus
        {
            新房 = 1,
            老房 = 2,
        }

        public enum HouseType
        {
            小户型 = 1,
            别墅 = 2,
            复式 = 3,
            一居室 = 4,
            二居室 = 5,
            三居室 = 6,
            四居室 = 7,
            跃层 = 8,
        }

        public enum HouseUse
        {
            自住 = 1,
            投资 = 2,
            度假 = 3,
        }

        public enum IdentityType
        {
            业主 = 0,
            管家操作 = 1,
            设计师 = 2,
            工长 = 3,
            监理 = 4,
            客服 = 5,
            主材商 = 6,
            搜房销售 = 7,
            供应商 = 8,
            财务 = 10,
            总经理 = 11,
            总部技术 = 12,
            总部管理 = 13,
            总部财务 = 14,
            集团领导 = 15,
            副总经理 = 16,
            企划 = 17,
            平台运营 = 18,
            人事 = 19,
            总部领导 = 20,
            合作伙伴 = 100,
        }

        public enum IMIdentityType
        {
            业主 = 0,
            家装顾问 = 1,
            工程管家 = 2,
            主材顾问 = 3,
            设计管家 = 4,
            工长 = 5,
            B端人员 = 6,
            C端游客 = 7,
            拓客部经理 = 8,
            设计部经理 = 9,
            设计经理 = 10,
            主材部经理 = 12,
            工程部经理 = 13,
        }

        public enum InfoType
        {
            主材明细 = 1,
            辅料明细 = 2,
            项目施工 = 3,
            升级清单 = 4,
            折现清单 = 5,
            其他费用 = 6,
        }

        public enum IntentionUserStage
        {
            前期沟通 = 1,
            见面到场 = 2,
            收房验房 = 4,
            上门量房 = 8,
            初步方案 = 16,
            修改方案 = 32,
            约看工地 = 64,
        }

        public enum IsQuality
        {
            合格 = 1,
            不合格 = 2,
        }

        public enum JiFenInterfaceType
        {
            积分赠送申请接口 = 1,
            积分赠送确认接口 = 2,
            积分赠送取消接口 = 3,
            积分赠送明细查询接口 = 4,
        }

        public enum JiFenOpType
        {
            客户来源 = 1,
            跟进状态 = 2,
            签约奖励 = 3,
            客户延期 = 4,
        }

        public enum JumpPage
        {
            首页 = 1001,
            订单列表页 = 1002,
            装修进度详情页 = 1003,
            支付明细页面 = 1004,
            服务团队列表页 = 1005,
            装修问答首页 = 1006,
            wap页 = 1007,
            节点评价页 = 1008,
        }

        public enum LogType
        {
            技术日志 = -1,
            手写跟进日志 = 0,
            订单状态日志 = 1,
            订单支付状态日志 = 2,
            验收日志 = 3,
            系统短信日志 = 4,
            服务申请记录 = 5,
            售后维修日志 = 6,
            投诉建议日志 = 7,
        }

        public enum MaterialOrderStatus
        {
            未处理 = 0,
            已配货 = 1,
            已发货 = 2,
            已收货 = 3,
            已撤销 = 4,
            重新配货 = 5,
            退货 = 6,
            拒收 = 7,
        }

        public enum MaterialUse
        {
            主材 = 1,
            辅材 = 2,
        }

        public enum N_PictureDescription
        {
            数量不够 = 1,
            清晰度不高 = 2,
            内容不符 = 3,
            无指定图片 = 4,
        }

        public enum N_WordDescription
        {
            字数不够 = 1,
            描述值不高 = 2,
            无关信息 = 3,
        }

        public enum NAlliancePayState
        {
            异常状况 = -2,
            待打款 = 0,
            已冻结 = 1,
            打款成功 = 2,
        }

        public enum Order_ProcessType
        {
            业主下单 = 0,
            客情更新 = 1,
            订单待付款 = 2,
            订单已付款 = 3,
            管家已确认完工 = 4,
            交易完成 = 5,
        }

        public enum Order_Source
        {
            装修帮后台App装修管家添加 = 1,
            PC端网友 = 2,
            pc端管家 = 3,
            管家自己推广 = 4,
        }

        public enum OrderChangeTypeID
        {
            变更金额为0 = 0,
            变更金额不为0 = 1,
        }

        public enum OrderTypeEnum
        {
            未签约 = 1,
            已签约 = 2,
            已完工 = 3,
            交纳定金 = 4,
        }

        public enum PageSoucre
        {
            房APP = 0,
            管家App = 1,
            PC端网友 = 2,
            pc端管家 = 3,
            管家自己推广 = 4,
            非管家自己推广的漏斗用户 = 5,
            潜在转意向 = 6,
            媒体渠道 = 7,
        }

        public enum PayDetailType
        {
            主材费 = 1,
            施工费 = 2,
        }

        public enum PayNote_IsLastPay
        {
            否 = 0,
            是 = 1,
        }

        public enum PayNote_PayType
        {
            设计款 = 0,
            施工款 = 1,
            主材款 = 2,
            尾款 = 3,
            定金 = 4,
            节点支付 = 5,
        }

        public enum PayState
        {
            更换优惠金额 = -6,
            更换优惠 = -5,
            切换支付方式 = -4,
            退款支付 = -3,
            支付异常 = -2,
            待付款 = -1,
            处理中 = 0,
            付款成功 = 1,
            付款失败 = 2,
            财务退回 = 3,
            已退款 = 4,
            已冻结 = 5,
            已消费 = 6,
            退款中 = 7,
            待退款 = 8,
            退款失败 = 9,
            支付中 = 10,
        }

        public enum PicCategory
        {
            原始测量图 = 0,
            平面拆改图 = 1,
            平面布局图 = 2,
            细化方案 = 3,
            装修前 = 4,
            装修中 = 5,
            开工交底 = 6,
            水电验收 = 7,
            瓦木验收 = 8,
            油漆验收 = 9,
            完工验收 = 10,
            设计合同 = 11,
            施工合同 = 12,
            主材合同 = 13,
            增项合同 = 14,
            防水验收 = 15,
            泥木验收 = 16,
            房屋拆改 = 17,
        }

        public enum PromotionRange
        {
            设计费 = 1,
            工艺费 = 2,
            主材费 = 3,
            套餐整单 = 4,
        }

        public enum PromotionRangeType
        {
            现金 = 0,
            比率 = 1,
        }

        public enum PromotionStatus
        {
            活动中 = 0,
            已结束 = 1,
            未开始 = 2,
        }

        public enum PushTaskType
        {
            短信 = 11,
            推送 = 21,
            保存推送 = 29,
            IM = 31,
            小秘书 = 41,
            装修小秘书_保存 = 49,
        }

        public enum PushType
        {
            添加跟进 = 1,
            发起支付 = 2,
            问题操作 = 3,
            预算转订单 = 4,
            其他 = 5,
        }

        public enum PushUserType
        {
            电商意向 = 1,
            电商签约 = 2,
            合作联盟意向 = 4,
            合作联盟签约 = 8,
            会员 = 16,
            _4S = 32,
            非会员 = 64,
            其它 = 128,
        }

        public enum QuoteStatus
        {
            未完成报价 = 0,
            完成报价 = 1,
        }

        public enum RealPayState
        {
            交易创建 = 0,
            提交待支付 = 1,
            扣款成功 = 2,
            扣款失败 = 3,
            交易成功 = 4,
            交易关闭 = 5,
        }

        public enum RealPayStateByKG
        {
            TRADE_CREATE = 0,
            TRADE_WAIT_PAY = 1,
            DEDUCT_SUCCESS = 2,
            DEDUCT_FAIL = 3,
            TRADE_SUCCESS = 4,
            TRADE_CLOSED = 5,
        }

        public enum RepairType
        {
            工程质量类 = 1,
            主材类 = 2,
        }

        public enum SearchState
        {
            客户姓名 = 0,
            业主手机号 = 1,
            付款流水号 = 2,
            退款操作人 = 3,
        }

        public enum SendMsgType
        {
            默认 = 0,
            B端APP跟进 = 1,
            添加意向客户 = 2,
            添加意向客户编辑短信内容 = 3,
            发送问题 = 4,
            抢客户 = 5,
            管家短链 = 6,
            意向客户发送短信记录表 = 7,
            Wap516活动 = 8,
            Wap333活动 = 9,
            线上支付定金短信提醒 = 10,
            Wap613活动 = 11,
            Wap703红包活动 = 12,
            Wap718活动 = 13,
        }

        public enum SettleOrderStatus
        {
            结算中 = 1,
            已结算 = 2,
            撤销结算 = 3,
            重新打款 = 4,
        }

        public enum Sex
        {
            女 = 0,
            男 = 1,
        }

        public enum SFJiFenOpType
        {
            量房 = 1,
            保存预算表 = 300002,
            转订单 = 300003,
            支付订单 = 300004,
            签到送积分 = 300006,
            现场签到 = 300010,
        }

        public enum ShigongState
        {
            未开工 = 0,
            开工交底 = 1,
            房屋拆改 = 2,
            水电改造 = 3,
            瓦木工程 = 4,
            油漆工程 = 5,
            施工完成 = 6,
        }

        public enum SignUserStage
        {
            开工交底 = 1,
            房屋拆改 = 2,
            水电验收 = 4,
            防水验收 = 8,
            瓦工验收 = 16,
            木工验收 = 32,
            油漆验收 = 64,
            竣工验收 = 128,
        }

        public enum StatusID
        {
            待处理 = 0,
            处理中 = 1,
            已解决 = 2,
        }

        public enum UrgentLevel
        {
            普通 = 1,
            紧急 = 2,
        }

        public enum UserRank
        {
            VIP = -2,
            A = 1,
            B = 2,
            C = 3,
            E = 5,
        }

        public enum UserSource
        {
            WAP = 1,
            APP = 2,
            PC = 3,
        }

        public enum UserType
        {
            供货商 = 1,
            产品顾问 = 2,
            工长 = 3,
            工程管家 = 4,
        }

        public enum VoucherState
        {
            未使用 = 0,
            已冻结 = 1,
            已使用 = 2,
        }

        public enum Yibao_PayState
        {
            未确认 = 0,
            支付成功 = 1,
            已结算 = 2,
            支付失败 = 3,
            已经申请退款 = 4,
            已部分退款 = 5,
            退款失败 = 6,
            已全部退款 = 7,
            订单已关闭 = 8,
            查询频率过大 = 9,
        }
    }
}
