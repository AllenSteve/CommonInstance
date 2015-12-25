using BaseFunction.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class ServiceFactory:IServiceFactory
    {
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
