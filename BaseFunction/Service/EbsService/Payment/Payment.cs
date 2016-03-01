// ***********************************************************************
// Assembly         : EBS.Service.Bussiness.PaymentService
// Author           : 仇士龙
// Created          : 02-03-2016
//
// Last Modified By : 仇士龙
// Last Modified On : 02-03-2016
// ***********************************************************************
// <copyright file="Payment.cs" company="Microsoft">
//     Copyright (c) 搜房网房天下家居集团电商总部
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseFunction.ServiceInterface.IEbsServcie;
using BaseComponent.Debugger;
using EBS.Interface.Payment.Model;
using ComponentModels.EbsDBModel;

namespace BaseFunction.Service.EbsService
{
    public partial class PaymentService :IPayment
    {
        /// <summary>
        /// 数据库连接字符串-只读
        /// </summary>
        private static string PAYMENT_READ_CONSTR = ConfigurationManager.ConnectionStrings["EBS_READ"].ConnectionString;

        /// <summary>
        /// 数据库连接字符串-只写
        /// </summary>
        private static string PAYMENT_WRITE_CONSTR = ConfigurationManager.ConnectionStrings["EBS_WRITE"].ConnectionString;

        /// <summary>
        /// 数据库连接字符串-沙箱库
        /// </summary>
        private static string PAYMENT_READ_TEST_CONSTR = ConfigurationManager.ConnectionStrings["EBS_READ_TEST"].ConnectionString;

        /// <summary>
        /// 数据库连接字符串-沙箱库
        /// </summary>
        private static string PAYMENT_WRITE_TEST_CONSTR = ConfigurationManager.ConnectionStrings["EBS_WRITE_TEST"].ConnectionString;

        /// <summary>
        /// 打开到数据库的连接
        /// </summary>
        private SqlConnection read_connection;

        /// <summary>
        /// 打开到数据库的连接
        /// </summary>
        private SqlConnection write_connection;

        protected SqlConnection ReadConnection
        {
            get
            {
                if (read_connection == null)
                {
                    read_connection = PaymentService.CreateConnection();
                }
                return read_connection;
            }
        }

        protected SqlConnection WriteConnection
        {
            get
            {
                if (write_connection == null)
                {
                    write_connection = PaymentService.CreateConnection(1);
                }
                return write_connection;
            }
        }

        private static SqlConnection CreateConnection(int db_type = 0)
        {
            if(db_type == 1)
            {
                return new SqlConnection(PaymentService.PAYMENT_WRITE_CONSTR);
            }
            else if (db_type == 2)
            {
                return new SqlConnection(PaymentService.PAYMENT_READ_TEST_CONSTR);
            }
            else if (db_type == 3)
            {
                return new SqlConnection(PaymentService.PAYMENT_WRITE_TEST_CONSTR);
            }
            else
            {
                return new SqlConnection(PaymentService.PAYMENT_READ_CONSTR);
            }
        }

        protected PaymentInfo paymentInfo { get; set; }
        private static string SQL { get; set; }
        protected DateTime startTime { get; set; }
        protected IEnumerable<N_Payment_SchemeItem> paymentNodeList { get; set; }
        protected bool isDebugMode { get; set; }

        protected DebugHelper debug { get; set; }

        public PaymentService()
        {
            this.startTime = DateTime.Now;
            this.paymentInfo = new PaymentInfo();
            this.debug = new DebugHelper();
        }

    }
}
