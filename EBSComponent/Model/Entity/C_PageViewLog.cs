using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.EbsDBModel
{
    public partial class C_PageViewLog
    {
        public int ID { get; set; }
        public Nullable<long> SoufunID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> Platform { get; set; }
        public string PageView { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string SoufunName { get; set; }
        public string OSVersion { get; set; }
        public string DeviceInfo { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    }
}
