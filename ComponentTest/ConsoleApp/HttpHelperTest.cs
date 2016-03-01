using BaseComponent.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.ConsoleApp
{
    public class HttpHelperTest
    {
        private HttpHeader header { get; set; }

        public HttpHelperTest()
        {
            LoginFangtianxiaTest();
        }

        /// <summary>
        /// 登录：https://passport.fang.com/
        /// </summary>
        public void LoginFangtianxiaTest()
        {
            Console.WriteLine("登录房天下测试！");

            header = new HttpHeader();
            header.accept = "*/*";
            header.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            header.method = "POST";
            header.userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
            header.maxTry = 3;

            // 后续地址
            string getUrl = "http://my.fang.com/";

            string loginUrl = "https://passport.fang.com/";

            string postParam = "biz_id=747420150428100001&call_time=2015-11-27 02:40:23.788&charset=GB2312&extra_param=商家ID|支付类型|backurl跳转的地址（由用户传入）&invoker=&notify_url=&origin=EOP后台&out_trade_no=&paid_amount=0.01&platform=PC&price=0.01&quantity=1&return_url=&service=cashier_order_create_for_web&sign=&sign_type=MD5&subject=EOP商家 + 商家ID + 质保金/预存款 充值xxx元&title=EOP商家质保金&trade_amount=0.01&trade_type=30003&user_id=73318605&version=1.0&sign=0074e9c00b81e48ddf9272f85bd27257&txtusername=17701000011&password=451426&backurl=http://my.fang.com/";

            CookieContainer cookieContainer = HTMLHelper.GetCookie(loginUrl, postParam, header);

            string html = HTMLHelper.GetHtml(getUrl, cookieContainer, header);

            Console.WriteLine(html);
            Console.ReadLine();
        }


        public void LoginRenRenTest()
        {
            header = new HttpHeader();
            header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.contentType = "application/x-www-form-urlencoded";
            header.method = "POST";
            header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.maxTry = 3;

            // 后续地址
            string getUrl = "http://guide.renren.com/guide";
            //登录地址
            string loginUrl = "http://www.renren.com/PLogin.do";
            //请求参数
            string postParam = "email=aaa@163.com&password=111&icode=&origURL=http%3A%2F%2Fwww.renren.com%2Fhome&domain=renren.com&key_id=1&_rtk=90484476";

            CookieContainer cookieContainer = HTMLHelper.GetCookie(loginUrl, postParam, header);

            string html = HTMLHelper.GetHtml(getUrl, cookieContainer, header);

            Console.WriteLine(html);


            Console.ReadLine();
        }
    }
}
