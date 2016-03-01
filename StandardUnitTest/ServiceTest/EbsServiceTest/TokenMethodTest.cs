using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseFunction.Service.EbsService;
using BaseComponent.Encryption;

namespace StandardUnitTest.ServiceTest.EbsServiceTest
{
    [TestClass]
    public class TokenMethodTest
    {
        private TokenMethod method { get; set; }

        public TokenMethodTest()
        {
            this.method = new TokenMethod();
        }

        [TestMethod]
        public void CreateCryptographTest()
        {
            string timestamp = "1451875269";
            timestamp = TokenMethod.ConvertDateTime2Timestamp(DateTime.Now).ToString();
            timestamp = "1451902055";
            string soufunId = "54090696";
            string cryptograph = TokenMethod.CreateCryptograph(timestamp, soufunId);
            bool result = method.VerifyParam(soufunId, cryptograph.Replace("%2B", "+"), new TimeSpan(TimeSpan.TicksPerDay * 5));
        }

        [TestMethod]
        public void CryptographTest()
        {
            string cryptograph = "rzcssbtvCfz6kCM5B7XQeS9jWISTaLXe/BqGLiGeVICOkJsDEq7Fwe3oFt/S9Pff";
            cryptograph = "xG/pk85t9Lfw2HLsGF8W8HJBGcje55WeQDXzkB1CIvZ9MMFNLh2xJbY9cP2AV5i0";
            cryptograph = "EIkfoBi7ws7u2kS0adOFemx5J8vMX7gUKjeVvB9VX/lq1/9LafIpoxVL8NXuko41";
            string plaintext = AdvancedEncryption.AESDecrypt(cryptograph);
        }
    }
}
