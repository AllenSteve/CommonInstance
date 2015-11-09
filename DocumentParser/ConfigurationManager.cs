using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser
{
    /// <summary>
    /// 用于读取配置文件中的信息
    /// </summary>
    public class ConfigurationManagerInstance
    {
        public ConfigurationManagerInstance()
        {
        }

        /// <summary>
        /// 注意App.Config文件不能放到类库项目中读取。
        /// App.config不能在类库离！运行是会出现！connectionstring 尚未初始化
        /// 所以App.config配置文件应该放在启动项下
        /// </summary>
        public void Run()
        {
            
            var settings = System.Configuration.ConfigurationManager.AppSettings.AllKeys;

            //读取配置文件的常规方法
            string timeCount = System.Configuration.ConfigurationManager.AppSettings["count"];
            //注意配置文件中的参数格式是这样的
            /*
                <appSettings >
                <add key="count" value="60"/>
                </appSettings>
             * 
             * 是的，你给类库加了app.config文件也没用，
             * 因为类库要编译成dll的，编译后dll里就不包含配置文件了。
             * 所以它读到的是你的实际运行项目（也就是你的启动项目）的app.config
             */
            Console.WriteLine("TimeCount is:" + timeCount);

        }


    }
}
