using DocumentParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest
{
    public class LogParserTest
    {
        public LogParserTest()
        { 
        }

        public void Run()
        {
            LogParser log = new LogParser();
            string path = @"e://fzxapp_log_2015-11-03.txt";

            //path = @"e://sample.txt";
            //path = @"e://errorlog.txt";
            path = @"e://error.txt";

            log.LoadLogFile(path);
            log.ParseLogToList();

            Console.WriteLine(log.logModelList.Count);

            log.BulkInsert(log.logModelList);
        }
    }
}
