using BaseFunction.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class DateService : IDateService, IBaseService
    {

        public void Display()
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"));
            System.Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss"));
            System.Console.WriteLine(DateTime.Now.ToString("yyyy年MM月dd日HH時mm分ss秒"));
        }


        /// <summary>
        /// 获得每月的第五工作日
        /// </summary>
        /// <param name="month">月份字符串</param>
        /// <returns>第五工作日日期</returns>
        public DateTime GetMonthFifthWorkingDay(string month)
        {
            //获取当月第一天的日期类型变量
            DateTime objDay = DateTime.Parse(FormatDateString(month));

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

        /// <summary>
        /// 格式化日期参数
        /// </summary>
        /// <param name="dateStr">日期参数，例如201405</param>
        /// <returns>格式化后的日期参数，可以通过DateTime.Parse()来解析为日期类型，例如2014/05</returns>
        private static string FormatDateString(string dateStr)
        {
            string str = dateStr.Substring(0, 4) + "/" + dateStr.Substring(4);
            return str;
        }
    }
}
