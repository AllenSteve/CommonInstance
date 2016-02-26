using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent
{
    public class AdoTutorial
    {
        private static string DB_CONNECTION = ConfigurationManager.ConnectionStrings["EBS_WRITE"].ConnectionString;

        public AdoTutorial()
        {

        }


    }
}
