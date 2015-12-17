using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseFunction.ServiceInterface;
using BaseFunction.Service;

namespace StandardUnitTest.ServiceTest
{
    [TestClass]
    public class ServiceFactoryTest
    {
        private IServiceFactory factory { get; set; }

        private IBaseService service { get; set; }

        public ServiceFactoryTest()
        {
            this.factory = new ServiceFactory();
        }


        [TestMethod]
        public void CreateTest()
        {
            service = factory.Create<DateService>();
            var dateService = factory.Create<DateService>();
            var date = dateService.GetMonthFifthWorkingDay("201512");
        }
    }
}
