using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.ServiceInterface.IEbsServcie
{

    /// <summary>
    /// 1-获取http://m.fang.com/站的用户登录信息。 查看suft   cookie。 
    /// 2-调用Soufun.Passport.Config .GetUrlValidSfutCookie() 方法获取解密地址， 后调用该地址。 
    /// 3-获取用户的信息。 封装成对象。
    /// </summary>
    public interface IParseCookie
    {
        string ParseSfutCookie();

        string DecryptSfut2Url(string sfutCookie);

        CheckUserInfo ParseUserInfo(string url, string encoding = "UTF-8");

    }
}
