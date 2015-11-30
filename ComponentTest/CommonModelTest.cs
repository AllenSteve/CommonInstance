using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;
using ComponentModels.Enums;
using System.Reflection;

namespace ComponentTest
{
    public class CommonModelTest
    {
        TransactionServiceBaseModel transactionServiceBaseModel { get; set; }

        TransactionServiceBusinessModel transactionServiceBusinessModel { get; set; }

        public CommonModelTest()
        { 
        }

        public void RunEnumTest()
        {
            DatabaseType type = DatabaseType.OrderReadOnly;

            int zeroDB = 0;
            int userReadOnlyDB = 1;

            DatabaseType zero = (DatabaseType)zeroDB;

            Console.WriteLine(type);
            Console.WriteLine((int)type);
            Console.WriteLine((DatabaseType)zeroDB);
            Console.WriteLine(zeroDB);
            Console.WriteLine(userReadOnlyDB);

            Console.WriteLine("遍历枚举中的值");
            // 遍历枚举中的值
            foreach (DatabaseType item in Enum.GetValues(typeof(DatabaseType)))
            {
                Console.WriteLine((int)item);
            }

            Console.WriteLine("遍历枚举中的值");
            // 遍历枚举中的名称
            foreach (string item in Enum.GetNames(typeof(DatabaseType)))
            {
                Console.WriteLine(item);
            }

            // 枚举数组测试
            Console.WriteLine("枚举数组测试");
            var arr = Enum.GetValues(typeof(DatabaseType));
            Console.WriteLine(arr.Length);
            Console.WriteLine((int)arr.GetValue(0));
            for(int i=0;i<arr.Length;i++)
            {
                Console.WriteLine((int)arr.GetValue(i));
            }
                




        }

        public void RunTransactionServiceBaseModelTest()
        {
            TransactionServiceBusinessModel model = null;
            model = new TransactionServiceBusinessModel("return_url",
                                                                                      "soufunId",
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      "title",
                                                                                      "subject: EOP DealerName|DealerID|Type|Amount",
                                                                                      "extra_param: DealerID|tradeType|backurl"
                                                                                      );
            var query = model.ToQueryString();


            //Console.WriteLine("queryStr:" + query);
            var extra_param = model.extra_param.Split('|');

            var subject = model.subject.Split('|');

            Console.WriteLine(extra_param[0]);
            Console.WriteLine(extra_param[1]);
            Console.WriteLine(extra_param[2]);

            Console.WriteLine("DealerID:" + subject[1]);
            Console.WriteLine("Type:"+subject[2]);
            Console.WriteLine("Amount:" + subject[3]);
        }

        public void RunParamsTest()
        {
            string a = null;
            DateTime date = DateTime.Now;
            object param = new { Id = 1, Name = "名称", NullColumn = a, Date = date };
            Type type = param.GetType();
            PropertyInfo[] properties = type.GetProperties();
            object value = null;
            for (int i = 0; i < properties.Length; ++i)
            {
                value = properties[i].GetValue(param,null);
                //if (value != null && !string.IsNullOrEmpty(value.ToString().Trim()))
                //{
                if (value==null)
                {
                    Console.WriteLine("{0}:Null", properties[i].Name);
                }
                else if (value.GetType().Name.Equals("String") || value.GetType().Name.Equals("DateTime"))
                {
                    Console.WriteLine("{0}:'{1}'", properties[i].Name,value);
                    //Console.WriteLine(string.Format("Member name:{0},value:{1},TYPE:{2}", properties[i].Name, value.ToString(), properties[i].GetValue(param, null).GetType().Name));
                }
                else
                {
                    Console.WriteLine("Type:{0}", value.GetType().Name);
                    Console.WriteLine("{0}:{1}", properties[i].Name, value);
                }
                //}
            }
        }
    }
}
