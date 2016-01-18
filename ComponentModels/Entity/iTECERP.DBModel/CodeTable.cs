using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ComponentModels.MyDBModel.iTECERP
{
    [Table(Name = "CodeTable")]
    public partial class CodeTable
    {
        public int Id { get; set; }
        public string SysCode { get; set; }
        public string SysNo { get; set; }
        public string Name { get; set; }
        public string Tname { get; set; }
        public string Cname { get; set; }
        public string Description { get; set; }
        public int OtherValue1 { get; set; }
        public decimal OtherValue2 { get; set; }
        public string ParentCode { get; set; }
        public string Domain { get; set; }
        public int IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int State { get; set; }
    }
}
