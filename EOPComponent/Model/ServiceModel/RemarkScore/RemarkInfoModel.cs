using ComponentModels.EbsDBModel;
using EOPComponent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.ScoreAccumulation
{
    public class RemarkLogInfoModel : ScoreLogModel
    {
        public int FollowUpId { get; set; }
        public long OwnerSoufunId { get; set; }
        public int IsFlower { get; set; }
        public int IsUsed { get; set; }

        private static IDictionary<int, string> RemarkDictionary = RemarkLogInfoModel.GetRemarkDictionary();

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="logInfo">The log information.</param>
        public RemarkLogInfoModel(N_ServicePoint_Log log)
            : base(log)
        {
            if (log != null)
            {
                this.ParseRemarkInfo(log.CauseText);
            }
        }

        private void ParseRemarkInfo(string logInfo)
        {
            if (!string.IsNullOrEmpty(logInfo))
            {
                string[] info = logInfo.Split('|');
                this.FollowUpId = int.Parse(info[0]);
                this.OwnerSoufunId = long.Parse(info[1]);
                this.IsFlower = int.Parse(info[2]);
                this.IsUsed = int.Parse(info[3]);
            }
        }

        private static IDictionary<int, string> GetRemarkDictionary()
        {
            IDictionary<int, string> dictionary = new Dictionary<int,string>();
            dictionary.Add((int)RemarkScoreType.鸡蛋,RemarkScoreType.鸡蛋.ToString());
            dictionary.Add((int)RemarkScoreType.鲜花,RemarkScoreType.鲜花.ToString());
            return dictionary;
        }

        private bool IsFlowerRemark()
        {
            return RemarkLogInfoModel.RemarkDictionary.ContainsKey(this.IsFlower);
        }

        public bool IsUsedFlowerRemark()
        {
            return this.IsUsed == 1 && this.IsFlowerRemark();
        }

        public bool MatchesWith(CompanyFollowUpModel followInfo)
        {
            return this.CompanyId == followInfo.CompanyId 
                      && this.OwnerSoufunId == followInfo.OwnerSoufunId
                      && this.FollowUpId == followInfo.FollowId;
        }
    }
}
