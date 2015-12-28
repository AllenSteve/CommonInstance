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
        private IKernel kernel { get; set; }

        // 以后这里就不用更改这里了，只需要该xml文件就可以了
        public Register()
        {
            // 如果这里抛异常提示继承关系访问级别问题
            // 请将Ninject.Extensions.Xml项目中的AssemblyInfo.cs文件中的debug行注释掉即可
            var settings = new NinjectSettings() { LoadExtensions = false };
            this.kernel = new StandardKernel(settings, new XmlExtensionModule());

            // 注意这里木人的路径在bin/Debug目录下
            //_kernel.Load("Xml/Register.xml");
            // 注意此处只接受XML格式文件
            this.kernel.Load("../../NinjectServiceModule.xml");
        }

        // 获取接口实例
        public TInterface Get<TInterface>()
        {
            return kernel.Get<TInterface>();
        }

        public void Dispose()
        {
            kernel.Dispose();
        }
    }
}
