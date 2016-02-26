using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.DebugComponent
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
    }
}
