using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentORM.SQLHelper;
using System.Linq.Expressions;

namespace ComponentTest.ConsoleApp
{
    /// <summary>
    /// http://www.cnblogs.com/Terrylee/archive/2008/08/25/custom-linq-provider-part-2-IQueryable-IQueryProvider.html
    /// </summary>
    public class OrmQueryTest
    {

        public OrmQueryTest()
        {
            var myList1 = new List<String>() { "a", "ab", "cd", "bd" }.AsQueryable<String>();

            IQueryable<String> query1 = from sq in myList1
                                       where sq.StartsWith("a")
                                       select sq;
            var ret = query1.ToList();

            var spList = new List<int>() { 1, 2, 3, 4 }.AsQueryable<int>();
            Repository<int> sp = new Repository<int>(from sq in spList
                                  where sq>1
                                  select sq);

            var spret = sp.Filter(o => o < 3).FirstOrDefault();
            Console.WriteLine(spret);


            Query<int> ss = new Query<int>(new QueryProvider());
            var t = from item in ss where item == 123 select item;
            int ta = t.FirstOrDefault();

            Expression<Func<int, int, int>> lambda = (a, b) => a + b * 2;

            Console.WriteLine(lambda.ToString());

            List<String> myList = new List<String>() { "a", "ab", "cd", "bd" };

            IEnumerable<String> query = from s in myList
                                        where s.StartsWith("a")
                                        select s;

            foreach (String s in query)
            {
                Console.WriteLine(s);
            }

            Console.Read();
        }
        
    }
}
