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
            //Console.WriteLine(str.CreateSQLInsertNewEntity<BaseModel>());
            //Console.WriteLine(str.CreateSQLUpdateById<BaseModel>());

            object columnParam = new { Id=1,Name="名称",Age=10,Phone="13121135599"};
            object conditionParam = new { Id = 1, Phone =11111};

            Console.WriteLine(str.CreateSQLUpdateByProperties<BaseModel>(columnParam, conditionParam));

            string s = "Hello World!";
            this.RefStr(ref s);
            Console.WriteLine(s);

            this.OutStr(out s);
            Console.WriteLine(s);

            ToGB2312("EOP电商");
        }

        public void ToGB2312(string str)
        {
            var defaultEncode = System.Text.Encoding.Default;
            var GB2312 = System.Text.Encoding.GetEncoding("GB2312");
            byte[] buffer = GB2312.GetBytes(str);
            buffer = System.Text.Encoding.Convert(defaultEncode,GB2312,buffer);
            Console.WriteLine(GB2312.GetString(buffer));
        }

        public void RefStr(ref string str)
        {
            str = "REF Hello";
        }
        public void OutStr(out string str)
        {
            str = "OUT Hello";
        }

    
    }
}
