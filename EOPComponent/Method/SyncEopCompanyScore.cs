using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Method
{
    public class SyncEopCompanyScore
    {
        private static string DB_CONNECTION = ConfigurationManager.ConnectionStrings["EBS_WRITE"].ConnectionString;

        public SyncEopCompanyScore()
        {
        }

        /// <summary>
        /// 执行公司积分同步--注意该方法只能执行一次，慎用
        /// </summary>
        public void UpdateCompanyScore()
        {
            using (SqlConnection conn = new SqlConnection(SyncEopCompanyScore.DB_CONNECTION))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(SyncEopCompanyScore.CreateRandomUpdateSQL(), conn);
                command.ExecuteNonQuery();
            }
        }

        public void StatisticsCompanyLastDayScore()
        {
            using (SqlConnection conn = new SqlConnection(SyncEopCompanyScore.DB_CONNECTION))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(SyncEopCompanyScore.CreateStatisticsUpdateSQL(), conn);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 生成积分同步SQL
        /// </summary>
        /// <returns>SQL语句</returns>
        private static string CreateUpdateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Partner_Company");
            sql.Append(" SET Score = E.Score");
            sql.Append(" FROM Partner_Company AS P");
            sql.Append(" INNER JOIN Partner_CompanyExtent AS E");
            sql.Append(" ON E.DealerID = P.ID");
            return sql.ToString();
        }

        /// <summary>
        /// 生成积分同步SQL-增加对特殊公司的处理
        /// </summary>
        /// <returns>SQL语句</returns>
        private static string CreateRandomUpdateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Partner_Company");
            sql.Append(" SET Score =CAST(1000*(RAND(CHECKSUM(NEWID()))+2)  AS DECIMAL(38,0))");
            sql.Append(" FROM Partner_Company AS P");
            sql.Append(" INNER JOIN dbo.Partner_CompanyExtent AS E");
            sql.Append(" ON E.DealerID = P.ID");
            sql.Append(" WHERE   P.CompanyType = 2");
            sql.Append(" AND P.CompanyLevel = 3");
            return sql.ToString();
        }

        /// <summary>
        /// 积分统计更新语句
        /// </summary>
        /// <returns>更新SQL</returns>
        private static string CreateStatisticsUpdateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Partner_Company");
            sql.Append(" SET LastDayScore = ScoreChangeAmount");
            sql.Append(" FROM Partner_Company AS C");
            sql.Append(" INNER JOIN (");
            sql.Append(" SELECT  N.SoufunId AS CompanyId,");
            sql.Append("         SUM(ABS(N.ChangeAmount)) AS ScoreChangeAmount");
            sql.Append(" FROM N_ServicePoint_Log AS N");
            sql.Append(" INNER JOIN Partner_Company AS P ON P.ID = N.SoufunId");
            sql.Append(" INNER JOIN dbo.Partner_CompanyExtent AS E ON E.DealerID = P.ID");
            sql.Append(" WHERE P.CompanyType = 2");
            sql.Append("       AND P.CompanyLevel = 3");
            sql.Append("       AND N.CauseSource IN('-1','-2','-3','-4','-5','-6','-7')");
            sql.Append(" GROUP BY N.SoufunId) ");
            sql.Append(" AS CompanyScoreLog ON CompanyScoreLog.CompanyId = C.ID");
            return sql.ToString();
        }
    }
}
