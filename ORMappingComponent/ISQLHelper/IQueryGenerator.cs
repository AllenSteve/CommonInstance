// ***********************************************************************
// Assembly         : ORMappingComponent
// Author           : Dragonet
// Created          : 12-18-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-18-2015
// ***********************************************************************
// <copyright file="QueryGenerator.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComponentORM.SQLHelper
{
    /// <summary>
    /// 修查询型的SQL生成器接口
    /// </summary>
    public interface IQueryGenerator : IQueryProvider
    {
        //IQueryable IQueryProvider.CreateQuery(Expression expression);
    }
}
