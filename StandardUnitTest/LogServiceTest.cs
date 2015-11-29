using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using ORMappingComponent;
using DapperModel.EBS;
using EOP.DapperModel;
using EOP.Enums;
namespace EOP.UnitTest
{
    [TestClass]
    public class LogServiceTest
    {
        [TestMethod]
        public void AddDealerServiceLogTest()
        {
            // arrange
            DBHelper db = new DBHelper();
            DealerServiceLog log = new DealerServiceLog()
            {
                ServiceType = (int)ServiceType.已签约,
                DealerID=1,//(int)DateTime.Now.Ticks,
                ApplyID=0,
                OperatorSoufunID=10000,
                OperatorRoleID=(int)OperatorRole.客服,
                OperatorRoleName=OperatorRole.客服.ToString(),
                OperatorRealName="张亮亮",
                OrderID="6372637284783",
                Description = "积分奖励3多米",
                CreateTime=DateTime.Now,
                IsDel=0
            };
            int count = 0;

            // act
            db.ExecuteSQL("DELETE FROM DealerServiceLog", null);

            count = db.Add<DealerServiceLog>(log);
            // assert
            Assert.AreEqual(1, count);

            log.ServiceType = (int)ServiceType.退单失败;
            log.OperatorRoleID = (int)OperatorRole.运营;
            log.OperatorRoleName = OperatorRole.运营.ToString();
            log.OperatorRealName = "张亮亮";
            log.Description = "退单申请被驳回";
            // act
            count = db.Add<DealerServiceLog>(log);
            // assert
            Assert.AreEqual(1, count);

            log.ServiceType = (int)ServiceType.退单成功;
            log.OperatorRoleID = (int)OperatorRole.运营经理;
            log.OperatorRoleName = OperatorRole.运营经理.ToString();
            log.OperatorRealName = "张亮亮";
            log.Description = "积分返还 3多米，信息费返款 166元";
            // act
            count = db.Add<DealerServiceLog>(log);
            // assert
            Assert.AreEqual(1, count);

            log.ServiceType = (int)ServiceType.申请退单;
            log.OperatorRoleID = (int)OperatorRole.商家;
            log.OperatorRoleName = OperatorRole.商家.ToString();
            log.OperatorRealName = "张媛媛";
            log.Description = null;
            // act
            count = db.Add<DealerServiceLog>(log);
            // assert
            Assert.AreEqual(1, count);

            //DealerServiceLog L = db.Query<DealerServiceLog>("SELECT * FROM DealerServiceLog WHERE ID=78").FirstOrDefault();

            //L.Description = "UPDATE<T>():测试用例";

            //db.Update<DealerServiceLog>(L);
        }

        [TestMethod]
        public void AddCallbackServiceLogTest()
        {
            DBHelper db = new DBHelper();
            db.ExecuteSQL("DELETE FROM DealerServiceLog", null);
            // arrange
            DealerServiceLog log = new DealerServiceLog()
            {
                ServiceType = (int)ServiceType.已竣工,
                DealerID = 2,//(int)DateTime.Now.Ticks,
                ApplyID = 0,
                OperatorSoufunID = 20000,
                OperatorRoleID = (int)OperatorRole.监理,
                OperatorRoleName = OperatorRole.监理.ToString(),
                OperatorRealName = "渣与与",
                SendOrderID = null,
                CallbackRemark = null,
                CreateTime = DateTime.Now,
                ApplySourcePageUrl = null,
                SendOrderCompany = null,
                SigningCompany = null,
                IsDel = 0
            };
            int count = 0;
            // act 
            count = db.Add<DealerServiceLog>(log);
            // assert
            Assert.AreEqual(1, count);

            // 已开工
            log.ServiceType = (int)ServiceType.已开工;
            log.OperatorRoleID = 0;
            log.OperatorRoleName = null;
            log.OperatorRealName = null;
            log.CreateTime = DateTime.Now;
            db.Add<DealerServiceLog>(log);

            // 已签约
            // “已签约”，就只显示1家签约公司，不显示派单公司的信息了
            log.ServiceType = (int)ServiceType.已签约;
            log.OperatorRoleID = (int)OperatorRole.运营;
            log.OperatorRoleName = OperatorRole.运营.ToString();
            log.OperatorRealName = "渣与与";
            log.SigningCompany = "嘻嘻装饰公司";
            log.CreateTime = DateTime.Now;
            db.Add<DealerServiceLog>(log);

            // 已派单
            // 展示派单公司--顺序先派单后签约，如果有
            log.ServiceType = (int)ServiceType.已派单;
            log.SendOrderCompany = "嘻嘻装饰公司，闫亚白装饰公司，美俄可解装饰公司";
            log.SigningCompany = null;
            log.CreateTime = DateTime.Now;
            db.Add<DealerServiceLog>(log);

            // 待派单
            log.ServiceType = (int)ServiceType.待派单;
            log.OperatorRoleID = (int)OperatorRole.客服;
            log.OperatorRoleName = OperatorRole.客服.ToString();
            log.OperatorRealName = "张亮亮";
            log.SendOrderCompany = null;
            log.SigningCompany = null;
            log.CreateTime = DateTime.Now;
            db.Add<DealerServiceLog>(log);

            // 已回访
            log.ServiceType = (int)ServiceType.已回访;
            log.OperatorRoleID = (int)OperatorRole.客服;
            log.OperatorRoleName = OperatorRole.客服.ToString();
            log.OperatorRealName = "张亮亮";
            log.CallbackRemark = "回访备注回访备注回访备注回访备注回访备注回访备注回访备注回访备注回访备注回访备注回访备注回访备注回访备注";
            log.CreateTime = DateTime.Now;
            db.Add<DealerServiceLog>(log);

            // 已报名
            log.ServiceType = (int)ServiceType.已报名;
            log.OperatorRoleID = 0;
            log.OperatorRoleName = null;
            log.OperatorRealName = null;
            log.CallbackRemark = null;
            log.CreateTime = DateTime.Now;
            db.Add<DealerServiceLog>(log);
        }

        [TestMethod]
        public void AddPaymentLogTest()
        {
            /*
                POST:
                biz_id=747420150428100001
                extra_param=extra_param
                notify_id=66ab5067e086453892133b3cf03be2bc
                out_trade_no=123456789
                paid_amount=0.01
                sign=28f2bc6d4515c49fc2e7a2ed69cadfed
                sign_type=MD5
                third_part_amount=
                third_part_id=
                third_part_type=0
                trade_amount=0.01
                trade_no=T_TRADE_20151127_0000000033
                trade_state=SUCCESS
                trade_time=2015-11-27+16%3a24%3a53
                use_balance_amount=0.00
             */
            // arrange
            DBHelper db = new DBHelper();
            PayNotice_2_0_Log log = new PayNotice_2_0_Log()
            {
                notify_id = "66ab5067e086453892133b3cf03be2bc",
                biz_id = "747420150428100001",
                trade_no=null,
                out_trade_no="123456789",//可用guid代替
                trade_state = "SUCCESS",
                trade_time=DateTime.Parse("2015/11/27 16:24:53"),
                trade_amount = decimal.Parse("0.01"),
                extra_param = "extra_param:DealerID|tradeType|backurl",
                sign_type="MD5",
                third_part_type=0,
                third_part_id=null,
                third_part_amount=null,
                Remark=null,
                paid_amount = decimal.Parse("0.01"),
                CreateTime=DateTime.Now,
                IsDel=0
            };
            int count = 0;
            // act 
            count = db.Add<PayNotice_2_0_Log>(log);
            // assert
            Assert.AreEqual(1, count);

        }
    }
}
