using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOP.DapperModel
{
    public class DealerClassConfig
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string CityName { get; set; }
        public decimal MinMargin { get; set; }
        public decimal MaxMargin { get; set; }
        public decimal MinPreDeposits { get; set; }
        public decimal MaxPreDeposits { get; set; }
        public decimal ServiceRate { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
    }
}
