using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Persistence;
using EbsComponent.Enums;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class CreateTest
    {
        private PersistenceEBS persistence { get; set; }

        public CreateTest()
        {
            //this.persistence = new PersistenceEBS();
        }

        [TestMethod]
        public void CreatePersistenceTest()
        {
            int databaseRead = (int)DatabaseEnum.EBS_READ;
            int databaseWrite = (int)DatabaseEnum.EBS_WRITE;
            this.persistence = new PersistenceEBS(databaseRead,databaseWrite);

        }

        [TestMethod]
        public void AddTransactionTest()
        {

        }

        [TestMethod]
        public void AddWithTransactionTest()
        {

        }
    }
}
