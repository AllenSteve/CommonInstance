using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.DesignPatterns.DoubleDispatch
{
    public class SurveyBase
    {
        public virtual void DoSurvey()
        {
            Console.WriteLine("Base Class");
        }
    }
}
