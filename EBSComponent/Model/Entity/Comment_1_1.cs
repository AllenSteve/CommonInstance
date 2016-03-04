using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSComponent.Model.Entity
{
    public partial class Comment_1_1
    {
        public int ID { get; set; }
        public string CONTENT { get; set; }
        public int ContentType { get; set; }
        public long SoufunID { get; set; }
        public string SoufunName { get; set; }
        public string TrueName { get; set; }
        public int IdentityType { get; set; }
        public long ProjectID { get; set; }
        public long ProjectSoufunID { get; set; }
        public int ProjectIdentityType { get; set; }
        public string ProjectDisplayName { get; set; }
        public string ProjectRemark { get; set; }
        public string ReplyMessageID { get; set; }
        public int CommentType { get; set; }
        public string MessageID { get; set; }
        public int IsPublic { get; set; }
        public string Remark { get; set; }
        public string PictureUrls { get; set; }
        public int IsDel { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string RootMessageID { get; set; }
        public string FunctionName { get; set; }
        public string ProjectFunctionName { get; set; }
    }
}
