using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using ORMappingComponent;
using EOP.DapperModel;
namespace EOP.UnitTest
{
    [TestClass]
    public class DapperHelperTest
    {
        [TestMethod]
        public void QueryAllTest()
        {
            // arrange
            DBHelper db = new DBHelper();
            var list = db.QueryAll<DealerServiceLog>().ToList();
            // act
            int count = list.Count;
            // assert
            Assert.AreEqual(4, count);

        }

        [TestMethod]
        public void QueryManyTest()
        {
            // arrange
            DBHelper db = new DBHelper();
            string sql = "select CompanyName,CityId,CityName,Balance,Margin,PreDeposits,DealerClass,Partner_CompanyExtent.Score,OrderAmount,ContractAmount,Partner_CompanyExtent.[Status],SolutionIDs,DealerDistrict from Partner_Company,Partner_CompanyExtent where Partner_CompanyExtent.DealerID=Partner_Company.ID";

            sql = "SELECT * FROM DealerClassConfig  AS D JOIN DealerClassConfig AS D1 ON D.id = D1.ID ";
            var list = db.Query(sql).ToList();
            // act
            int count = list.Count;
            var first = (IDictionary<string, object>)list.FirstOrDefault();
            // assert
            Assert.AreEqual(3, count);

        }

        [TestMethod]
        public void InsertLogTest()
        {
            // arrange
            DBHelper.Sqldb db = DBHelper.Sqldb.OrderWrite;
            DBHelper helper = new DBHelper((int)db);

            string sql = "SELECT * FROM DealerClassConfig  AS D JOIN DealerClassConfig AS D1 ON D.id = D1.ID ";

            var list = helper.Query(sql).ToList();
            // act
            int count = list.Count;
            var first = (IDictionary<string, object>)list.FirstOrDefault();
            // assert
            Assert.AreEqual(3, count);
        }
    }
}
