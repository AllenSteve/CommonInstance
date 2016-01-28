using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.DesignPatterns.DoubleDispatch
{
    /// <summary>
    /// 来源：http://www.cnblogs.com/HQFZ/p/4942561.html
    /// </summary>
    public class Survey : SurveyBase
    {
        public override void DoSurvey()
        {
            Console.WriteLine("Derived Class");
        }

        public string DoSurvey(IAnimal animal)
        {
            return "IAnimal Type";
        }

        public string DoSurvey(Mammals animal)
        {
            return "Mammals Type";
        }

        public string DoSurvey(Dog animal)
        {
            return "Dog Type";
        }
    }
}
