using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public class SolutionConfig
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public decimal SolutionMinBudget { get; set; }
        public decimal SolutionMaxBudget { get; set; }
        public decimal InformationFees { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
        public int SolutionID { get; set; }

    }
}
