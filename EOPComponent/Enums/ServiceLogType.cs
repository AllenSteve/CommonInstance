using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Enums
{
    public enum SendOrderStatusType
    {
        已派单 = 1,
        已退单 = 2,
        已签约 = 3
    }

    public enum ServiceLogType
    {
        商户服务日志 = 1,
        业主服务日志 = 2
    }

    public enum ServiceType
    {
        已签约 = 1,
        退单失败 = 2,
        退单成功 = 3,
        已开工 = 4,
        已竣工 = 5,
        申请退单 = 6,
        已派单 = 7,
        已回访 = 8,
        已报名 = 9,
        服务状态 = 10,
        待派单 = 11
    }

    public enum OperatorRole
    {
        客服 = 1,
        运营 = 2,
        运营经理 = 3,
        商家 = 4,
        监理 = 5
        // 待续
    }
    /// <summary>
    /// 用户等级
    /// </summary>
    public enum UserRank
    {
        VIP = -2,
        未评级 = -1,
        A_ = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4
    }
    public enum CooperationType
    {
        电商 = 0,
        EOP平台 = 1
    }
    /// <summary>
    /// 电商客源户型
    /// </summary>
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
        六居室以上 = 11
    }
    public class  EnumHelper
    {
        /// <summary>
        /// 处理用户等级A+的问题【用法：EBS.BLL.EnumBLL_.UserRankA((EBS.BLL.EnumBLL.UserRank)参数)】
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string UserRankA(EOPComponent.Enums.UserRank a)
        {
            if (a == 0)
            {
                return "A+";
            }
            return a.ToString();
        }
    }

}
