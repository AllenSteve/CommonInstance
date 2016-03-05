using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Interface;
using EBSComponent.Model.EntityType;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class PersistenceTest
    {
        private Persistence<EntityEBS> persistence { get; set; }

        public PersistenceTest()
        {
            this.persistence = new Persistence<EntityEBS>();
        }

        [TestMethod]
        public void InitTest()
        {
            var connection = persistence;
        }
    }
}
