using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponent.CommonMethod
{
    public class SyncEopCompanyScore
    {
        public SyncEopCompanyScore()
        {
        }

        /// <summary>
        /// 执行公司积分同步
        /// </summary>
        public void UpdateCompanyScore()
        {
            string DB_CONNECTION = ConfigurationManager.ConnectionStrings["TestServerDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(DB_CONNECTION))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(SyncEopCompanyScore.CreateUpdateSQL(), conn);
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
            sql.Append(" INNER JOIN dbo.Partner_CompanyExtent AS E");
            sql.Append(" ON E.DealerID = P.ID");
            return sql.ToString();
        }
    }
}
