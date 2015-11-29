using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOP.DapperModel
{
   public class SolutionConfig
    {
        public int ID { get; set; }
        public string TypeID { get; set; }
        public int SolutionMinBudget { get; set; }
        public int SolutionMaxBudget { get; set; }
        public int InformationFees { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsDel { get; set; }

    }
}
