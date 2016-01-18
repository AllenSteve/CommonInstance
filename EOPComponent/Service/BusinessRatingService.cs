// ***********************************************************************
// Assembly         : EOP.Service
// Author           : 仇士龙
// Created          : 12-05-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-14-2015
// ***********************************************************************
// <copyright file="BusinessRatingService.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOPComponent.ServiceInterface;
using ComponentORM.ORMappingTools;
using EOPComponent.Model.Entity;

/// <summary>
/// The Service namespace.
/// </summary>
namespace EOP.Service
{
    /// <summary>
    /// Class BusinessRatingService.
    /// </summary>
    public class BusinessRatingService : IBusinessRatingService
    {
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        private DBHelper db { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRatingService" /> class.
        /// </summary>
        public BusinessRatingService()
        {
            db = new DBHelper();
        }

        public int? GetScoreByConfig(int typeId, int scoreId)
        {
            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            StringBuilder sql = new StringBuilder("SELECT * FROM ScoreConfig ");
            object paramArray = null;
            sql.Append(" WHERE TypeID=@TypeID AND  ScoreID=@ScoreID");
            paramArray = new { TypeID = typeId, ScoreID = scoreId };
            // 如果查询结果为null值此处where(o=>o.IsDel==0)会抛出异常
            ScoreConfig scoreRecord = db.Query<ScoreConfig>(sql.ToString(), paramArray)
                                                            .Where(o=>o.IsDel==0)
                                                            .FirstOrDefault();
            if (scoreRecord == null)
                return null;
            return int.Parse(scoreRecord.ScoreNumber);
        }

        public SolutionConfig GetSolutionByMargin(decimal dealerMargin)
        {
            return this.GetSolutionByBudget(0,dealerMargin);
        }

        /// <summary>
        /// Verifies the database.
        /// </summary>
        /// <param name="useDB">The use database.</param>
        private void VerifyDB(DBHelper.Sqldb useDB)
        {
            if (this.db.dbType != useDB)
            {
                this.db.SwitchDB(useDB);
            }
        }

        public SolutionConfig GetSolutionByBudget(int typeId, decimal budgetMinima)
        {
            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            StringBuilder sql = new StringBuilder("SELECT * FROM SolutionConfig WHERE TypeID=@TypeID");
            SolutionConfig solution = db.Query<SolutionConfig>(sql.ToString(), new { TypeID = typeId })
                                                      .Where(s => s.SolutionMinBudget <= budgetMinima &&
                                                                 s.SolutionMaxBudget > budgetMinima &&
                                                                 s.IsDel==0)
                                                      .FirstOrDefault();
            return solution;
        }


        public IEnumerable<ScoreConfig> GetScoreConfigList()
        {
            this.VerifyDB(DBHelper.Sqldb.UserReadOnly);
            StringBuilder sql = new StringBuilder("SELECT * FROM SolutionConfig WHERE TypeID=@TypeID");
            IEnumerable<ScoreConfig> scoreConfigList = db.QueryAll<ScoreConfig>();
            return scoreConfigList;
        }
    }
}
