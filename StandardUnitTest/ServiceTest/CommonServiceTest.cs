using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseFunction.ServiceInterface;
using BaseFunction.Service;
using BaseFunction.Service.EbsService;

namespace StandardUnitTest.ServiceTest
{
    [TestClass]
    public class CommonServiceTest
    {
        private IServiceFactory factory { get; set; }

        [TestMethod]
        public void ParserTest()
        {
            factory = new ServiceFactory();
            var service = factory.Create<ParseCookie>();
            string sfut = service.ParseSfutCookie();
            string url = service.DecryptSfut2Url(sfut);
            object userInfo = service.ParseUserInfo(url);
        }

    }
}
