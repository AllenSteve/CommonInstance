using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.ConsoleApp.CommonClass
{
    public class Serializer
    {
        public Serializer()
        { 
        }

        /// <summary>
        /// 将目标文件序列化为数组形式的数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public string[,] SerializeToArray(string path, int columnCount)
        {
            string[] Array = File.ReadAllLines(path, Encoding.GetEncoding("shift-jis"));
            string[] rowArray;
            string[,] array = new string[Array.Length, columnCount];
            for (int rowIndex = 0; rowIndex < Array.Length; ++rowIndex)
            {
                rowArray = Array[rowIndex].Split('\t');
                for (int columnIndex = 0; columnIndex < columnCount; ++columnIndex)
                    array[rowIndex, columnIndex] = rowArray[columnIndex];
            }

            return array;
        }


        /// <summary>
        /// 最初的方法，相对较笨拙一些，可扩展性不强，
        /// 相关的类型发生变动这边的代码也要相应的做更改。
        /// 新方法参照ReflectionTest类中的反射泛型方法
        /// </summary>
        /// <param name="path"></param>
        /// <param name="colCount"></param>
        /// <returns></returns>
        public List<SankagetuMatomeBunkaieLicenseFromFile> SerializeToList(string path, int colCount)
        {
            List<SankagetuMatomeBunkaieLicenseFromFile> list = new List<SankagetuMatomeBunkaieLicenseFromFile>();
            string[,] array = this.SerializeToArray(path, colCount);
            for (int rowIndex = 0; rowIndex < array.GetLength(0); ++rowIndex)
            {
                SankagetuMatomeBunkaieLicenseFromFile sankagetuMatomeBunkaieLicenseFromFile = new SankagetuMatomeBunkaieLicenseFromFile();
                sankagetuMatomeBunkaieLicenseFromFile.InterfaceKey = (string)array[rowIndex, 0];
                sankagetuMatomeBunkaieLicenseFromFile.ContentsClass = (string)array[rowIndex, 1];
                sankagetuMatomeBunkaieLicenseFromFile.ContentsBranchNo = (string)array[rowIndex, 2];
                sankagetuMatomeBunkaieLicenseFromFile.MedleyClass = (string)array[rowIndex, 3];
                sankagetuMatomeBunkaieLicenseFromFile.MedleyBranchNo = (string)array[rowIndex, 4];
                sankagetuMatomeBunkaieLicenseFromFile.CorrectCode = (string)array[rowIndex, 5];
                sankagetuMatomeBunkaieLicenseFromFile.JASRACCode = (string)array[rowIndex, 6];
                sankagetuMatomeBunkaieLicenseFromFile.OriginalTitle = (string)array[rowIndex, 7];
                sankagetuMatomeBunkaieLicenseFromFile.Subtitle = (string)array[rowIndex, 8];
                sankagetuMatomeBunkaieLicenseFromFile.SongWriterName = (string)array[rowIndex, 9];
                sankagetuMatomeBunkaieLicenseFromFile.WordTranslatorName = (string)array[rowIndex, 10];
                sankagetuMatomeBunkaieLicenseFromFile.ComposerName = (string)array[rowIndex, 11];
                sankagetuMatomeBunkaieLicenseFromFile.ArrangerName = (string)array[rowIndex, 12];
                sankagetuMatomeBunkaieLicenseFromFile.ArtistName = (string)array[rowIndex, 13];
                sankagetuMatomeBunkaieLicenseFromFile.InfoCharge = (string)array[rowIndex, 14];
                sankagetuMatomeBunkaieLicenseFromFile.IVTClass = (string)array[rowIndex, 15];
                sankagetuMatomeBunkaieLicenseFromFile.LyricClass = (string)array[rowIndex, 16];
                sankagetuMatomeBunkaieLicenseFromFile.ILClass = (string)array[rowIndex, 17];
                sankagetuMatomeBunkaieLicenseFromFile.RequestCount = (string)array[rowIndex, 18];
                sankagetuMatomeBunkaieLicenseFromFile.CAName = (string)array[rowIndex, 19];
                sankagetuMatomeBunkaieLicenseFromFile.ProductNo = (string)array[rowIndex, 20];
                list.Add(sankagetuMatomeBunkaieLicenseFromFile);
            }
            return list;
        }

        /// <summary>
        /// 旧方法
        /// 新方法参见ReflectionTest中的类方法,通过类的属性个数确定列数
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private int GetColumnByFileType(string path)
        {
            FileInfo file = new FileInfo(path);
            string fileExtension = Path.GetExtension(file.FullName);
            if (fileExtension.Equals(".dat"))
            {
                return 20;
            }
            else if (fileExtension.Equals(".txt"))
            {
                return 21;
            }
            return -1;
        }
        /// <summary>
        /// 旧方法
        /// </summary>
        /// <returns></returns>
        public object[,] Read(/*string filepath,string type,int column*/)
        {
            string path = "..\\..\\Data\\doc\\ID13531-201206.txt";

            Stopwatch watch = Stopwatch.StartNew();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string line;
            int count = File.ReadAllLines(path).Length;
            int column = GetColumnByFileType(path); ;
            object[,] datetable = new object[count, column];
            int index = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] array = line.Split('\t');
                for (int i = 0; i < column; ++i)
                {
                    datetable[index, i] = array[i];
                }
                ++index;
            }
            watch.Stop();
            Console.WriteLine(datetable.Length + "\ncost time:" + watch.ElapsedMilliseconds);

            return datetable;
        }
    }
}
