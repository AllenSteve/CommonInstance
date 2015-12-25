using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseFunction.Service.NinjectService;
using BaseFunction.ServiceInterface.INinjectService;
//using Ninject;
//using Ninject.Infrastructure.Disposal;

namespace StandardUnitTest.ServiceTest.NinjectServiceTest
{
    [TestClass]
    public class ShoppingCartTest
    {
        private ShoppingCart shoppingCart { get; set; }

        //private IKernel kernel { get; set; }

        public ShoppingCartTest()
        {
            // 未绑定inject时需要构造函数初始化
            this.shoppingCart = new ShoppingCart(new LinqValueCalculator());
            
            // 使用Ninject
            //kernel = new StandardKernel();
            //kernel.Bind<IValueCalculater>().To<LinqValueCalculator>();
        
        }

        [TestMethod]
        public void CalculatStockValueTest()
        {
            var value = this.shoppingCart.CalculatStockValue();
        }
    }
}
