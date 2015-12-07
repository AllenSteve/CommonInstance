using BaseFunction.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class CommonService : ICommonService, IBaseService
    {
        //测试finally的执行次序；
        private static int GetInt()
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
