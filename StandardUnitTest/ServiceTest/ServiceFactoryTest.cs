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

        [TestMethod]
        public void CreateTest()
        {
            factory = new ServiceFactory();
            service = factory.Create<DateService>();
            var dateService = factory.Create<DateService>();

            var date = dateService.GetMonthFifthWorkingDay("201512");
            
        }
    }
}
