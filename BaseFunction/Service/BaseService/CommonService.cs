using BaseComponent.NetworkComponent;
using BaseFunction.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class CommonService : ICommonService, IBaseService
    {
        //测试finally的执行次序；
        private static int GetInt()
        {
            int i = 8;
            try
            {
                i++;
                Console.WriteLine("a");
                return i;
            }
            finally
            {
                Console.WriteLine("b");
                i++;
            }
        }


        public  void ParseCookie()
        {
            HttpHeader header = new HttpHeader();
            HTMLHelper html = new HTMLHelper();
            string url = "http://m.fang.com/my/?c=mycenter&a=index&city=bj";
            url = "https://m.fang.com/passport/login.aspx";
            //url = "http://m.fang.com";
            //url = "http://m.fang.com/my/";
            //header.method = "get";
            //header.contentType = "text/html";


            //var cookie = HTMLHelper.GetCookie(url,string.Empty,header);

            header = new HttpHeader();
            header.accept = "text/html, application/xhtml+xml, */*";
            header.contentType = "application/x-www-form-urlencoded; charset=GBK";
            header.method = "POST";
            //header.userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
            header.maxTry = 3;

            string postParam = "username=17701000011&password=451426&backurl=http://m.fang.com/";

            var uri = new Uri(url);
            var cc = HTMLHelper.GetCookie(url, postParam, header, "gbk");
            var cookie = cc.GetCookies(uri);
            var list = HTMLHelper.GetAllCookies(cc).FirstOrDefault();
            var hcontent = HTMLHelper.GetHtml("http://m.fang.com/my/?c=mycenter&a=index&city=bj", cc, header, "gbk");
        }


        /// <summary>
        /// 访问博客园
        /// </summary>
        public void ParseCnBlogCookie()
        {
            HttpHeader header = new HttpHeader();
            HTMLHelper html = new HTMLHelper();
            string url = string.Empty;
            url = "http://passport.cnblogs.com/user/signin";

            header = new HttpHeader();
            header.accept = "text/html, application/xhtml+xml, */*";
            header.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            header.method = "POST";
            //header.userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
            header.maxTry = 3;

            string postParam = "username=*****&password=*******&backurl=http://home.cnblogs.com/u/allenstar/";

            var uri = new Uri(url);
            var cc = HTMLHelper.GetCookie(url, postParam, header);
            var cookie = cc.GetCookies(uri);
            var list = HTMLHelper.GetAllCookies(cc);
            var hcontent = HTMLHelper.GetHtml("http://home.cnblogs.com/u/allenstar/", cc, header);
        }
    }
}
