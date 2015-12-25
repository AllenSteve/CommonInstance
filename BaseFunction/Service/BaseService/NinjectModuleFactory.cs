using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;
using Ninject;
//using Ninject.Extensions.Xml;
//using Ninject.Extensions.Xml.Handlers;


namespace BaseFunction.Service.BaseService
{
    public class NinjectModuleFactory
    {
        // 使用Ninject
        private IKernel kernel { get; set; }

        //private IDictionary<string, IXmlElementHandler> elementHandlers;

        public NinjectModuleFactory()
        {
            this.kernel = CreateKernel();
        }

        public T CreateInstance<T>()
        {
            if (!typeof(T).FullName.StartsWith("BaseFunction.Service"))
            {
                throw new ArgumentException("未使用指定命名空间中的类型进行初始化", typeof(T).FullName);
            }
            if (typeof(T).IsInterface)
            {
                throw new ArgumentException("请使用类类型进行初始化", typeof(T).FullName);
            }

            //T service = this.kernel.Get<T>();//Activator.CreateInstance(typeof(param),false);

            //return service;
            Exception  ex= new Exception();
            throw ex;
        }

        private static IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();

            //RegisterServices(kernel);

            return kernel;
        }
    }
}
