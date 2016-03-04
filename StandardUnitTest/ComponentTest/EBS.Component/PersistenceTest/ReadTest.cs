using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentModels.EbsDBModel;
using System.Collections.Generic;
using EBSComponent.Enums;
using EBSComponent.Persistence;
using System.Linq;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class PersistenceRead
    {
         private PersistenceEBS persistence { get; set; }

         public PersistenceRead()
        {
            int databaseRead = (int)DatabaseEnum.LOCAL_DATABASE;
            int databaseWrite = (int)DatabaseEnum.LOCAL_DATABASE;
            this.persistence = new PersistenceEBS(databaseRead, databaseWrite);
        }

        [TestMethod]
        public void QueryTest()
        {
            List<N_Order_Base> list = this.persistence.Query<N_Order_Base>().ToList();
            Assert.AreNotEqual(0, list.Count);
        }
    }
}
