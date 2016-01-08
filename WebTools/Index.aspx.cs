using EBS.Interface.EContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EOP.Web.Ajax
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpContext context = new HttpContext(Request,new HttpResponse(new System.IO.StringWriter()));
            var contract = new GetUnsignedConstructionContract();
            contract.ProcessRequest(base.Context);
            base.Context.Response.End();
        }
    }
}