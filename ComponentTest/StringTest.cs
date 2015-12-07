using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;
using ComponentModels;
using System.Web;

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

            ToUTF8("EOP电商");

            string score = "+5";
            int ss = int.Parse(score);

            score = "-5";
            ss = int.Parse(score);
            DateTime dd = DateTime.Parse("1900/1/1");

            Console.WriteLine((decimal)ss);

            Console.WriteLine(dd.ToString("yyyy-MM-dd HH:mm"));

            byte[] a = new byte[2];
            a[0] = 0x01;
            a[1] = 0x00;

            Type t = a.GetType();


            LogModel log = new LogModel() 
            {
                interfaceName =null
            };

            Console.WriteLine(string.IsNullOrEmpty(log.interfaceUrl));


        }

        public void ToUnicode(string str)
        {
            // UTF8格式编码
            Encoding Unicode = Encoding.Unicode;
            Encoding GB2312 = Encoding.GetEncoding("GB2312");

            // 系统默认格式编码
            byte[] buffer1 = GB2312.GetBytes(str);
            // 转换为unicode
            byte[] buffer2 = Encoding.Convert(GB2312, Unicode, buffer1, 0, buffer1.Length);
            string strBuffer = GB2312.GetString(buffer2, 0, buffer2.Length);
            Console.WriteLine(strBuffer);
        }

        public void ToUTF8(string str)
        {
            // UTF8格式编码
            Encoding UTF8 = Encoding.UTF8;
            Encoding GB2312 = Encoding.GetEncoding("GB2312");

            //HttpUtility.UrlEncode();


            // 系统默认格式编码
            byte[] buffer1 = GB2312.GetBytes(str);
            // 转换为unicode
            byte[] buffer2 = Encoding.Convert(GB2312, UTF8, buffer1, 0, buffer1.Length);
            string strBuffer = GB2312.GetString(buffer2, 0, buffer2.Length);
            Console.WriteLine(HttpUtility.UrlEncode(strBuffer));
        }

        public void ToGB2312(string str)
        {
            //str = "骞垮憡涓戦椈";
            var defaultEncode = System.Text.Encoding.Default;
            var GB2312 = System.Text.Encoding.GetEncoding("GB2312");
            byte[] buffer = GB2312.GetBytes(str);
            buffer = System.Text.Encoding.Convert(defaultEncode,GB2312,buffer);
            Console.WriteLine(GB2312.GetString(buffer));

            byte[] buffer1 = Encoding.Default.GetBytes(str);
            byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, buffer1, 0, buffer1.Length);
            string strBuffer = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
            Console.WriteLine(strBuffer);
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
