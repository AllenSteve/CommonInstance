﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionComponent.String;
using System.Diagnostics;
using EOPComponent.Model.Entity;

namespace StandardUnitTest.ExtensionTest
{
    [TestClass]
    public class StringExtensionTest
    {
        private string str { get; set; }

        public StringExtensionTest()
        {

        }
        
        [TestMethod]
        public void CreateSQLTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string s = str.CreateSQLInsertNewEntity<DealerSettlement>();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:000}",
                                                                    ts.Hours, ts.Minutes, ts.Seconds,
                                                                    ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
