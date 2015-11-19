using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORMappingComponent.TableClass;
using ORMappingComponent;

namespace StandardUnitTest
{
    [TestClass]
    public class DapperApiUnitTest 
    {
        [TestMethod]
        public void QueryTest()
        {
            // Arrange-测试设置,创建对象
            Customer customer = new Customer()
                                               {
                                                   UserName = "User-1",
                                                   Password = "PWDHASH-1 ",
                                                   Email = "EMAIL-1 ",
                                                   PhoneNumber = "PHONE-1 ",
                                                   IsFirstTimeLogin = false,
                                                   AccessFailedCount = 0,
                                                   CreationDate = DateTime.Now,
                                                   IsActive = false
                                               };
            int count = 0;

            // 拼接字符串的时候要注意标量的名字要与类中的成员变量名一样（可忽略字母的大小写）
            // 例如：此处sql中的@Password中的Password就必须与Customer类中的成员变量Password名称一样（可忽略大小写）
            string queryStr = @"SELECT * FROM dbo.CICUser WHERE UserName=@UserName AND PasswordHash=@Password";
            
            // Act-测试行为,使用功能
            count = DapperHelper.Query<Customer>(queryStr, customer).Count;

            // Assert-测试结果,验证结果
            Assert.AreEqual(1,count);

        }
    }
}
