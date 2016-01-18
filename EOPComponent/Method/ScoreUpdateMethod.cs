using ComponentModels.EbsDBModel;
using ComponentORM.ORMappingTools;
using EOP.Service;
using EOPComponent.Model.Entity;
using EOPComponent.Model.ScoreAccumulation;
using EOPComponent.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Enums.Method
{
    public class ScoreUpdateMethod
    {
        private static IBusinessRatingService businessService = new BusinessRatingService();
        private static IDealerService dealerService = new DealerService();
        private DBHelper db { get; set; }

        // 业主记日志积分
        public int remarkScore { get; set; }
        // 鲜花好评积分
        public int flowerScore { get; set; }
        // 鸡蛋差评积分
        public int eggScore { get; set; }
        // 验收合格积分
        public int qualifiedScore { get; set; }

        public ScoreUpdateMethod()
        {
            // 新增临时变量，这样只需要查询一次数据库
            IEnumerable<ScoreConfig> scoreList = ScoreUpdateMethod.businessService.GetScoreConfigList();
            // 业主记日志积分
            this.remarkScore = this.GetScoreByConfig(scoreList,1, 4);
            // 鲜花好评积分
            this.flowerScore = this.GetScoreByConfig(scoreList, 1, 2);
            // 鸡蛋差评积分
            this.eggScore = this.GetScoreByConfig(scoreList, 1, 3);
            // 验收合格积分
            this.qualifiedScore = this.GetScoreByConfig(scoreList, 2, 1);

            this.db = new DBHelper((int)DBHelper.Sqldb.OrderReadOnly);
        }

        /// <summary>
        /// 更新公司积分信息
        /// </summary>
        /// <param name="result">查询记录包括订单ID和公司ID</param>
        /// <param name="score">要更新的积分值</param>
        public void UpdateCompanyScore(IEnumerable<object> result, int score)
        {
            IDictionary<string, object> itemLine;
            string orderId;
            int companyId;
            int amount;
            foreach (var item in result)
            {
                itemLine = (IDictionary<string, object>)item;
                orderId = itemLine["OrderID"].ToString();
                companyId = int.Parse(itemLine["CompanyId"].ToString());
                amount = int.Parse(itemLine["AMOUNT"].ToString());
                Partner_CompanyExtent companyExtent = ScoreUpdateMethod.dealerService.GetDealerExtentInfo(companyId);
                companyExtent.Score += amount * score;
                ScoreUpdateMethod.dealerService.UpdateDealerExtentInfo(companyExtent);
            }
        }

        /// <summary>
        /// 查询业主日记反馈
        /// </summary>
        /// <returns>查询SQL</returns>
        public static string QueryFollowUp()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT F.OrderID,S.CompanyId,COUNT(*) AS AMOUNT ");
            sql.Append(" FROM Order_FollowUp AS F ");
            sql.Append(" LEFT JOIN N_Order_Base AS N ON F.OrderID=N.OrderId ");
            sql.Append(" INNER JOIN N_Order_Service AS S ON S.OrderId=F.OrderID ");
            // 管家操作
            sql.Append(" WHERE F.IdentityType =0 AND");
            // 手写跟进日志
            sql.Append(" F.LogType =0 AND ");
            sql.Append(" F.IsDel=0 AND ");
            sql.Append(" F.CreateTime BETWEEN @ST AND @ET AND");
            // EOP订单
            sql.Append(" N.OrderType=2 AND ");
            sql.Append(" N.IsDel=0 AND");
            // 家装顾问
            sql.Append(" S.FunctionId=1 AND  ");
            sql.Append(" S.IsDel=0 ");
            sql.Append(" GROUP BY F.OrderID,S.CompanyId ");
            return sql.ToString();
        }

        /// <summary>
        /// 查询工程质量验收合格记录
        /// </summary>
        /// <returns>查询SQL</returns>
        public static string QueryAcceptanceRecord()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT C.OrderId,S.CompanyId,COUNT(*) AS AMOUNT ");
            sql.Append(" FROM ConstructAudit_Log AS C ");
            sql.Append(" LEFT JOIN N_Order_Base AS N ON C.OrderID=N.OrderId ");
            sql.Append(" INNER JOIN N_Order_Service AS S ON S.OrderId=C.OrderId  ");
            sql.Append(" WHERE C.Status=1 AND  ");
            sql.Append(" C.IsDel=0 AND  ");
            sql.Append(" C.JianLiConstructAuditTime BETWEEN @ST AND @ET AND  ");
            // EOP订单
            sql.Append(" N.OrderType=2 AND ");
            sql.Append(" N.IsDel=0 AND  ");
            // 家装顾问
            sql.Append(" S.FunctionId=1 AND   ");
            sql.Append(" S.IsDel=0 ");
            sql.Append(" GROUP BY C.OrderId,S.CompanyId  ");
            return sql.ToString();
        }

        private int GetScoreByConfig(IEnumerable<ScoreConfig> scoreList, int typeId, int scoreId)
        {
            try
            {
                string scoreNumber = scoreList.Where(o => o.TypeID == typeId && o.ScoreID == scoreId && o.IsDel == 0)
                                                                .Select(o => o.ScoreNumber)
                                                                .FirstOrDefault();
                int score = 0;
                if (!string.IsNullOrEmpty(scoreNumber))
                {
                    score = int.Parse(scoreNumber);
                }
                return score;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 全表扫描跟进表Order_FollowUp
        /// </summary>
        /// <returns>查询字符串</returns>
        public static string QueryFollowUpCompanyList()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT F.ID AS FollowId");
            sql.Append(" ,F.IsFreshFlowers AS IsFlower");
            sql.Append(" ,N.SoufunId AS OwnerSoufunId");
            sql.Append(" ,S.CompanyId AS CompanyId");
            sql.Append(" FROM   Order_FollowUp AS F");
            sql.Append(" LEFT JOIN N_Order_Base AS N ON F.OrderID = N.OrderId");
            sql.Append(" INNER JOIN N_Order_Service AS S ON S.OrderId = F.OrderID");
            sql.Append(" INNER JOIN N_ServicePoint_Log AS P ON P.SoufunId = S.CompanyId");
            sql.Append(" WHERE  F.IdentityType = 0");
            sql.Append(" AND F.IsFreshFlowers IN(1,2)");
            sql.Append(" AND F.LogType = 0");
            sql.Append(" AND F.IsDel = 0");
            sql.Append(" AND N.OrderType = 2");
            sql.Append(" AND N.IsDel = 0");
            sql.Append(" AND S.FunctionId = 1");
            sql.Append(" AND S.IsDel = 0");
            sql.Append(" GROUP BY F.ID");
            sql.Append(" ,F.IsFreshFlowers");
            sql.Append(" ,N.SoufunId");
            sql.Append(" ,S.CompanyId ");
            return sql.ToString();
        }

        /// <summary>
        /// 查询最新变更的跟进记录
        /// </summary>
        /// <returns>查询SQL</returns>
        public static string QueryFollowUpLogCompanyList()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT O.FollowUpID AS FollowId");
            sql.Append(" ,O.ID AS ID");
            sql.Append(" ,O.SoufunID AS OwnerSoufunId");
            sql.Append(" ,O.IsFlower AS IsFlower");
            sql.Append(" ,A.CompanyId AS CompanyId");
            sql.Append(" FROM OrderFollowUpEvaluateLog AS O");
            sql.Append(" INNER JOIN Admin_UserInfo AS A ON O.OperSoufunID = A.SoufunId");
            sql.Append(" WHERE O.IsFlower IN(1,2)");
            sql.Append("       AND O.ID =");
            sql.Append("       (SELECT MAX(O1.ID)");
            sql.Append("        FROM OrderFollowUpEvaluateLog AS O1");
            sql.Append("        WHERE O1.FollowUpID=O.FollowUpID)");
            // 连接查询时加上以下两个字段会对查询性能造成一定影响，建议加上非聚集索引
            sql.Append("       AND A.Status=1 ");
            sql.Append("       AND A.IsDel =0 ");
            sql.Append(" GROUP BY O.ID ");
            sql.Append(" ,O.FollowUpID");
            sql.Append(" ,O.SoufunID ");
            sql.Append(" ,O.IsFlower ");
            sql.Append(" ,A.CompanyId");
            sql.Append(" HAVING O.ID = MAX(O.ID)");
            sql.Append(" ORDER BY O.FollowUpID");
            sql.Append(" ,O.ID DESC");
            sql.Append(" ,O.SoufunID ");
            sql.Append(" ,O.IsFlower ");
            sql.Append(" ,A.CompanyId");
            return sql.ToString();
        }

        /// <summary>
        /// Queries the remark list.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>System.String.</returns>
        public static string QueryRemarkListByOrderId()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT F.ID");
            sql.Append(",F.OrderID");
            sql.Append(",N.SoufunId");
            sql.Append(",S.CompanyId");
            sql.Append(",P.CauseText");
            sql.Append(",P.CreateTime");
            sql.Append(" FROM   Order_FollowUp AS F");
            sql.Append(" LEFT JOIN N_Order_Base AS N ON F.OrderID = N.OrderId");
            sql.Append(" INNER JOIN N_Order_Service AS S ON S.OrderId = F.OrderID");
            sql.Append(" INNER JOIN N_ServicePoint_Log AS P ON P.SoufunId = S.CompanyId");
            sql.Append("  WHERE  F.IdentityType = 0 ");
            sql.Append(" AND F.LogType = 0 ");
            sql.Append(" AND F.IsDel = 0 ");
            sql.Append(" AND N.OrderType = 2 ");
            sql.Append(" AND N.IsDel = 0 ");
            sql.Append(" AND S.FunctionId = 1 ");
            sql.Append(" AND S.IsDel = 0");
            sql.Append(" AND P.IsDel = 0");
            sql.Append(" AND F.OrderID = @OrderID");
            return sql.ToString();
        }

        /// <summary>
        /// 获取鲜花鸡蛋积分日志信息.
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <returns>评价列表</returns>
        public List<RemarkLogInfoModel> GetRemarkList(int companyId)
        {
            object param = new { SoufunId = companyId };
            IEnumerable<N_ServicePoint_Log> list = db.Query<N_ServicePoint_Log>(ScoreUpdateMethod.QueryScoreLogByCompanyId(), param);
            List<RemarkLogInfoModel> remarkList = list.Select(o => new RemarkLogInfoModel(o)).ToList();
            return remarkList;
        }

        private static string QueryScoreLogByCompanyId()
        {
            return @"SELECT * FROM N_ServicePoint_Log WHERE IsDel=0 AND AccountType =2 AND SoufunId=@SoufunId";
        }

        public IEnumerable<object> Query(string sql, object param = null)
        {
            return this.db.Query(sql, param);
        }

        /// <summary>
        /// 1：公司列表-未去重
        /// </summary>
        /// <returns>List{CompanyFollowUpModel}.</returns>
        public List<CompanyFollowUpModel> GetFollowUpCompanyList()
        {
            IEnumerable<object> list = this.Query(ScoreUpdateMethod.QueryFollowUpLogCompanyList());
            List<CompanyFollowUpModel> companyList = list.Select(o=>new CompanyFollowUpModel(o)).ToList();
            return companyList;
        }

        /// <summary>
        /// 1：公司列表-未去重
        /// </summary>
        /// <returns>List{CompanyFollowUpModel}.</returns>
        public List<CompanyFollowUpModel> GetFollowUpLogCompanyList()
        {
            IEnumerable<object> list = this.Query(ScoreUpdateMethod.QueryFollowUpLogCompanyList());
            List<CompanyFollowUpModel> companyList = list.Select(o => new CompanyFollowUpModel(o)).ToList();
            return companyList;
        }

        public int InsertOrderFollowUpEvaluateLogLog(long ownerSoufunId, int followId, int isFlower, long companyStaffSoufunId)
        {
            OrderFollowUpEvaluateLog log = new OrderFollowUpEvaluateLog()
            {
                // 业主搜房ID
                SoufunID = ownerSoufunId,
                FollowUpID = followId,
                IsFlower = isFlower,
                CreateTime = DateTime.Now,
                IsDel = 0,
                OperSoufunID = companyStaffSoufunId
            };
            return this.db.Add(log);
        }

        public int InsertServicePointLog(int companyId, int score, int scoreType, int balance, string sourceCode, int followId, long soufunId, int scoreString, int state)
        {
            N_ServicePoint_Log log = new N_ServicePoint_Log()
            {
                SoufunId = companyId,
                ChangeAmount = score,
                ChangeType = scoreType,
                Balance = balance,//积分余额
                CauseSource = sourceCode,//原因编码
                //格式，followupId|ownersoufunId|鸡蛋：1，鲜花：2|未扣除积分：0，已扣除积分1
                CauseText = string.Format("{0}|{1}|{2}|{3}", followId, soufunId, scoreString, state),
                AccountType = 2,//公司
                CreateTime = DateTime.Now,
                IsDel = 0
            };
            return this.db.Add(log);
        }

        public object CreateQueryParam()
        {
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddMinutes(-10);
            return new { ST = startTime, ET = endTime };
        }

        /// <summary>
        /// 获取公司积分操作列表
        /// </summary>
        /// <param name="companyList">1：公司列表-未去重</param>
        /// <returns>List{CompanyScoreOperationModel}.</returns>
        public List<CompanyScoreOperationModel> CreateScoreOperationList(List<CompanyFollowUpModel> companyList)
        {
            // 1：公司列表-去重得到
            IEnumerable<int> dictinctCompanyList = companyList.Select(o => o.CompanyId).Distinct().ToArray();

            // 2：鲜花鸡蛋评价日志列表-由公司列表获得-大集合
            List<RemarkLogInfoModel> remarkLogList = this.GetRemarkList(dictinctCompanyList).FindAll(n => n.IsUsedFlowerRemark());

            // 3：匹配并统计积分：
            List<CompanyScoreOperationModel> companyScoreOperationList = new List<CompanyScoreOperationModel>();
            foreach (var followInfo in companyList)
            {
                CompanyScoreOperationModel operation = new CompanyScoreOperationModel(followInfo.CompanyId, followInfo.OwnerSoufunId, followInfo.FollowId);
                RemarkLogInfoModel matchRemarkLog = remarkLogList.Where(o => o.MatchesWith(followInfo))
                                                                                                         .OrderByDescending(m => m.Id)
                                                                                                         .FirstOrDefault();
                // 没有积分结算记录
                if (matchRemarkLog == null)
                {
                    operation.Calculate(followInfo);
                }
                else
                {
                    operation.Calculate(followInfo, matchRemarkLog);
                }

                if (operation.Enable)
                {
                    companyScoreOperationList.Add(operation);
                }
            }
            return companyScoreOperationList;
        }

        public List<CompanyScoreAccumulationModel> GetScoreAccumulationList(List<CompanyScoreOperationModel> scoreOperationList)
        {
            List<CompanyScoreAccumulationModel> list = new List<CompanyScoreAccumulationModel>();
            list = (from operation in scoreOperationList
                    group operation by new { operation.CompanyId } into groupList
                    orderby groupList.Key.CompanyId descending
                    select new CompanyScoreAccumulationModel
                    {
                        CompanyId = groupList.Key.CompanyId,
                        LastEggCount = groupList.Count(o => o.IsEggRemark(o.LastScoreType)),
                        LastFlowerCount = groupList.Count(o => o.IsFlowerRemark(o.LastScoreType)),
                        CurrentEggCount = groupList.Count(o => o.IsEggRemark(o.CurrentScoreType)),
                        CurrentFlowerCount = groupList.Count(o => o.IsFlowerRemark(o.CurrentScoreType)),
                    }).OrderBy(o => o.CompanyId).ToList();
            return list;
        }

        /// <summary>
        /// Gets the score accumulation list.
        /// </summary>
        /// <param name="companyList">1：公司列表-未去重</param>
        /// <returns>List{CompanyScoreAccumulationModel}.</returns>
        public List<CompanyScoreAccumulationModel> GetScoreAccumulationList(List<CompanyFollowUpModel> companyList)
        {
            List<CompanyScoreOperationModel> scoreOperationList = this.CreateScoreOperationList(companyList);
            return this.GetScoreAccumulationList(scoreOperationList);
        }

        /// <summary>
        /// 获得积分变更记录表-用于单元测试
        /// </summary>
        /// <returns>积分变更记录表</returns>
        public List<CompanyScoreAccumulationModel> GetScoreAccumulationList()
        {
            List<CompanyScoreOperationModel> scoreOperationList = this.CreateScoreOperationList();
            List<CompanyScoreAccumulationModel> scoreAccumulationList = new List<CompanyScoreAccumulationModel>();
            if (scoreOperationList.Count>0)
            {
                scoreAccumulationList = this.GetScoreAccumulationList(scoreOperationList);
            }
            return scoreAccumulationList;
        }

        /// <summary>
        /// 此处更新积分--该方法效率较低，不建议采用
        /// </summary>
        /// <param name="scoreAccumulationList">公司积分统计列表</param>
        public void UpdateEachCompanyScore(List<CompanyScoreAccumulationModel> scoreAccumulationList)
        {
            foreach (var company in scoreAccumulationList)
            {
                Partner_CompanyExtent companyExtent = ScoreUpdateMethod.dealerService.GetDealerExtentInfo(company.CompanyId);
                companyExtent.Score += company.GetScoreAccumulation(this.eggScore,this.flowerScore);
                ScoreUpdateMethod.dealerService.UpdateDealerExtentInfo(companyExtent);
            }
        }

        public List<CompanyScoreOperationModel> CreateScoreOperationList()
        {
            List<CompanyFollowUpModel> companyList = this.GetFollowUpCompanyList();
            List<CompanyScoreOperationModel> scoreOperationList = new List<CompanyScoreOperationModel>();
            if (companyList.Count > 0)
            {
                scoreOperationList = this.CreateScoreOperationList(companyList);
            }
            return scoreOperationList;
        }

        public void CreateEachCompanyScoreOperationLog(List<CompanyScoreOperationModel> scoreOperationList)
        {
            foreach(var operation in scoreOperationList)
            {
                this.db.Add(operation.CreateServicePointLogByOperation(this.eggScore,this.flowerScore));
            }
        }

        private static string QueryScoreLogByCompanyIdList()
        {
            return @"SELECT * FROM N_ServicePoint_Log WHERE IsDel=0 AND AccountType =2 AND SoufunId IN @SoufunIds";
        }

        /// <summary>
        /// 获取鲜花鸡蛋积分日志信息.
        /// </summary>
        /// <param name="companyIdList">公司Id列表</param>
        /// <returns>评价列表</returns>
        public List<RemarkLogInfoModel> GetRemarkList(IEnumerable<int> companyIdList)
        {
            object param = new { SoufunIds = companyIdList };
            IEnumerable<N_ServicePoint_Log> list = db.Query<N_ServicePoint_Log>(ScoreUpdateMethod.QueryScoreLogByCompanyIdList(), param);
            List<RemarkLogInfoModel> remarkList = list.Select(o => new RemarkLogInfoModel(o)).ToList();
            return remarkList;
        }

        public static string UpdateCompanyByCompanyScoreList()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Partner_CompanyExtent");
            sql.Append(" SET Score +=");
            sql.Append(" (CASE DealerID WHEN -1 THEN 0");
            sql.Append(" @CompanyScoreCase");
            sql.Append(" ELSE 0 END)");
            sql.Append(" WHERE DealerID IN @CompanyIds");
            return sql.ToString();
        }

        private string CompanyScoreCase(List<CompanyScoreAccumulationModel> scoreAccumulationList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (var company in scoreAccumulationList)
            {
                sql.Append(string.Format(" WHEN {0} THEN {1}", company.CompanyId, company.GetScoreAccumulation(this.eggScore, this.flowerScore)));
            }
            return sql.ToString();
        }

        /// <summary>
        /// 此处更新积分
        /// </summary>
        /// <param name="scoreAccumulationList">公司积分统计列表</param>
        public void UpdateBatchCompanyScore(List<CompanyScoreAccumulationModel> scoreAccumulationList)
        {
            string companyScoreCase = this.CompanyScoreCase(scoreAccumulationList);
            string updateSQL = ScoreUpdateMethod.UpdateCompanyByCompanyScoreList().Replace("@CompanyScoreCase", companyScoreCase);
            object param = new { CompanyIds = scoreAccumulationList.Select(o=>o.CompanyId).ToArray() };
            int updateResult = this.db.ExecuteSQL(updateSQL, param);
        }
    }
}
