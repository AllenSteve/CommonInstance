using ComponentTest;
using System;

namespace ApplicationInstance
{
    public class BaseInstance
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Instance Is Running!");
            //new CommonModelTest().RunTransactionServiceBaseModelTest();
            //new CommonModelTest().RunEnumTest();
            //new StringTest();
            new CommonModelTest().RunParamsTest();
            new StringTest();
        }
    }
}
