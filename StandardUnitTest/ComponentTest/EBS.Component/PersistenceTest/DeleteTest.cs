using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Persistence;
using BaseComponent.Debugger;
using EBSComponent.Enums;
using ComponentModels.EbsDBModel;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class PersistenceDeleteTest
    {
        private PersistenceEBS persistence { get; set; }
        private DebugHelper debug { get; set; }

        public PersistenceDeleteTest()
        {
            int databaseRead = (int)EntityTypeEnum.LOCAL_DATABASE;
            int databaseWrite = (int)EntityTypeEnum.LOCAL_DATABASE;
            this.persistence = new PersistenceEBS(databaseRead, databaseWrite);
            this.debug = new DebugHelper();
        }

        [TestMethod]
        public void DeleteTest()
        {
            N_Order_Base order = new N_Order_Base();
            int ret = 0;
            //order = null;
            //ret = persistence.Delete<N_Order_Base>(order);
            //Assert.AreEqual(1,ret);

            object param = new { ID = 1, Name = "test" };
            ret = persistence.Delete<N_Order_Base>(null, param);
            Assert.AreEqual(1, ret);
        }

        [TestMethod]
        public void RemoveTest()
        {
            N_Order_Base order = new N_Order_Base();
            int ret = 0;
            //order = null;
            //ret = persistence.Remove<N_Order_Base>(order);
            //Assert.AreEqual(1, ret);

            object param = new { ID = 1, Name = "test" };
            ret = persistence.Remove<N_Order_Base>(null, param);
            Assert.AreEqual(1, ret);
        }
    }
}
