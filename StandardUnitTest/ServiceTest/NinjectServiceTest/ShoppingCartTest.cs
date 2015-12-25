using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseFunction.Service.NinjectService;
using BaseFunction.ServiceInterface.INinjectService;
using BaseFunction.ServiceInterface;
using BaseFunction.Service;

namespace StandardUnitTest.ServiceTest.NinjectServiceTest
{
    [TestClass]
    public class ShoppingCartTest
    {
        private IServiceFactory factory { get; set; }
        private ShoppingCart shoppingCart { get; set; }
        private LinqValueCalculator calculator { get; set; }

        public ShoppingCartTest()
        {
            factory = new ServiceFactory();
            // 使用工厂方法生成类
            this.calculator = factory.CreateInstance<LinqValueCalculator>();
            this.shoppingCart = factory.CreateInstance<ShoppingCart>();
        }

        [TestMethod]
        public void LinqValueCalculatorTest()
        {
            var value = this.calculator.ValueProducts();
        }

        [TestMethod]
        public void CalculatStockValueTest()
        {
            var value = this.shoppingCart.CalculatStockValue();
            Assert.AreEqual(79893.45M, value);
        }
    }
}
