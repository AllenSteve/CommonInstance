using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.ScoreAccumulation
{
    public class CompanyFollowUpModel
    {
        public int FollowId { get; set; }
        public long OwnerSoufunId { get; set; }
        public int IsFlower { get; set; }
        public int CompanyId { get; set; }

        public CompanyFollowUpModel()
        { 
        }

        /// <summary>
        /// 构造函数，解析Dapper查询结果记录行（DapperRow类型）
        /// </summary>
        /// <param name="record">查询记录</param>
        public CompanyFollowUpModel(object record)
        {
            if (record != null)
            {
                var row = (IDictionary<string, object>)record;
                this.FollowId = int.Parse(row["FollowId"].ToString());
                this.OwnerSoufunId = long.Parse(row["OwnerSoufunId"].ToString());
                this.IsFlower = int.Parse(row["IsFlower"].ToString());
                this.CompanyId = int.Parse(row["CompanyId"].ToString());
            }
        }
    }
}
