using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseFunction.ServiceInterface;
using BaseFunction.Service;
using BaseFunction.Service.BaseService;
using BaseFunction;

namespace StandardUnitTest.ServiceTest
{
    [TestClass]
    public class ServiceFactoryTest
    {
        private IServiceFactory factory { get; set; }

        private NinjectModuleFactory ninjectFactory { get; set; }

        private IBaseService service { get; set; }

        public ServiceFactoryTest()
        {
            this.factory = new ServiceFactory();
            this.ninjectFactory = new NinjectModuleFactory();
        }


        [TestMethod]
        public void CreateTest()
        {
            service = factory.CreateInstance<IBaseService>();
            var dateService = factory.CreateInstance<DateService>();
            var date = dateService.GetMonthFifthWorkingDay("201512");
        }

        [TestMethod]
        public void CreateNinjectModuleFactoryTest()
        {
            IDateService ds = factory.CreateInterface<IDateService>();

            ds = ninjectFactory.CreateInterface<IDateService>();
        }
    }
}
