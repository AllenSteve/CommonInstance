using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Persistence;
using EbsComponent.Enums;
using ComponentModels.EbsDBModel;
using System.Collections.Generic;
using System.Linq;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class PersistenceCreateTest
    {
        private PersistenceEBS persistence { get; set; }

        public PersistenceCreateTest()
        {
            int databaseRead = (int)DatabaseEnum.LOCAL_DATABASE;
            int databaseWrite = (int)DatabaseEnum.LOCAL_DATABASE;
            this.persistence = new PersistenceEBS(databaseRead, databaseWrite);
        }

        [TestMethod]
        public void CreatePersistenceTest()
        {
            int databaseRead = (int)DatabaseEnum.EBS_READ;
            int databaseWrite = (int)DatabaseEnum.EBS_WRITE;
            this.persistence = new PersistenceEBS(databaseRead,databaseWrite);
        }

        [TestMethod]
        public void AddTest()
        {

        }

        [TestMethod]
        public void AddWithTransactionTest()
        {

        }
    }
}
