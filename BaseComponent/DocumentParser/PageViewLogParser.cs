using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace BaseComponent.DocumentParser
{
    public class PageViewLogParser
    {
        private JArray jArray { get; set; }

        private string jText { get; set; }

        private StringBuilder jBuilder { get; set; }

        private IEnumerable<JToken> jResult { get; set; }

        public bool knowledgeAccess { get; set; }

        public bool forumAccess { get; set; }

        public bool QAAccess { get; set; }

        public bool kfAccess { get; set; }

        public bool fqAccess { get; set; }

        public bool kqAccess { get; set; }

        public bool kfqAccess { get; set; }

        public PageViewLogParser()
        {
            this.jBuilder = new StringBuilder();
        }

        /*
         * pageView格式：
         * [{"Data":[{"ET":"2015-11-27 05:22:11","PageID":"1001","ST":"2015-11-27 05:22:03"},{"ET":"2015-11-27 05:29:13","PageID":"1001","ST":"2015-11-27 05:29:06"},{"ET":"2015-11-27 05:32:02","PageID":"1001","ST":"2015-11-27 05:31:47"},{"ET":"2015-11-27 05:34:52","PageID":"1001","ST":"2015-11-27 05:34:45"},{"ET":"2015-11-27 05:35:06","PageID":"1002","ST":"2015-11-27 05:35:05"},{"ET":"2015-11-27 05:35:09","PageID":"1001","ST":"2015-11-27 05:35:05"},{"ET":"2015-11-27 05:35:23","PageID":"1018","ST":"2015-11-27 05:35:22"},{"ET":"2015-11-27 05:35:26","PageID":"1001","ST":"2015-11-27 05:35:22"},{"ET":"2015-11-27 05:36:54","PageID":"1009","ST":"2015-11-27 05:36:14"},{"ET":"2015-11-27 05:37:18","PageID":"1009","ST":"2015-11-27 05:37:07"},{"ET":"2015-11-27 05:38:16","PageID":"1009","ST":"2015-11-27 05:38:15"},{"ET":"2015-11-27 05:38:29","PageID":"1009","ST":"2015-11-27 05:38:27"},{"ET":"2015-11-27 05:41:36","PageID":"1009","ST":"2015-11-27 05:41:02"},{"ET":"2015-11-27 05:43:55","PageID":"1009","ST":"2015-11-27 05:42:44"},{"ET":"2015-11-27 05:45:14","PageID":"1001","ST":"2015-11-27 05:45:11"}],"End":"","Start":"2015-11-27 05:21:57"}]
         */
        public PageViewLogParser(string pageView)
        {
            this.jBuilder = new StringBuilder();
            this.jBuilder.Append(this.CreateJsonText(pageView));
        }

        public StringBuilder JsonTextAppend(string pageView)
        {
            return this.jBuilder = this.jBuilder.Append(this.CreateJsonText(pageView)).Replace("][", ",");
        }

        public string CreateJsonText(string pageView)
        {
            string[] array = Regex.Split(pageView, "Data", RegexOptions.IgnoreCase);
            StringBuilder jsonBuilder = new StringBuilder();
            for (int index = 1; index < array.Length; ++index)
            {
                int startIndex = array[index].LastIndexOf('[');
                int length = array[index].IndexOf(']') - startIndex + 1;
                if (length > 2)
                {
                    jsonBuilder.Append(array[index].Substring(startIndex, length));
                }
            }
            return jsonBuilder.ToString().Replace("][",",");
        }


        private JArray CreateJArray()
        {
            return JArray.Parse(this.jText);
            //return (JArray)JsonConvert.DeserializeObject(this.jText);
        }

        private IEnumerable<JToken> CreateTokenList()
        {
            if (this.jArray == null)
                return null;
            this.jResult = this.jArray.Where(j => j.ToObject<JObject>()["PageID"].ToString().Equals("1018") ||
                                                                    j.ToObject<JObject>()["PageID"].ToString().Equals("1019") ||
                                                                    j.ToObject<JObject>()["PageID"].ToString().Equals("1020"));

            return this.jResult;
        }

        public IEnumerable<JToken> GetResultList()
        {
            this.jText = this.jBuilder.ToString();
            this.jArray = this.CreateJArray();
            this.jResult = this.CreateTokenList();
            if (jResult.Count() == 0)
                return null;
            return this.jResult;
        }

        public void Perform()
        {
            this.jText = this.jBuilder.ToString();
            this.jArray = this.CreateJArray();
            this.jResult = this.CreateTokenList();

            // 1018知识列表页；
            knowledgeAccess = this.jResult.Any(o => o["PageID"].ToString().Equals("1018"));
            // 1019论坛列表页；
            forumAccess = this.jResult.Any(o => o["PageID"].ToString().Equals("1019"));
            // 1020问答列表页
            QAAccess = this.jResult.Any(o => o["PageID"].ToString().Equals("1020"));
            kfAccess = knowledgeAccess & forumAccess;
            fqAccess = forumAccess & QAAccess;
            kqAccess = knowledgeAccess & QAAccess;
            kfqAccess = knowledgeAccess & forumAccess & QAAccess;
        }

    }


}
