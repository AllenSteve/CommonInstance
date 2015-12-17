using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.ServiceInterface.IEbsServcie
{
    /// <summary>
    /// EBS加密服务接口
    /// </summary>
    public interface IEncryptService
    {
        /// <summary>
        /// AES加密字符串
        /// </summary>
        /// <param name="origin">原文</param>
        /// <returns>密文</returns>
        string AdvancedEncrypt(string timestamp,string soufunId);

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cryptograph">密文</param>
        /// <returns>原文</returns>
        string AdvancedDecrypt(string cryptograph);
    }
}
