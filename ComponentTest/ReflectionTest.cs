using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ComponentTest.CommonClass;

namespace ComponentTest
{
    /// <summary>
    /// 反射测试类
    /// </summary>
    public class ReflectionTest
    {
        /// <summary>
        /// 返回行数
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>行数</returns>
        public int RowCount<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] p = t.GetProperties();
            Console.WriteLine(p.Length);
            return p.Length;
        }

        /// <summary>
        /// ファイルの中のデータの序列をList形式のデータに変わります
        /// 使用泛型方法加上反射能够充分扩展该函数的功能，使之能够适应更多类型的转换。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>データlist</returns>
        public List<T> SerializeToList<T>(string path) where T : new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo property = null;
            string[,] array = new Serializer().SerializeToArray(path, properties.Length);
            for (int rowIndex = 0; rowIndex < array.GetLength(0); ++rowIndex)
            {
                T type = new T();
                for (int i = 0; i < properties.Length; ++i)
                {
                    property = type.GetType().GetProperty(properties[i].Name);
                    property.SetValue(type, array[rowIndex, i], null);
                }

                list.Add(type);
            }

            return list;
        }

        /// <summary>
        /// 11
        /// </summary>
        /// <typeparam name="T">111</typeparam>
        /// <param name="list">1111</param>
        /// <returns>11111</returns>
        public string[] ConvertToArray<T>(List<T> list)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo property = null;
            string[] buffer = new string[list.Count];
            for (int rowIndex = 0; rowIndex < list.Count; ++rowIndex)
            {
                for (int columnIndex = 0; columnIndex < properties.Length; ++columnIndex)
                {
                    property = list[rowIndex].GetType().GetProperty(properties[columnIndex].Name);
                    buffer[rowIndex] += (string)property.GetValue(list[rowIndex], null) + "\t";
                }

                buffer[rowIndex] = buffer[rowIndex].Substring(0, buffer[rowIndex].Length - 1);
            }

            return buffer;
        }

        /// <summary>
        /// 11
        /// </summary>
        /// <typeparam name="T">111</typeparam>
        /// <param name="list">1111</param>
        /// <returns>11111</returns>
        public string ConvertToString<T>(List<T> list)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo property = null;
            string[] insertArray = this.CreateInsertArray(properties.Length);
            StringBuilder buffer = new StringBuilder(list.Count * properties.Length);
            for (int rowIndex = 0; rowIndex < list.Count; ++rowIndex)
            {
                for (int columnIndex = 0; columnIndex < properties.Length; ++columnIndex)
                {
                    property = list[rowIndex].GetType().GetProperty(properties[columnIndex].Name);
                    buffer.Append(property.GetValue(list[rowIndex], null));
                    buffer.Append(insertArray[rowIndex]);
                }
            }

            return buffer.ToString(0, buffer.Length - 2);
        }

        /// <summary>
        /// 1
        /// </summary>
        /// <param name="size">2</param>
        /// <returns>3</returns>
        private string[] CreateInsertArray(int size)
        {
            string[] insertArray = new string[size];
            for (int i = 0; i < size; ++i)
            {
                insertArray[i] = "\t";
            }
               
            insertArray[size - 1] = "\r\n";
            return insertArray;
        }
    }
}
