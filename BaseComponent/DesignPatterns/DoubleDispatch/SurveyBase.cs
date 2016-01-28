using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.DesignPatterns.DoubleDispatch
{
    public class SurveyBase
    {

        public string Target { get; set; }

        public virtual void DoSurvey()
        {
            this.Target = this.GetType().Name;
            Console.WriteLine("Base Class");
        }
    }
}
