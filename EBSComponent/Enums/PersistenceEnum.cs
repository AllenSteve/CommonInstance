using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbsComponent.Enums
{
    /// <summary>
    /// 数据库枚举
    /// </summary>
    public enum DatabaseEnum 
    {
        /// <summary>
        /// 只读库
        /// </summary>
        EBS_READ = 1,

        /// <summary>
        /// 读写库
        /// </summary>
        EBS_WRITE = 2,

        /// <summary>
        /// 测试库-只读
        /// </summary>
        EBS_READ_TEST = 3,

        /// <summary>
        /// 测试库-读写
        /// </summary>
        EBS_WRITE_TEST = 4
    }
}
