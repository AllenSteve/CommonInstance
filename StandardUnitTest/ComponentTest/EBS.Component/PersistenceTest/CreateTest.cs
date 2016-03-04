using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EBSComponent.Persistence;
using EBSComponent.Enums;
using ComponentModels.EbsDBModel;
using System.Collections.Generic;
using System.Linq;
using BaseComponent.Debugger;

namespace StandardUnitTest.ComponentTest.EBS.Component.PersistenceTest
{
    [TestClass]
    public class PersistenceCreateTest
    {
        private PersistenceEBS persistence { get; set; }
        private DebugHelper debug { get; set; }

        public PersistenceCreateTest()
        {
            int databaseRead = (int)DatabaseEnum.LOCAL_DATABASE;
            int databaseWrite = (int)DatabaseEnum.LOCAL_DATABASE;
            this.persistence = new PersistenceEBS(databaseRead, databaseWrite);
            this.debug = new DebugHelper();
        }

        [TestMethod]
        public void CreatePersistenceTest()
        {
            int databaseRead = (int)DatabaseEnum.EBS_READ;
            int databaseWrite = (int)DatabaseEnum.EBS_WRITE;
            this.persistence = new PersistenceEBS(databaseRead,databaseWrite);
        }

        /// <summary>
        /// 插入单条记录测试
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            N_Order_Base order = new N_Order_Base();
            order = this.debug.PadValue(order);
            int ret = this.persistence.Add(order);
            Assert.AreEqual(1, ret);
        }

        /// <summary>
        /// 批量插入测试
        /// </summary>
        [TestMethod]
        public void AddRangeTest()
        {
            List<N_Order_Base> list = new List<N_Order_Base>();
            N_Order_Base order1 = new N_Order_Base();
            N_Order_Base order2 = new N_Order_Base();

            list.Add(this.debug.PadValue(order1));
            list.Add(this.debug.PadValue(order2));
            int ret = this.persistence.AddRange(list);
            Assert.AreEqual(2, ret);
        }

        /// <summary>
        /// 事务型新增
        /// </summary>
        [TestMethod]
        public void AddRangeWithTransactionTest()
        {
            List<N_Order_Base> list = new List<N_Order_Base>();
            N_Order_Base order1 = new N_Order_Base();
            N_Order_Base order2 = new N_Order_Base();

            // 不新增，直接返回
            list.Add(order1);
            list.Add(this.debug.PadValue(order2));
            int ret = this.persistence.AddRange(list,true);
            Assert.AreEqual(0, ret);

            // 此时会先新增再删除,此时自增ID会出现不连续的情况
            list.Clear();
            list.Add(this.debug.PadValue(order2));
            list.Add(order1);
            ret = this.persistence.AddRange(list, true);
            Assert.AreEqual(0, ret);
        }
    }
}
