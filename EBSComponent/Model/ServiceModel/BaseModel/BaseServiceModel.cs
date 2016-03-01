using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBS.Interface.BaseService.Model
{
    public class BaseServiceModel
    {
        /// <summary>
        /// 接口调用返回状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 接口返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 接口耗时
        /// </summary>
        public TimeSpan TimeConsuming { get; set; }
    }

    public enum ServiceStatus
    {
        请求成功 = 200,
        参数有误 = 400,
        内部异常 = 500
    }
}
