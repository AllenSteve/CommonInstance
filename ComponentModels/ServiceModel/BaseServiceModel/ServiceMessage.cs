// ***********************************************************************
// Assembly         : ComponentModels
// Author           : Dragonet
// Created          : 12-25-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-25-2015
// ***********************************************************************
// <copyright file="ServiceMessage.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels.ServiceModel.BaseServiceModel
{
    /// <summary>
    /// 服务消息
    /// </summary>
    public class ServiceMessage
    {
        private Guid Id { get; set; }

        private double timestamp { get; set; }

        /// <summary>
        /// 消息Id
        /// </summary>
        /// <value>The message identifier.</value>
        public string MessageId 
        {
            get 
            {
                if (this.Id == null)
                {
                    this.Id = Guid.NewGuid();
                }
                return this.Id.ToString();
            }
        }

        /// <summary>
        /// 消息时间戳
        /// </summary>
        /// <value>The timestamp.</value>
        public double Timestamp
        {
            get
            {
                if (this.timestamp >= 0L)
                {
                    this.timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                }
                return this.timestamp;
            }
        }

        /// <summary>
        /// 返回消息类型
        /// </summary>
        /// <value>The type of the return.</value>
        public int ReturnType { get; set; }

        /// <summary>
        /// 返回类型名称
        /// </summary>
        /// <value>The name of the return type.</value>
        public string ReturnTypeName { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <value>The return message.</value>
        public string ReturnMessage { get; set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        /// <value>The type of the error.</value>
        public int ErrorType { get; set; }

        /// <summary>
        /// 错误类型名称
        /// </summary>
        /// <value>The name of the error type.</value>
        public int ErrorTypeName { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        /// <value>The type of the exception.</value>
        public int ExceptionType { get; set; }

        /// <summary>
        /// 异常类型名称
        /// </summary>
        /// <value>The name of the exception type.</value>
        public int ExceptionTypeName { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        /// <value>The exception message.</value>
        public string ExceptionMessage { get; set; }
    }
}
