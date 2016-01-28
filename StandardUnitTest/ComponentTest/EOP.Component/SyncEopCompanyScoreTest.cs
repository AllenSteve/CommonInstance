using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EOPComponent.Method;

namespace StandardUnitTest.ComponentTest.Base.Component
{
    [TestClass]
    public class SyncEopCompanyScoreTest
    {

        SyncEopCompanyScore sync { get; set; }

        public SyncEopCompanyScoreTest()
        {
            this.sync = new SyncEopCompanyScore();
        }

        [TestMethod]
        public void SyncTest()
        {
            sync.UpdateCompanyScore();
        }
    }
}
