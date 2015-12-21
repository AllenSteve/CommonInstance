using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace EBS.Interface.ESignature.Method
{
    public class ZipUtility
    {
        /// <summary>
        /// 用zip压缩文件（单个文件）
        /// </summary>
        /// <param name="FileName">压缩后文件名</param>
        /// <param name="inputByets">被压缩的文件流</param>
        /// <returns>压缩后的文件流</returns>
        public static MemoryStream ZipFile(string FileName, byte[] inputByets)
        {
            MemoryStream fileStreamOut = new MemoryStream();
            ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut);
            ZipEntry entry = new ZipEntry(Path.GetFileName(FileName));
            entry.CompressionMethod = CompressionMethod.Deflated;
            zipOutStream.PutNextEntry(entry);
            zipOutStream.Write(inputByets, 0, inputByets.Length);

            zipOutStream.Close();
            fileStreamOut.Close();
            return fileStreamOut;
        }

        /// <summary>
        /// 解压zip文件
        /// </summary>
        /// <param name="stream">本解压的文件流</param>
        /// <returns>解压后的文件流</returns>
        public static byte[] UnZipFile(Stream stream)
        {
            using (ZipInputStream inputZipStream = new ZipInputStream(stream))
            {
                ZipEntry theEntry = inputZipStream.GetNextEntry();
                using (MemoryStream outZipStream = new MemoryStream())
                {
                    inputZipStream.CopyTo(outZipStream);
                    return outZipStream.ToArray();
                }
            }
        }
    }
}