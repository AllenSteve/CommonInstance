using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionComponent;
using EOPComponent.Model;
using EOPComponent.Model.ScoreAccumulation;
using EOPComponent.Enums.Method;
using System.Collections.Generic;

namespace StandardUnitTest.ExtensionTest
{
    [TestClass]
    public class DapperRowExtensionTest
    {

        private CompanyScoreOperationModel model { get; set; }

        public DapperRowExtensionTest()
        {

        } 


        [TestMethod]
        public void QueryTest()
        {
            ScoreUpdateMethod method = new ScoreUpdateMethod();
            object param = method.CreateQueryParam();

            IEnumerable<object> list = method.Query(ScoreUpdateMethod.QueryFollowUpLogCompanyList(), param);
            foreach(var item in list)
            {
                var record = (IDictionary<string, object>)item;
                CompanyFollowUpModel result = record.ConvertDapperRowTo<CompanyFollowUpModel>();
                
            }

            //// 查询业主日记反馈
            //IEnumerable<CompanyScoreOperationModel> followUpResult = method.Query(ScoreUpdateMethod.QueryFollowUp(), method.remarkScore, param);
            //// 查询工程质量验收合格记录
            //IEnumerable<CompanyScoreOperationModel> constructResult = method.Query(ScoreUpdateMethod.QueryAcceptanceRecord(), method.qualifiedScore, param);
        }
    }
}
