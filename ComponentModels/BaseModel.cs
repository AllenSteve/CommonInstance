using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentModels
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Age { get; set; }
        public string Phone { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is BaseModel)
            {
                var baseModel = obj as BaseModel;
                //return person.Age == this.Age && person.Name == this.Name;
                return baseModel.Phone == this.Phone;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            //return this.Name.GetHashCode() ^ this.Age.GetHashCode();
            return this.Phone.GetHashCode();
        }
        
    }
}
