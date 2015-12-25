using BaseFunction.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class RegularService : IRegularService,IBaseService
    {
        public void RegularTest()
        {
            Regex r = new Regex("^\\d+");
            Console.WriteLine(r.Match("@").Success);
            Console.WriteLine(r.Match("akb48").Success);
            Console.WriteLine(r.Match("11akb48").Success);
        }
    }
}
