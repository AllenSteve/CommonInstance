using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionComponent
{
    public static class DapperRowExtension
    {
        public static T ConvertDapperRowTo<T>(this object row)where T : new()
        {
            T result = new T();
            Type columnType = null;
            string valueString = string.Empty;
            PropertyInfo[] properties = typeof(T).GetProperties();
            IDictionary<string, object> record = (IDictionary<string, object>)row;
            for (int i = 0; i < properties.Length; ++i)
            {
                columnType = properties[i].PropertyType;
                if (record.ContainsKey(properties[i].Name) && record[properties[i].Name]!=null)
                {
                    valueString = record[properties[i].Name].ToString();
                    properties[i].SetValue(result, Convert.ChangeType(valueString, columnType));
                }
            }
            return result;
        }
    }
}
