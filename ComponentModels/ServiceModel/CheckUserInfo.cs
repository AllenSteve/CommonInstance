using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.ServiceModel
{
    public class CheckUserInfo
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserType { get; set; }

        public string IsValid { get; set; }

        public string Result { get; set; }

        public string Reason { get; set; }

        public string Interface { get; set; }
    }
}
