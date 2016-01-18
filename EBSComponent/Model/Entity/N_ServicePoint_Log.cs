using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class N_ServicePoint_Log
    {
        public int Id { get; set; }
        public long SoufunId { get; set; }
        public int ChangeAmount { get; set; }
        public int ChangeType { get; set; }
        public int Balance { get; set; }
        public string CauseSource { get; set; }
        public string CauseText { get; set; }
        public int AccountType { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDel { get; set; }
    }
}
