using ComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionComponent;

namespace ComponentTest
{
    public class BaseModelTest
    {
        private BaseModel baseModel { get; set; }

        private List<BaseModel> baseModelList { get; set; }

        public BaseModelTest()
        {
            baseModel = new BaseModel();

            baseModelList = new List<BaseModel>();
        }

        public void RunDistinctExtension()
        {
            baseModel = new BaseModel() { Id = 1, Age = 1, Name = "allen", Phone = "13851870675" };
            baseModelList.Add(baseModel);
            baseModel = new BaseModel() { Id = 2, Age = 2, Name = "bllen", Phone = "13121125599" };
            baseModelList.Add(baseModel);
            baseModel = new BaseModel() { Id = 3, Age = 3, Name = "cllen", Phone = "13851870675" };
            baseModelList.Add(baseModel);
            baseModel = new BaseModel() { Id = 4, Age = 4, Name = "dllen", Phone = "13121125599" };
            baseModelList.Add(baseModel);

            List<BaseModel> distList = new List<BaseModel>();

            distList = baseModelList.Distinct(model => model.Phone).ToList();
            int count = -1;
            Console.WriteLine("Distinct列表元素数为:" + distList.Count());
            foreach (var item in distList)
            {
                count = baseModelList.Count(model => model.Phone.GetHashCode() == item.GetHashCode());
                Console.WriteLine("手机号为：" + item.Phone + "\t的记录共："+count+"\t个");
            }
        }

    }
}
