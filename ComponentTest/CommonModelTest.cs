using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;

namespace ComponentTest
{
    public class CommonModelTest
    {
        TransactionServiceBaseModel transactionServiceBaseModel { get; set; }

        TransactionServiceBusinessModel transactionServiceBusinessModel { get; set; }

        public CommonModelTest()
        { 
        }

        public void RunTransactionServiceBaseModelTest()
        {
            TransactionServiceBusinessModel model = null;
            model = new TransactionServiceBusinessModel("return_url",
                                                                                      "soufunId",
                                                                                      "tradeType",
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      0,
                                                                                      "title",
                                                                                      "subject",
                                                                                      "invoker",
                                                                                      "extra_param",
                                                                                      "platform",
                                                                                      "origin",
                                                                                      "Utf-8"
                                                                                      );
            var query = model.ToQueryString();


            Console.WriteLine("queryStr:" + query);
        }

        
    }
}
