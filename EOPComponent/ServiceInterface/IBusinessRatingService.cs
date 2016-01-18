using EOPComponent.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.ServiceInterface
{
    /// <summary>
    /// 权限管理-商家等级管理（结算，权限）
    /// 权限管理-多米积分管理
    /// </summary>
    public interface IBusinessRatingService
    {
        /// <summary>
        /// 根据积分种类和积分Id获取积分数值
        /// </summary>
        /// <param name="typeId">积分类型</param>
        /// <param name="scoreId">积分Id</param>
        /// <returns>积分信息</returns>
        int? GetScoreByConfig(int typeId,int scoreId);

        /// <summary>
        /// 根据质保金充值时的金额获取积分Id配置对象
        /// </summary>
        /// <param name="dealerMargin">商户质保金金额</param>
        /// <returns>积分配置对象</returns>
        SolutionConfig GetSolutionByMargin(decimal dealerMargin);

        /// <summary>
        /// 获取服务方案对象及手续费
        /// </summary>
        /// <param name="typeId">积分类型Id</param>
        /// <param name="budgetMinima">最小预算金额</param>
        /// <returns>积分配置对象</returns>
        SolutionConfig GetSolutionByBudget(int typeId, decimal budgetMinima);

        /// <summary>
        /// 获取积分配置列表
        /// </summary>
        /// <returns>积分配置列表</returns>
        IEnumerable<ScoreConfig> GetScoreConfigList();
    }
}
