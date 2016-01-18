using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ComponentModels.ServiceModel;
using EOPComponent.Model.ServiceModel;

namespace EOP.UnitTest.ModelTest
{
    [TestClass]
    public class TransactionServiceBusinessModelTest
    {
        [TestMethod]
        public void GetBusinessModelParamDictionary()
        {
            TransactionServiceBusinessModel model = null;
            model = new TransactionServiceBusinessModel("return_url",
                                                                                      "soufunId",
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      "title",
                                                                                      "subject",
                                                                                      "extra_param"
                                                                                      );
            string query = model.ToQueryString();

            Assert.AreEqual(22,model.GetType().GetProperties().Length);
        }
    }
}
