using ComponentModels.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;
using ComponentModels.Enums;
using System.Reflection;
using BaseComponent.DocumentParser;
using ORMappingComponent;
using ComponentModels.EbsDBModel;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using EOP.DapperModel.EBS;
using ORMappingComponent.ORMappingTools;

namespace ComponentTest
{
    public class CommonModelTest
    {
        TransactionServiceBaseModel transactionServiceBaseModel { get; set; }

        TransactionServiceBusinessModel transactionServiceBusinessModel { get; set; }

        public CommonModelTest()
        { 
        }

        public void RunCreateSQLTest()
        {
            string sql = null;
            object columnParam = new { Status = 1 };
            object conditionParam = new { ID = 1000, IsDel=0 };
            sql = sql.CreateSQLUpdateByProperties<Partner_Company>(columnParam, conditionParam);
            Console.WriteLine(sql);

            columnParam = new { Score = 100 };
            conditionParam = new { DealerID = 1000, IsDel = 0 };
            sql = sql.CreateSQLUpdateByProperties<Partner_CompanyExtent>(columnParam, conditionParam);
            Console.WriteLine(sql);
        }

        public void RunJsonTest()
        {

            DBHelper db = new DBHelper((int)DBHelper.Sqldb.OrderReadOnly);
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT  * FROM    dbo.C_PageViewLog ");
            sql.Append(" WHERE Token IS NOT NULL AND ");
            sql.Append(" CreateTime BETWEEN '2015-11-27' AND '2015-12-08' ");
            sql.Append(" AND ( ");
            // 1018知识列表页；1019论坛列表页；1020问答列表页
            sql.Append(" PageView LIKE '%1018%' ");
            sql.Append(" OR PageView LIKE '%1019%' ");
            sql.Append(" OR PageView LIKE '%1020%' ");
            sql.Append(" ) ");

            IEnumerable<C_PageViewLog> resultList = db.Query<C_PageViewLog>(sql.ToString());
            IDictionary<string, PageViewLogParser> dictionary = new Dictionary<string, PageViewLogParser>();


            int count = 1;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("开始插入本地数据库");
            //foreach (var item in resultList)
            //{
            //    db.SwitchDB(DBHelper.Sqldb.UserReadOnly);
            //    db.AddWithID(item);
            //}

            Console.WriteLine("插入本地数据库完成");
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                                                                    ts.Hours, ts.Minutes, ts.Seconds,
                                                                    ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);
            stopWatch.Start();
            foreach (var res in resultList)
            {
                PageViewLogParser jParser = new PageViewLogParser(res.PageView);
                if (!dictionary.ContainsKey(res.Token))
                {
                    dictionary.Add(res.Token, jParser);
                }
                else
                {
                    dictionary[res.Token].JsonTextAppend(res.PageView);
                }
            }
            
            Console.WriteLine(dictionary.Count);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                                                                    ts.Hours, ts.Minutes, ts.Seconds,
                                                                    ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);

