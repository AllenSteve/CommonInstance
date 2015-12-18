using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORMappingComponent.TableClass;
using ORMappingComponent;
using System.Collections.Generic;
using System.Linq;
using ORMappingComponent.ORMappingTools;

namespace StandardUnitTest
{
    [TestClass]
    public class DapperApiUnitTest 
    {
        [TestMethod]
        public void QueryTest()
        {
            // Arrange-测试设置,创建对象
            DBHelper db = new DBHelper();
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
            List<Customer> customerList = new List<Customer>();

            // 拼接字符串的时候要注意标量的名字要与类中的成员变量名一样（可忽略字母的大小写）
            // 例如：此处sql中的@Password中的Password就必须与Customer类中的成员变量Password名称一样（可忽略大小写）
            string querySQL = @"SELECT * FROM dbo.CICUser WHERE UserName=@UserName AND PasswordHash=@Password";
            
            // Act-测试行为,使用功能
            customerList = db.Query<Customer>(querySQL, customer).ToList();
            count = customerList.Count;

            // Assert-测试结果,验证结果
            Assert.AreEqual(1,count);
        }

        [TestMethod]
        public void AddTest()
        {
            // Arrange-测试设置,创建对象
            DBHelper db = new DBHelper();
            UserInfo user = new UserInfo()
                                    {
                                        Code = "UnitTest",
                                        Name = "单元测试用户",
                                        Description = "本条记录为单元测试新增用户"
                                    };
            // 用于接收返回结果
            int result = 0;
            string insertSQL = @"INSERT INTO UserInfo(Code,Name,Description) VALUES (@Code,@Name,@Description)";

            // Act-测试行为,使用功能
            result = db.Add<UserInfo>(insertSQL,user);

            // Assert-测试结果,验证结果
            Assert.AreEqual(1,result);
        }
    }
}
