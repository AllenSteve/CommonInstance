using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Persistence;
using BaseComponent.Debugger;
using EbsComponent.Enums;

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

        }

        [TestMethod]
        public void UpdateWithTransactionTest()
        {

        }
    }
}
