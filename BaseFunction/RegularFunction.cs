using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseFunction
{
    public class RegularFunction
    {
        public RegularFunction()
        { }

        public void ExpressionTest()
        {
            Regex r = new Regex("^\\d+");
            Console.WriteLine(r.Match("@").Success);
            Console.WriteLine(r.Match("akb48").Success);
            Console.WriteLine(r.Match("11akb48").Success);
        }
    }
}
