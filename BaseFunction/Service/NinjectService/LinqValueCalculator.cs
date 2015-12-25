using BaseFunction.ServiceInterface.INinjectService;
using ComponentModels.ServiceModel.BaseServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service.NinjectService
{
    public class LinqValueCalculator : IValueCalculater
    {
        public decimal ValueProducts(params Product[] products)
        {
            return products.Sum(p => p.Price);
        }
    }
}
