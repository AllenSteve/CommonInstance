#region
/*************************************************/
/***** Create by zoujiliang 2015/11/24 11:40:04****/
/*************************************************/
using System;
using System.Collections.Generic;
using System.Text;
namespace EOP.DapperModel.EBS
{
    public partial class Partner_CompanyExtent
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 商家ID
        /// </summary>
        public int DealerID { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 质保金
        /// </summary>
        public decimal Margin { get; set; }
        /// <summary>
        /// 预存款
        /// </summary>
        public decimal PreDeposits { get; set; }
        /// <summary>
        /// 商家评级
        /// </summary>
        public int DealerClass { get; set; }
        /// <summary>
        /// 当前积分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 累计派单
        /// </summary>
        public int OrderAmount { get; set; }
        /// <summary>
        /// 累计签约
        /// </summary>
        public int ContractAmount { get; set; }
        /// <summary>
        /// 状态（1停用，0审核）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 解决方案ID逗号拼接
        /// </summary>
        public string SolutionIDs { get; set; }
        /// <summary>
        /// 商家服务区域，用于装修预算
        /// </summary>
        public string DealerDistrict { get; set; }
        /// <summary>
        /// 商家服务区域IDs，如：1,2,3,4
        /// </summary>
        public string DealerDistrictIDs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
#endregion