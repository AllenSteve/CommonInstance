using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.Debugger
{
    public class DebugHelper
    {
        protected StringBuilder mailContext { get; set; }

        protected string mailAddress { get; set; }

        //public object logWriter { get; set; }

        public DebugHelper()
        {
            this.mailAddress = "qiushilong@fang.com";
            this.mailContext = new StringBuilder();
        }

        public void WriteMail(string context)
        {
            this.mailContext.Append(context);
            this.mailContext.Append("\r\n");
        }

        public void WriteMail<T>(T model)
        {
            //this.WriteMail(string.Empty.ParseString(model));
        }

        public void WriteMail(params string[] contextList)
        {
            foreach (var context in contextList)
            {
                this.WriteMail(context);
            }
        }

        public void SendEmail(string title)
        {
            //SendEmail.Send(this.mailAddress, title, this.mailContext.ToString());
        }

        /// <summary>
        /// 填充Model对象默认值
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <param name="model">要填充默认值的对象</param>
        /// <returns>填充默认值之后的对象</returns>
        public T PadValue<T>(T model) where T : new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                Type type = property.PropertyType;
                if (type.Equals(typeof(DateTime)))
                {
                    property.SetValue(model, DateTime.Now);
                }
                else if (type.Equals(typeof(string)))
                {
                    property.SetValue(model, string.Empty);
                }
            }
            return model;
        }

        public string GetPaddingString(bool empty = true)
        {
            if (!empty)
            {
                return Guid.NewGuid().ToString().Replace("-",string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
