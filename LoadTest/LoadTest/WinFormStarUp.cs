using BaseFunction.BaseTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CocurrencyTest
{
    static class WinFormStarUp
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            Application.Run(new CocurrencyLoader());
            Application.Run(new TransactionService());
        }
    }
}
