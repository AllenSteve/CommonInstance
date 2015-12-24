using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBS.Interface.EContract.Model
{
    public class Payment
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public int StateCode { get; set; }
        /// <summary>
        /// 节点名
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 收款比例
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 计算后金额
        /// </summary>
        public decimal Money { get; set; }
    }
}