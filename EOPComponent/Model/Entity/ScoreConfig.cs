using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public class ScoreConfig
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int ScoreID { get; set; }
        public string ScoreName { get; set; }
        public string ScoreNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsDel { get; set; }

    }
}
