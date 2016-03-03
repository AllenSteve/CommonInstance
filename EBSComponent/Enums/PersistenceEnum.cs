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
        /// 本地测试数据库
        /// </summary>
        LOCAL_DATABASE = 0,

        /// <summary>
        /// EBS-只读
        /// </summary>
        EBS_READ = 1,

        /// <summary>
        /// EBS-读写
        /// </summary>
        EBS_WRITE = 2,

        /// <summary>
        /// EBS-测试库-只读
        /// </summary>
        EBS_READ_TEST = 3,

        /// <summary>
        /// EBS-测试库-读写
        /// </summary>
        EBS_WRITE_TEST = 4,

        /// <summary>
        /// 开放平台-只读
        /// </summary>
        OPENPLATFORM_READ = 5,

        /// <summary>
        /// 开放平台-读写
        /// </summary>
        OPENPLATFORM_WRITE = 6,

                /// <summary>
        /// 开放平台-测试库-只读
        /// </summary>
        OPENPLATFORM_READ_TEST = 7,

        /// <summary>
        /// 开放平台-测试库-读写
        /// </summary>
        OPENPLATFORM_WRITE_TEST = 8,
    }
}
