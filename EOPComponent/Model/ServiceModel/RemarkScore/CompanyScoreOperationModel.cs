using ComponentModels.EbsDBModel;
using EOPComponent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.ScoreAccumulation
{
    public class CompanyScoreOperationModel
    {
        public int CompanyId { get; set; }
        public long OwnerSoufunId { get; set; }
        public int FollowUpId { get; set; }
        public int LastScoreType { get; set; }
        public int LastScore { get; set; }
        public int CurrentScoreType { get; set; }
        public int CurrentScore { get; set; }
        public int OperationScore { get; set; }
        public bool Enable 
        {
            get
            {
                return !(this.CurrentScoreType == this.LastScoreType);
            }
        }
        // 是否是第一次更新积分
        public bool IsCreate { get; set; }

        public CompanyScoreOperationModel()
        {
        }

        public CompanyScoreOperationModel(int companyId,long soufunId,int followId)
        {
            this.CompanyId = companyId;
            this.OwnerSoufunId = soufunId;
            this.FollowUpId = followId;
        }

        public CompanyScoreOperationModel(CompanyScoreOperationModel operation)
        {
            this.CompanyId= operation.CompanyId;
            this.LastScore = operation.LastScore;
            this.LastScoreType = operation.LastScoreType;
            this.CurrentScore = operation.CurrentScore;
            this.CurrentScoreType = operation.CurrentScoreType;
            this.OperationScore = operation.OperationScore;
        }

        public CompanyScoreOperationModel(object record,int score)
        {
            if (record != null)
            {
                var row = (IDictionary<string, object>)record;
                //string orderId = row["OrderID"].ToString();
                this.CompanyId = int.Parse(row["CompanyId"].ToString());
                this.OperationScore = int.Parse(row["AMOUNT"].ToString()) * score;
            }
        }

        public bool IsEggRemark(int remarkType)
        {
            return remarkType == (int)RemarkScoreType.鸡蛋;
        }

        public bool IsFlowerRemark(int remarkType)
        {
            return remarkType == (int)RemarkScoreType.鲜花;
        }

        /// <summary>
        /// 没有积分结算记录，第一次执行积分计算
        /// </summary>
        /// <param name="followInfo">The follow information.</param>
        public void Calculate(CompanyFollowUpModel followInfo)
        {
            // 新增积分操作日志--该操作统一在后续操作中进行；
            // 计算上次获得积分
            this.LastScoreType = (int)RemarkScoreType.默认值不操作;
            // 计算本次获得积分
            this.CurrentScoreType = followInfo.IsFlower;
            // 第一次更新积分的标记
            this.IsCreate = true;
        }

        /// <summary>
        /// 有积分结算记录，执行积分计算
        /// </summary>
        /// <param name="followInfo">跟进信息</param>
        /// <param name="remarkLog">The remark log.</param>
        public void Calculate(CompanyFollowUpModel followInfo,RemarkLogInfoModel remarkLog)
        {
            // 计算上次结算的积分类型
            this.LastScoreType = remarkLog.IsFlower;
            // 计算本次结算的积分类型
            this.CurrentScoreType = followInfo.IsFlower;
            // 非第一次更新积分的标记
            this.IsCreate = false;
        }

        private void CreateOperationScore(int eggScore,int flowerScore)
        {
            this.CurrentScore = this.IsEggRemark(this.CurrentScoreType) ? eggScore : flowerScore;
            this.LastScore = this.IsEggRemark(this.LastScoreType) ? eggScore : flowerScore;
        }

        public N_ServicePoint_Log CreateServicePointLogByOperation(int eggScore, int flowerScore)
        {
            this.CreateOperationScore(eggScore, flowerScore);
            N_ServicePoint_Log log = new N_ServicePoint_Log()
            {
                SoufunId = this.CompanyId,
                ChangeAmount = this.CurrentScore,
                ChangeType = this.CurrentScoreType,
                Balance = -1,
                CauseSource = "-1",
                //格式，followupId|ownersoufunId|鸡蛋：1，鲜花：2|未扣除积分：0，已扣除积分1
                CauseText = string.Format("{0}|{1}|{2}|{3}", this.FollowUpId, this.OwnerSoufunId, this.CurrentScoreType, 1),
                AccountType = 2,
                CreateTime = DateTime.Now,
                IsDel = 0
            };
            return log;
        }
    }
}
