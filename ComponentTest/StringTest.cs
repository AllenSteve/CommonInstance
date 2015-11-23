using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;
using ComponentModels;

namespace ComponentTest
{
    public class StringTest
    {

        public StringTest()
        {
            string str = "13851870675";

            //Console.WriteLine(str.IsNullOrEmpty());
            //Console.WriteLine(str.MaskPhoneNo());
            //Console.WriteLine(str.Encrypt());
            Console.WriteLine(str.CreateInsertSQL<BaseModel>());

        }
    
    }
}
