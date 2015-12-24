using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOP.DapperModel.Common
{
    public class EOPCityInfo
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
    public class EOPRegionInfo
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
    }
    public class EOPEstateInfo
    {
        public string EstateCode { get; set; }
        public string EstateName { get; set; }
    }
    #region 预算
    public class DebugetInfoList
    {
        /// <summary>
        /// 返回预算名称
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DecorationBudget(int i)
        {
            return DebugetList.Find(p => p.ID == i).DisplayName;
        }
        /// <summary>
        /// 返回预算ID
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int DecorationBudget(string i)
        {
            return DebugetList.Find(p => p.DisplayName == i).ID;
        }
        /// <summary>
        /// 将[1],[2],[3]转换为0-5万,5-10万,10-15万
        /// </summary>
        /// <returns></returns>
        public static string SolutionNames(string IDs)
        {
            return string.Join(",", IDs.Split(',').Select(t => DebugetList.Find(a => a.ID.ToString() == t.Replace("[", "").Replace("]", ""))).Select(q => q.DisplayName).ToList());
        }
        /// <summary>
        /// 将0-5万,5-10万,10-15万转换为[1],[2],[3]
        /// </summary>
        /// <returns></returns>
        public static string SolutionIDs(string Names)
        {
            return string.Join(",", Names.Split(',').Select(t => DebugetList.Find(a => a.DisplayName.ToString() == t)).Select(q => "[" + q.ID + "]").ToList());
        }
        private static List<DebugetInfo> debugetInfoList = new List<DebugetInfo>();
        public static List<DebugetInfo> DebugetList
        {
            get
            {
                if (debugetInfoList == null || debugetInfoList.Count <= 0)
                {
                    debugetInfoList.AddRange(new DebugetInfo[6] { 
                    new DebugetInfo(){ ID = 1, MinAmount = 0 , MaxAmount = 5 , DisplayName="0-5万"},
                    new DebugetInfo(){ ID = 2, MinAmount = 5 , MaxAmount = 10 , DisplayName="5-10万"},
                    new DebugetInfo(){ ID = 3, MinAmount = 10 , MaxAmount = 15 , DisplayName="10-15万"},
                    new DebugetInfo(){ ID = 4, MinAmount = 15 , MaxAmount = 20 , DisplayName="15-20万"},
                    new DebugetInfo(){ ID = 5, MinAmount = 20 , MaxAmount = 30 , DisplayName="20-30万"},
                    new DebugetInfo(){ ID = 6, MinAmount = 30 , MaxAmount = 0 , DisplayName="30万以上"}
                });
                }
                return debugetInfoList;
            }
        }
    }



    public class DebugetInfo
    {
        public int ID { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public string DisplayName { get; set; }
    }
    #endregion
}
