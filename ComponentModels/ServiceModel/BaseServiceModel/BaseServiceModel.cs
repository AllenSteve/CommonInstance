// ***********************************************************************
// Assembly         : ComponentModels
// Author           : Dragonet
// Created          : 12-25-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-25-2015
// ***********************************************************************
// <copyright file="BaseServiceModel.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ComponentModels.ServiceModel.BaseServiceModel
{
    /// <summary>
    /// ServiceModel基类
    /// </summary>
    public class BaseServiceModel
    {
        private ServiceMessage serviceMessage { get; set; }

        private ServiceReturn serviceReturn { get; set; }

        public BaseServiceModel()
        {
        }

        public BaseServiceModel(ServiceMessage serviceMessage,ServiceReturn serviceReturn)
        {
        }
    }
}
