// ***********************************************************************
// Assembly         : BaseFunction
// Author           : Dragonet
// Created          : 12-25-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-25-2015
// ***********************************************************************
// <copyright file="IValueCalculater.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ComponentModels.ServiceModel.BaseServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFunction.ServiceInterface.INinjectService
{
    /// <summary>
    /// Interface IValueCalculater
    /// http://www.cnblogs.com/tylerdonet/p/3297915.html
    /// http://www.cnblogs.com/jeffwongishandsome/archive/2012/04/15/2450462.html
    /// </summary>
    public interface IValueCalculater
    {
        decimal ValueProducts(params Product[] products);
    }
}
