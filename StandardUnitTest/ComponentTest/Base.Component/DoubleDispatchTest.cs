using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseComponent.DesignPatterns.DoubleDispatch;

namespace StandardUnitTest.ComponentTest.Base.Component
{
    [TestClass]
    public class DoubleDispatchTest
    {
        public DoubleDispatchTest()
        { 
        }

        [TestMethod]
        public void SurveyBaseTest()
        {
            SurveyBase instance = new Survey();
            instance.DoSurvey();
            Assert.AreEqual("Survey", instance.Target);
        }

        [TestMethod]
        public void SurveyOverloadTest()
        {

        }
    }
}
