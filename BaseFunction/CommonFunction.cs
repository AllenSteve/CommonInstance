using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction
{
    public class CommonFunction
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonFunction()
        {
            //测试finally的执行次序；
            Console.WriteLine(GetInt()); 
        }

        static int GetInt()
        {
            int i = 8;
            try
            {
                i++;
                Console.WriteLine("a");
                return i;
            }
            finally
            {
                Console.WriteLine("b");
                i++;
            }
        }
    }
}
