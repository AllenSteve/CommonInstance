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
        private string STR { get; set; }

        public StringTest()
        {
            string str = "13851870675";

            //Console.WriteLine(str.IsNullOrEmpty());
            //Console.WriteLine(str.MaskPhoneNo());
            //Console.WriteLine(str.Encrypt());
            Console.WriteLine(str.CreateSQLInsertNewEntity<BaseModel>());
            Console.WriteLine(str.CreateSQLUpdateById<BaseModel>());
        }
    
    }
}
