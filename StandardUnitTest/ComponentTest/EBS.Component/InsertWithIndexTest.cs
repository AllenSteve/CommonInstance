using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EbsComponent.Model.Entity;
using ComponentORM.ORMappingTools;
using System.Diagnostics;
using System.Data;
using Dapper;

namespace StandardUnitTest.ComponentTest.EBS.Component
{
    [TestClass]
    public class InsertWithIndexTest
    {

        // arrange
        private DBHelper db { get; set; }
        private Random rand { get; set; }

        public InsertWithIndexTest()
        {
            db = new DBHelper((int)DBHelper.Sqldb.CommentServerDB);
            this.rand = new Random();
        }

        [TestMethod]
        public void TestMethod1()
        {
           
            int i = 100;
            long[] projectIdList = new[] { 56965231L, 73231921L };
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (i-- > 0)
            {
                foreach (var projectId in projectIdList)
                {
                    var comment = this.CreateCommentRecord(projectId);
                    this.db.TransactionAdd(comment);
                    //this.db.Add(this.CreateCommentRecord(projectId));
                }
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                                                                    ts.Hours, ts.Minutes, ts.Seconds,
                                                                    ts.Milliseconds);
        }

        private Comment_1_1 CreateCommentRecord(long projectId)
        {
            return new Comment_1_1()
            {
                CONTENT = DateTime.Now.ToLongTimeString(),
                ContentType = 0,
                SoufunID = this.rand.Next(1000000, int.MaxValue),
                SoufunName = "测试用户",
                TrueName = "小明",
                IdentityType = 0,
                ProjectID = projectId,
                ProjectSoufunID = projectId,
                ProjectIdentityType = 1,
                ProjectDisplayName = "测试项目",
                ProjectFunctionName = "家装顾问",
                ProjectRemark = string.Empty,
                ReplyMessageID = string.Empty,
                CommentType = 5,
                MessageID = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(12),
                IsDel = 0,
                IsPublic = 1,
                RootMessageID = string.Empty,
                CreateTime = DateTime.Now,
                FunctionName = "业主",
                PictureUrls = string.Empty,
                Remark = string.Empty
            };
        }
    }
}
