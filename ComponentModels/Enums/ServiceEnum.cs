using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.Enums
{
    public enum DatabaseType
    {
        /// <summary>
        /// 用户只读库
        /// </summary>
        UserReadOnly = 1,
        /// <summary>
        /// 用户写库
        /// </summary>
        UserWrite = 2,
        /// <summary>
        /// 订单只读库
        /// </summary>
        OrderReadOnly = 3,
        /// <summary>
        /// 订单写库
        /// </summary>
        OrderWrite = 4,
        /// <summary>
        /// 服务中心只读库
        /// </summary>
        ServiceCenterReadOnly = 5,
        /// <summary>
        /// 服务中心写库
        /// </summary>
        ServiceCenterWrite = 6
    }
}
