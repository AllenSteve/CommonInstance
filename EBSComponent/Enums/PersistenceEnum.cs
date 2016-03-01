using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbsComponent.Enums
{
    public enum DatabaseEnum 
    {
        测试库只读 = 1,
        测试库读写 = 2,
        沙箱库只读 = 3,
        沙箱库读写 = 4,
        正式库只读 = 5,
        正式库读写 = 6
    }
}
