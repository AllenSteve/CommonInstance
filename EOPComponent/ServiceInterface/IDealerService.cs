using ComponentModels.EbsDBModel;
using EOPComponent.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.ServiceInterface
{
    /// <summary>
    /// 商家管理-商家列表
    /// 商家管理-商家管理详情页
    /// 商家页面-账号管理
    /// 商家页面-提现页面
    /// 商家管理-充值页面
    /// </summary>
    public interface IDealerService
    {

        /// <summary>
        /// 新增支付日志
        /// </summary>
        /// <param name="log">支付日志对象</param>
        /// <returns>新增日志记录数：1-新增成功，0-新增失败</returns>
        int AddPaymentLog(PayNotice_2_0_Log log);

        /// <summary>
        /// Executes the transaction service.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int ExecuteTransactionService();

        /// <summary>
        /// 根据不同的积分类型获取积分数值
        /// </summary>
        /// <param name="type">积分类型</param>
        /// <param name="dealerClass">商户等级：仅在派单消耗时用于根据商户等级计算派单消耗</param>
        /// <param name="dealerMargin">商户质保金：质保金充值时根据保证金金额获取积分Id</param>
        /// <returns>积分数值</returns>
        int? GetScoreByType(int type,int dealerClass,decimal? dealerMargin =null);

        /// <summary>
        /// 根据交易号和交易类型获取结算信息
        /// </summary>
        /// <param name="tradeNo">交易号</param>
        /// <param name="type">交易类型</param>
        /// <returns>结算信息对象</returns>
        DealerPayRecord GetSingleSettlement(string tradeNo, int type);

        /// <summary>
        /// 根据商户号-DealerID获取商户扩展信息
        /// </summary>
        /// <param name="dealerId">商户号</param>
        /// <returns>商户扩展信息</returns>
        Partner_CompanyExtent GetDealerExtentInfo(int dealerId);

        /// <summary>
        /// 更新结算信息
        /// </summary>
        /// <param name="settlement">结算信息对象</param>
        /// <returns>更新记录数：1-成功，0-失败</returns>
        int UpdateSettlement(DealerPayRecord settlement);

        /// <summary>
        /// 更新商户扩展信息
        /// </summary>
        /// <param name="dealerExtent">商户扩展信息</param>
        /// <returns>更新记录数：1-成功，0-失败</returns>
        int UpdateDealerExtentInfo(Partner_CompanyExtent dealerExtent);

        /// <summary>
        /// 获取指定商家的派单数量
        /// </summary>
        /// <param name="dealerId">商家Id</param>
        /// <returns>该商家的派单数量</returns>
        int GetSendOrderAmount(int dealerId);

        /// <summary>
        /// 获取指定商家的签约数量
        /// </summary>
        /// <param name="dealerId">商家Id</param>
        /// <returns>该商家的签约数量</returns>
        int GetSignContractAmount(int dealerId);

        /// <summary>
        /// 获取商家信息
        /// </summary>
        /// <param name="dealerId">商家Id</param>
        /// <returns>商家信息</returns>
        Partner_Company GetCompanyInfo(int dealerId);

        /// <summary>
        /// 向短信表插入短信
        /// </summary>
        /// <param name="sms">短信对象</param>
        /// <returns>插入记录数：1-成功，0-失败</returns>
        int InsertSMS(C_SMSInfo sms);

        /// <summary>
        /// 根据商家Id和派单号获取一条派单信息
        /// </summary>
        /// <param name="dealerId">商家Id</param>
        /// <param name="sendOrderId">派单号</param>
        /// <returns>SendOrderInfo派单信息对象.</returns>
        SendOrderInfo GetSendOrderInfo(int dealerId, string sendOrderId);

        /// <summary>
        /// 根据回访Id和商家Id获取签约订单的派单信息
        /// </summary>
        /// <param name="callbackId">回访Id</param>
        /// <param name="dealerId">商家Id</param>
        /// <returns>SendOrderInfo派单信息对象.</returns>
        SendOrderInfo GetContractSendOrderInfo(int callbackId,int dealerId);

        /// <summary>
        /// 根据商户号或回访号查询派单信息
        /// </summary>
        /// <param name="id">商户dealerId或者回访表中的callbackId</param>
        /// <param name="type">默认0查询dealerId,其他查询callbackId</param>
        /// <returns>派单信息列表</returns>
        IEnumerable<SendOrderInfo> GetSendOrderList(int id,int type);

        /// <summary>
        /// 更新派单信息
        /// </summary>
        /// <param name="sendOrderInfo">派单信息对象</param>
        /// <returns>更新数量：1-成功，0-失败</returns>
        int UpdateSendOrderInfo(SendOrderInfo sendOrderInfo);

        /// <summary>
        /// 验证商家是否满足开户条件
        /// </summary>
        /// <param name="dealerClass">商家等级</param>
        /// <param name="margin">质保金金额</param>
        /// <param name="predeposits">预存款金额</param>
        /// <returns><c>true</c>满足开户条件; 不满足开户条件, <c>false</c>.</returns>
        bool IsDealerQualified(int dealerClass,decimal margin,decimal predeposits);

        /// <summary>
        /// 更新商家公司信息
        /// </summary>
        /// <param name="company">公司信息对象</param>
        /// <returns>更新数量：1-成功，0-失败</returns>
        int UpdateCompanyInfo(Partner_Company company);

        /// <summary>
        /// 更新EBS数据库相关表
        /// </summary>
        /// <typeparam name="T">表名称</typeparam>
        /// <param name="entity">更新对象</param>
        /// <returns>更新数量：1-成功，0-失败</returns>
        int UpdateEbsTable<T>(T entity);

        /// <summary>
        /// 更新指定商户号的商户状态
        /// </summary>
        /// <param name="dealerId">商户Id</param>
        /// <param name="status">商户状态值</param>
        /// <returns>更新数量：1-成功，0-失败</returns>
        int SetCompanyStatus(int dealerId, int status);

        /// <summary>
        /// 更新指定商户号的商户积分
        /// </summary>
        /// <param name="dealerId">商户Id</param>
        /// <param name="score">积分数</param>
        /// <returns>更新数量：1-成功，0-失败</returns>
        int AddCompanyScore(int dealerId, int score);
    }
}
