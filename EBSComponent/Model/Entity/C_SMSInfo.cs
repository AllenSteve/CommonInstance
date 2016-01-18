using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class C_SMSInfo
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public long SoufunId { get; set; }
        public string Phone { get; set; }
        public string SMSChannel { get; set; }
        public string SMSContent { get; set; }
        public System.DateTime SendTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int SendState { get; set; }
        public int IsDel { get; set; }
    }
}
