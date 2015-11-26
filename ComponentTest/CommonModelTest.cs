using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            transactionServiceBaseModel = new TransactionServiceBaseModel("www.baidu.com", DateTime.Now);
            var str = transactionServiceBaseModel.ToDictionary();
            Console.WriteLine(str.Count);
            transactionServiceBusinessModel = new TransactionServiceBusinessModel("soufunId","充值");
            str=transactionServiceBusinessModel.ToDictionary();
            Console.WriteLine(str.Count);
        }

        
    }
}
