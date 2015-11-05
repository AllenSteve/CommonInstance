using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction
{
    public class DateTimeFunction
    {
        /// <summary>
        /// 格式化日期参数
        /// </summary>
        /// <param name="dateStr">日期参数，例如201405</param>
        /// <returns>格式化后的日期参数，可以通过DateTime.Parse()来解析为日期类型，例如2014/05</returns>
        public static string FormatDateString(string dateStr)
        {
            return dateStr.Substring(0, 4) + "/" + dateStr.Substring(4);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DateTimeFunction()
        { 
        }

        /// <summary>
        /// 获取一个月份当中的第五个工作日
        /// </summary>
        /// <param name="objMonth">月份参数，例如201405</param>
        /// <returns>目标月份的第五个工作日</returns>
        public DateTime GetTheFifthWorkingDayOfAMonth(string objMonth)
        {
            //获取当月第一天的日期类型变量
            DateTime objDay = DateTime.Parse(FormatDateString(objMonth));

            for (int i = 0; i < 4; i++)
            {
                while (objDay.DayOfWeek == DayOfWeek.Sunday || objDay.DayOfWeek == DayOfWeek.Saturday)
                {
                    objDay = objDay.AddDays(1);
                }
                //判断星期五
                objDay = objDay.AddDays(objDay.DayOfWeek == DayOfWeek.Friday ? 3 : 1);
            }

            return objDay;
        }


        public void DisplayDateTime()
        {
            System.Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss"));
            System.Console.WriteLine(DateTime.Now.ToString("yyyy年MM月dd日HH時mm分ss秒"));
        }

    }
}
