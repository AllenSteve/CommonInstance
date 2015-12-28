using Ninject;
using Ninject.Extensions.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction
{
    public class Register
    {
        private StandardKernel _kernel = new StandardKernel();

        // 以后这里就不用更改这里了，只需要该xml文件就可以了
        public Register()
        {
            var settings = new NinjectSettings() { LoadExtensions = false };
            _kernel = new StandardKernel(settings, new XmlExtensionModule());
            //_kernel.Load("Xml/Register.xml");
            _kernel.Load("../../NinjectServiceModule.xml");
        }

        //获取
        public TInterface Get<TInterface>()
        {
            return _kernel.Get<TInterface>();
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}
