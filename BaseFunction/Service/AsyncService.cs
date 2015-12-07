using BaseFunction.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.Service
{
    public class AsyncService : IAsyncService
    {
        public void Create()
        {
            //string  path = @"E:\01.Doc\02.工作日志\02.2015-11\02-CallInterfaceLog日志解析\02-目标日志\zxbapp_log_2015-11-04.txt";
            //FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //Task task = new Task();
            //await task.Run(async () =>
            //{
            //    using (var writer = new StreamWriter(stream) { AutoFlush = true })
            //    using (var reader = new StreamReader(stream))
            //    {
            //        var line = string.Empty;
            //        while ((line = await reader.ReadLineAsync()) != null)
            //        {
            //            await writer.WriteAsync(">>> " + line + Environment.NewLine);
            //        }
            //        Console.WriteLine("connection closed");
            //    }
            //});

            var func = new Func<string, string>(i =>
            {
                return i + "i can fly";
            });

            Task<string>.Factory.FromAsync(func.BeginInvoke, func.EndInvoke, "yes,", null).ContinueWith
                (i =>
                {
                    Console.WriteLine(i.Result);
                });

            Console.Read();
        }
    }
}
