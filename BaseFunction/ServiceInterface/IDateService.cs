using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.ServiceInterface
{
    public interface IDateService
    {
        /// <summary>
        /// 参照页面：https://technet.microsoft.com/zh-cn/library/bb882581(VS.95).aspx
        /// </summary>
        void Display();

        /// <summary>
        /// 获取一个月份当中的第五个工作日
        /// </summary>
        /// <param name="objMonth">月份参数，例如201405</param>
        /// <returns>目标月份的第五个工作日</returns>
        DateTime GetMonthFifthWorkingDay(string month);
    }
}
