using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseFunction.ServiceInterface;
using BaseFunction.Service;
using BaseFunction.Service.EbsService;

namespace EOP.Web.Ajax
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class PaymentHandler : IHttpHandler
    {
        public int issuccess { get; set; }
        public string msg { get; set; }

        private IServiceFactory factory { get; set; }

        public PaymentHandler()
        {
            this.issuccess = 0;
            this.msg = string.Empty;
        }

        //1. 同步通知
        //2. 渠道派单/电商派单
        public void ProcessRequest(HttpContext context)
        {
            factory = new ServiceFactory();
            var service = factory.Create<ParseCookie>();
            string sfut = service.ParseSfutCookie(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
