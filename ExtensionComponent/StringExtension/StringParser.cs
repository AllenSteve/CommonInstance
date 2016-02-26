using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionComponent.String
{
    public static partial class StringExtension
    {
        /// <summary>
        /// 将对象类解析成string类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        public static string ParseString<T>(this string str, T model)
        {
            Type type = typeof(T);
            if (type.Name.Equals("String")) 
            {
                return model.ToString();
            }
            else
            {
                PropertyInfo[] properties = type.GetProperties();
                StringBuilder retStr = new StringBuilder();
                foreach (var item in properties)
                {
                    retStr.Append(item.Name);
                    retStr.Append("\t=\t");
                    retStr.Append(item.GetValue(model, null));
                    retStr.Append("\r\n");
                }
                return retStr.ToString();
            }
        }
    }
}
