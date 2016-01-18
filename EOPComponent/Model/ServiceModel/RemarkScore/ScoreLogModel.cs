using ComponentModels.EbsDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.ScoreAccumulation
{
    public class ScoreLogModel
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public int ScoreAmount { get; set; }
        public int ScoreType { get; set; }
        public int ScoreBalance { get; set; }
        public int Code { get; set; }
        public string CodeName { get; set; }
        public int Type { get; set; }
        public DateTime CreateTime { get; set; }
        
        public ScoreLogModel(N_ServicePoint_Log log)
        {
            if (log != null)
            {
                this.Id = log.Id;
                this.CompanyId = log.SoufunId;
                this.ScoreAmount = log.ChangeAmount;
                this.ScoreType = log.ChangeType;
                this.ScoreBalance = log.Balance;
                this.Code = int.Parse(log.CauseSource);
                this.CodeName = log.CauseText;
                this.Type = log.AccountType;
                this.CreateTime = log.CreateTime;
            }
        }
    }
}
