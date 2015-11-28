using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;
using ComponentModels.Enums;

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
                                                                                      "subject",
                                                                                      "extra_param:DealerID|tradeType|backurl"
                                                                                      );
            var query = model.ToQueryString();


            //Console.WriteLine("queryStr:" + query);
            string backurl = model.extra_param.Split('|').Last().Trim();
            Console.WriteLine(backurl);
        }

        
    }
}
