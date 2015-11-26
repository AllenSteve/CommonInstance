using BaseFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest
{
    public class DateTimeTest
    {
        private DateTime testDateTime;
        private string testDateTimeString;

        private void InitParams()
        {
            testDateTime = new DateTime();
            testDateTimeString = string.Empty;
        }

        private void ConfigParams(DateTime objDateTime, string objDateTimeString=null)
        {
            testDateTime = objDateTime;
            if (objDateTimeString != null)
                testDateTimeString = objDateTimeString;
        }

        private void SysW(string objString)
        {
            System.Console.WriteLine(objString);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DateTimeTest()
        {
            //url字符串时间戳解析  utf-8编码
            //2015%2f11%2f3+0%3a02%3a32
            //2015/11/3 0:02:32
            //string timeStamp;

            //timeStamp= "2015%2f11%2f3+0%3a02%3a32";
            ////timeStamp = "2015/11/3 0:02:32";
            //timeStamp = timeStamp.Replace("%2f", "/").Replace("%3a", ":").Replace("+", " ");


            //Console.WriteLine(DateTime.Parse(timeStamp));
        }

        public void RunDateFunction()
        {
            Console.WriteLine(Guid.NewGuid().ToString().Replace("-",""));
            DateTimeFunction.DisplayDateTime();
        }

        public static string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("//u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }  
    }
}
