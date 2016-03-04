using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Persistence;
using BaseComponent.Debugger;
using EBSComponent.Enums;
using ComponentModels.EbsDBModel;
using System.Linq;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class PersistenceUpdateTest
    {
        private PersistenceEBS persistence { get; set; }
        private DebugHelper debug { get; set; }

        public PersistenceUpdateTest()
        {
            int databaseRead = (int)DatabaseEnum.LOCAL_DATABASE;
            int databaseWrite = (int)DatabaseEnum.LOCAL_DATABASE;
            this.persistence = new PersistenceEBS(databaseRead, databaseWrite);
            this.debug = new DebugHelper();
        }

        [TestMethod]
        public void UpdateTest()
        {
            N_Order_Base order = persistence.Query<N_Order_Base>().LastOrDefault();
            int id = order.ID;
            Assert.AreNotEqual(null, order);

            string guid = debug.GetPaddingString(false).Substring(15);
            order.OrderId = guid;
            persistence.Update(order,new { ID = order.ID});
            order = persistence.Query<N_Order_Base>().FirstOrDefault(o => o.ID == id);
            Assert.AreEqual(guid, order.OrderId);
        }

        [TestMethod]
        public void UpdateWithTransactionTest()
        {

        }
    }
}
