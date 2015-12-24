using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.EBS
{
    public partial class PayNotice_2_0_Log
    {
        public int ID { get; set; }
        public string notify_id { get; set; }
        public string biz_id { get; set; }
        public string trade_no { get; set; }
        public string out_trade_no { get; set; }
        public string trade_state { get; set; }
        public Nullable<System.DateTime> trade_time { get; set; }
        public Nullable<decimal> trade_amount { get; set; }
        public string extra_param { get; set; }
        public string sign_type { get; set; }
        public Nullable<int> third_part_type { get; set; }
        public string third_part_id { get; set; }
        public Nullable<decimal> third_part_amount { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> IsDel { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> paid_amount { get; set; }
    }
}
