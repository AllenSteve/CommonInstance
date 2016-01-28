using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseComponent.DesignPatterns.DoubleDispatch;

namespace StandardUnitTest.ComponentTest.Base.Component
{
    [TestClass]
    public class DoubleDispatchTest
    {

        public IAnimal animal { get; set; }

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
            Survey survey = new Survey();
            IAnimal animal = new Dog();
            Assert.AreEqual("IAnimal Type", survey.DoSurvey(animal));

            var dog = new Dog();
            Assert.AreEqual("Dog Type", survey.DoSurvey(dog));
        }
    }
}
