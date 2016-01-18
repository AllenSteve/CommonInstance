using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.Entity
{
    public class AdminUserOperLog
    {
        public int ID { get; set; }
        public long OperSoufunID { get; set; }
        public string OperName { get; set; }
        [Description("操作类型，-1为删除；1为添加；2为编辑；4为保存。可自定义，不要冲突，自定义后添加至说明")]
        public int OperType { get; set; }
        public int IsCompleted { get; set; }
        public int OperObjectID { get; set; }
        public int TableName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CompleteTime { get; set; }
    }
}
