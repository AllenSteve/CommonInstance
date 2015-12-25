using BaseFunction.Service.NinjectService;
using BaseFunction.ServiceInterface;
using BaseFunction.ServiceInterface.INinjectService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class ServiceFactory:IServiceFactory
    {
        // 使用Ninject
        private IKernel kernel { get; set; }

        public ServiceFactory()
        {
            this.kernel = CreateKernel();
        }

        private static IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();

            RegisterServices(kernel);

            return kernel;
        }

        /// <summary>
        /// 通过实例类型来创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>``0.</returns>
        /// <exception cref="System.ArgumentException">
        /// 未使用指定命名空间中的类型进行初始化
        /// or
        /// 请使用类类型进行初始化
        /// </exception>
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

            T service = this.kernel.Get<T>();//Activator.CreateInstance(typeof(param),false);

            return service;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public T Create<T>()
        {
            if (!typeof(T).FullName.StartsWith("BaseFunction.Service"))
            {
                throw new ArgumentException("未使用指定命名空间中的类型进行初始化", typeof(T).FullName);
            }
            if (typeof(T).IsInterface)
            {
                throw new ArgumentException("请使用类类型进行初始化", typeof(T).FullName);
            }

            T service = Activator.CreateInstance<T>();

            return service;
        }


        public T CreateInterface<T>()
        {
            if (!typeof(T).FullName.StartsWith("BaseFunction.Service"))
            {
                throw new ArgumentException("未使用指定命名空间中的类型进行初始化", typeof(T).FullName);
            }
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException("请使用接口类型进行初始化", typeof(T).FullName);
            }

            T service = this.kernel.Get<T>();

            return service;
        }

        /// <summary>
        /// 注册绑定关系
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IValueCalculater>().To<LinqValueCalculator>();
        }    
    }
}
