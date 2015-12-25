using BaseFunction.ServiceInterface.INinjectService;
using ComponentModels.ServiceModel.BaseServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service.NinjectService
{
    public class ShoppingCart
    {
        protected IValueCalculater calculator;
        protected Product[] products;

        public ShoppingCart(IValueCalculater calcuParam)
        {
            this.calculator = calcuParam;
            products = new[]{
                                new Product(){Name="Kayak" , Price=275M},
                                new Product(){Name="Lifejacket" , Price=48.95M},
                                new Product(){Name="Scooceer ball" , Price=19.5M},
                                new Product(){Name="Stadium" , Price=79550M}
                            };
        }

        public virtual decimal CalculatStockValue()
        {
            decimal totalPrice = calculator.ValueProducts(products);
            return totalPrice;
        }
    }
}