            stopWatch.Start();
            foreach (var item in dictionary)
            {
                item.Value.Perform();
            }
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                                                                    ts.Hours, ts.Minutes, ts.Seconds,
                                                                    ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);

            stopWatch.Start();
            // 知识列表数
            int knowledgeCount = dictionary.Count(o => o.Value.knowledgeAccess == true);
            int forumCount = dictionary.Count(o => o.Value.forumAccess == true);
            int QACount = dictionary.Count(o => o.Value.QAAccess == true);

            int kfCount = dictionary.Count(o => o.Value.kfAccess == true);
            int fqCount = dictionary.Count(o => o.Value.fqAccess == true);
            int kqCount = dictionary.Count(o => o.Value.kqAccess == true);

            int kfqCount = dictionary.Count(o => o.Value.kfqAccess == true);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                                                                    ts.Hours, ts.Minutes, ts.Seconds,
                                                                    ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);

            Console.WriteLine("知识:\t" + knowledgeCount);
            Console.WriteLine("论坛:\t" + forumCount);
            Console.WriteLine("问答:\t" + QACount);
            Console.WriteLine("同时访问知识和论坛:\t" + kfCount);
            Console.WriteLine("同时访问论坛和问答:\t" + fqCount);
            Console.WriteLine("同时访问知识和问答:\t" + kqCount);
            Console.WriteLine("同时访问三者:\t" + kfqCount);


        }

        public void RunSwitchTest()
        {
            int i=3; 
            switch(i) 
            { 
                case 1: 
                Console.WriteLine(1);
                    Console.WriteLine(1);
                break; 

                case 2: 
                 Console.WriteLine(2);
                    Console.WriteLine(2);
                break; 

                case 3: 
                 Console.WriteLine(3);
                    Console.WriteLine(3);
                break; 

                default: 
                 Console.WriteLine(0);
                    Console.WriteLine(0);
                break; 
            }
        }

        public void RunEnumTest()
        {
            DatabaseType type = DatabaseType.OrderReadOnly;

            int zeroDB = 0;
            int userReadOnlyDB = 1;

            DatabaseType zero = (DatabaseType)zeroDB;

            Console.WriteLine(type);
            Console.WriteLine((int)type);
            Console.WriteLine((DatabaseType)zeroDB);
            Console.WriteLine(zeroDB);
            Console.WriteLine(userReadOnlyDB);

            Console.WriteLine("遍历枚举中的值");
            // 遍历枚举中的值
            foreach (DatabaseType item in Enum.GetValues(typeof(DatabaseType)))
            {
                Console.WriteLine((int)item);
            }

            Console.WriteLine("遍历枚举中的值");
            // 遍历枚举中的名称
            foreach (string item in Enum.GetNames(typeof(DatabaseType)))
            {
                Console.WriteLine(item);
            }

            // 枚举数组测试
            Console.WriteLine("枚举数组测试");
            var arr = Enum.GetValues(typeof(DatabaseType));
            Console.WriteLine(arr.Length);
            Console.WriteLine((int)arr.GetValue(0));
            for(int i=0;i<arr.Length;i++)
            {
                Console.WriteLine((int)arr.GetValue(i));
            }
                




        }

        public void RunTransactionServiceBaseModelTest()
        {
            TransactionServiceBusinessModel model = null;
            model = new TransactionServiceBusinessModel("return_url",
                                                                                      "soufunId",
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      decimal.Zero,
                                                                                      "title",
                                                                                      "subject: EOP DealerName|DealerID|Type|Amount",
                                                                                      "extra_param: DealerID|tradeType|backurl"
                                                                                      );
            var query = model.ToQueryString();


            //Console.WriteLine("queryStr:" + query);
            var extra_param = model.extra_param.Split('|');

            var subject = model.subject.Split('|');

            Console.WriteLine(extra_param[0]);
            Console.WriteLine(extra_param[1]);
            Console.WriteLine(extra_param[2]);

            Console.WriteLine("DealerID:" + subject[1]);
            Console.WriteLine("Type:"+subject[2]);
            Console.WriteLine("Amount:" + subject[3]);
        }

        public void RunParamsTest()
        {
            string a = null;
            DateTime date = DateTime.Now;
            object param = new { Id = 1, Name = "名称", NullColumn = a, Date = date };
            Type type = param.GetType();
            PropertyInfo[] properties = type.GetProperties();
            object value = null;
            for (int i = 0; i < properties.Length; ++i)
            {
                value = properties[i].GetValue(param,null);
                
                //if (value != null && !string.IsNullOrEmpty(value.ToString().Trim()))
                //{
                if (value==null)
                {
                    Console.WriteLine("{0}:Null", properties[i].Name);
                }
                else if (value.GetType().Name.Equals("String") || value.GetType().Name.Equals("DateTime"))
                {
                    Console.WriteLine("Type:{0}", value.GetType().Name);
                    Console.WriteLine("{0}:'{1}'", properties[i].Name,value);
                    //Console.WriteLine(string.Format("Member name:{0},value:{1},TYPE:{2}", properties[i].Name, value.ToString(), properties[i].GetValue(param, null).GetType().Name));
                }
                else
                {
                    Console.WriteLine("Type:{0}", value.GetType().Name);
                    Console.WriteLine("{0}:{1}", properties[i].Name, value);
                }
                //}
            }
        }
    }
}
