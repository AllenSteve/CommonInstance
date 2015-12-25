// ***********************************************************************
// Assembly         : ComponentModels
// Author           : Dragonet
// Created          : 12-25-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-25-2015
// ***********************************************************************
// <copyright file="ServiceReturn.cs" company="Microsoft">
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
    /// 服务返回
    /// </summary>
    public class ServiceReturn
    {
        public object ReturnValue { get; set; }

        public bool Success { get; set; }

        public ParamArrayAttribute param { get; set; }
    }
}
