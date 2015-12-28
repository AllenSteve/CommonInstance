using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ninject;
using Ninject.Extensions.Xml;
using Ninject.Extensions.Xml.Processors;


namespace BaseFunction.Service.BaseService
{
    public class NinjectModuleFactory
    {
        private Register register { get; set; }

        public NinjectModuleFactory()
        {
            this.register = new Register();
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

            return this.register.Get<T>();
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

            return this.register.Get<T>();
        }
    }
}
