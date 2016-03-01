using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExtensionComponent.String
{
    public static partial class StringExtension
    {
        public static string ConvertEncoding(this string str, string srcEncoding = "GB2312", string destEncoding = "UTF-8")
        {
            // 源格式编码
            Encoding SRC = Encoding.GetEncoding(srcEncoding);
            // 源格式编码
            byte[] buffer = SRC.GetBytes(str);

            // 目标格式编码
            Encoding DEST = Encoding.GetEncoding(destEncoding);
            // 转换为 目标格式编码
            byte[] buffer2 = Encoding.Convert(SRC, DEST, buffer, 0, buffer.Length);

            return SRC.GetString(buffer2, 0, buffer2.Length);
        }

        public static string ConvertToUrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }
    }
}
