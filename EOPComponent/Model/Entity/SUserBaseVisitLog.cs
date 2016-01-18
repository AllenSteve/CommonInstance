using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public partial class SUserBaseVisitLog
    {
        public int ID { get; set; }
        public int SUserBaseID { get; set; }
        public int OperType { get; set; }
        public int IsCompleted { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
    }
}
