using ComponentModels.EbsDBModel;
using ComponentORM.ORMappingTools;
using EOPComponent.Enums;
using EOPComponent.Model.Entity;
using EOPComponent.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOP.Service
{
    public class DealerService : IDealerService
    {
        private DBHelper db { get; set; }

        private IBusinessRatingService businessService { get; set; }

        public DealerService()
        {
            db = new DBHelper();
            businessService = new BusinessRatingService();
        }

        public int AddPaymentLog(PayNotice_2_0_Log log)
        {
            this.VerifyDB(DBHelper.Sqldb.OrderWrite);
            int retAddLog = db.Add<PayNotice_2_0_Log>(log);
            return retAddLog;
        }

        public int ExecuteTransactionService()
        {
            throw new NotImplementedException();
        }

        public DealerPayRecord GetSingleSettlement(string tradeNo, int type)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM DealerPayRecord ");
            object paramArray = null;

            if ((TradeNoType)type == TradeNoType.Out_Trade_NO)
            {
                sql.Append(" WHERE OutTradeNO=@OutTradeNO");
                paramArray = new { OutTradeNO = tradeNo };
                
            }
            else if ((TradeNoType)type == TradeNoType.Trade_NO)
            {
                sql.Append(" WHERE InTradeNO=@InTradeNO");
                paramArray = new { InTradeNO = tradeNo };
            }

            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            DealerPayRecord record = db.Query<DealerPayRecord>(sql.ToString(), paramArray)
                                                          .Where(o=>o.IsDel==0)
                                                          .FirstOrDefault();
            return record;
        }


        public int UpdateSettlement(DealerPayRecord settlement)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            int result = db.Update<DealerPayRecord>(settlement);
            return result;
        }


        public Partner_CompanyExtent GetDealerExtentInfo(int dealerId)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM Partner_CompanyExtent ");
            sql.Append(" WHERE DEALERID=@DEALERID");
            object paramArray = new { DEALERID = dealerId };
            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            Partner_CompanyExtent dealerExtent = db.Query<Partner_CompanyExtent>(sql.ToString(), paramArray)
                                                                             .Where(o => o.IsDel == 0)
                                                                             .FirstOrDefault();
            return dealerExtent;
        }


        public int UpdateDealerExtentInfo(Partner_CompanyExtent dealerExtent)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            int updateRET = db.Update<Partner_CompanyExtent>(dealerExtent);
            return updateRET;
        }

        private void VerifyDB(DBHelper.Sqldb useDB)
        {
            if (this.db.dbType != useDB)
            {
                this.db.SwitchDB(useDB);
            }
        }


        public int? GetScoreByType(int type, int dealerClass=0, decimal? dealerMargin = null)
        {
            int? score = null;
            int typeId = type;
            int scoreId = 0;

            // 签约订单
            if ((ScoreType)type == ScoreType.接单能力)
            {
                scoreId = 1;
            }
            // 根据保证金获取积分Id
            else if ((ScoreType)type == ScoreType.线上流水)
            {
                var solution = businessService.GetSolutionByMargin((decimal)dealerMargin);
                if (solution == null)
                {
                    return 0;
                }
                else
                {
                    scoreId = solution.SolutionID;
                }
            }
            // 根据商户等级计算派单消耗
            else if ((ScoreType)type == ScoreType.派单消耗)
            {
                scoreId = dealerClass;
            }
            // 获取积分
            score = businessService.GetScoreByConfig(typeId, scoreId);
            if (score == null)
            {        
                return 0;
            }
            else
            {
                return score;
            }            
        }

        public int GetSendOrderAmount(int dealerId)
        {
            this.VerifyDB(DBHelper.Sqldb.ServiceCenterWrite);
            string sql = "SELECT COUNT(*) FROM SendOrderInfo WHERE DealerID=@DealerID AND State IN(@State1,@State2)  AND IsDel=0";
            object param = new { DealerID = dealerId, State1 = (int)SendOrderStatusType.已派单, State2 = (int)SendOrderStatusType.已签约 };
            int count = (int)db.ExecuteScalar(sql, param);
            return count;
        }

        public int GetSignContractAmount(int dealerId)
        {
            this.VerifyDB(DBHelper.Sqldb.ServiceCenterWrite);
            string sql = "SELECT COUNT(*) FROM SendOrderInfo WHERE DealerID=@DealerID AND State=@State AND IsDel=0";
            object param = new { DealerID = dealerId, State = (int)SendOrderStatusType.已签约 };
            int count = (int)db.ExecuteScalar(sql, param);
            return count;
        }

        public Partner_Company GetCompanyInfo(int dealerId)
        {
            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            Partner_Company company = db.Query<Partner_Company>(dealerId);
            return company;
        }

        public int InsertSMS(C_SMSInfo sms)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            return db.Add<C_SMSInfo>(sms);
        }


        public SendOrderInfo GetSendOrderInfo(int dealerId, string sendOrderId)
        {
            this.VerifyDB(DBHelper.Sqldb.ServiceCenterReadOnly);
            StringBuilder sql = new StringBuilder("SELECT * FROM SendOrderInfo WHERE IsDel=0 ");
            sql.Append(" AND DealerID=@DealerID");
            sql.Append(" AND SendOrderID=@SendOrderID");
            object paramArray = new { DealerID = dealerId, SendOrderID = sendOrderId };
            SendOrderInfo record = db.Query<SendOrderInfo>(sql.ToString(), paramArray)
                                                      .FirstOrDefault();
            return record;
        }

        public SendOrderInfo GetContractSendOrderInfo(int callbackId, int dealerId)
        {
            this.VerifyDB(DBHelper.Sqldb.ServiceCenterReadOnly);
            StringBuilder sql = new StringBuilder("SELECT * FROM SendOrderInfo WHERE IsDel=0 ");
            sql.Append(" AND CallbackID=@CallbackID");
            sql.Append(" AND DealerID=@DealerID");
            object paramArray = new { CallbackID = callbackId, DealerID = dealerId };
            SendOrderInfo record = db.Query<SendOrderInfo>(sql.ToString(), paramArray)
                                                      .FirstOrDefault();
            return record;
        }

        public int UpdateSendOrderInfo(SendOrderInfo sendOrderInfo)
        {
            this.VerifyDB(DBHelper.Sqldb.ServiceCenterWrite);
            return this.db.Update<SendOrderInfo>(sendOrderInfo);
        }

        public IEnumerable<SendOrderInfo> GetSendOrderList(int id, int type=0)
        {
            this.VerifyDB(DBHelper.Sqldb.ServiceCenterReadOnly);
            StringBuilder sql = new StringBuilder("SELECT * FROM SendOrderInfo WHERE IsDel=0 ");
            object paramArray = null;

            if (type == 0)
            {
                sql.Append(" AND DealerID=@DealerID");
                paramArray = new { DealerID = id };
            }
            else
            {
                sql.Append(" AND CallbackID=@CallbackID");
                paramArray = new { CallbackID = id };
            }

            return db.Query<SendOrderInfo>(sql.ToString(), paramArray);
        }

        public bool IsDealerQualified(int dealerClass,decimal margin, decimal predeposits)
        {
            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            bool qualified;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM DealerClassConfig ");
            sql.Append(" WHERE TYPE=@TYPE AND IsDel=0 ");
            object param = new { TYPE = dealerClass};
            DealerClassConfig config = db.Query<DealerClassConfig>(sql.ToString(),param).FirstOrDefault();
            if (config == null)
            {
                qualified = false;
            }
            else
            {
                qualified = (margin >= config.MinMargin) && (predeposits >= config.MinPreDeposits);
            }
            return qualified;
        }


        public int UpdateCompanyInfo(Partner_Company company)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            int result = db.Update<Partner_Company>(company);
            return result;
        }

        public int UpdateEbsTable<T>(T entity)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            int result = db.Update<T>(entity);
            return result;
        }


        public int SetCompanyStatus(int dealerId, int status)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            object columnParam = new { Status=1};
            object conditionParam = new { ID = dealerId, IsDel = 0 };
            int result = db.Update<Partner_Company>(columnParam, conditionParam);
            return result;
        }

        public int AddCompanyScore(int dealerId, int score)
        {
            this.VerifyDB(DBHelper.Sqldb.UserWrite);
            object columnParam = new { Score = score };
            object conditionParam = new { DealerID = dealerId, IsDel = 0 };
            int result = db.Update<Partner_CompanyExtent>(columnParam, conditionParam);
            return result;
        }
    }
}
