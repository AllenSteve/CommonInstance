// ***********************************************************************
// Assembly         : EBS.Common
// Author           : 仇士龙
// Created          : 12-17-2015
//
// Last Modified By : 仇士龙
// Last Modified On : 12-17-2015
// ***********************************************************************
// <copyright file="AdvancedEncryption.cs" company="家居集团无线技术部">
//     Copyright (c) Fang.com. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace BaseFunction.Service.EbsService
{
    public class AdvancedEncryption
    {
        private static string _key;

        /// <summary>  
        /// 获取密钥  
        /// 例如：@")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M"
        /// </summary>  
        private static string Key
        {
            get 
            {
                if (_key == null)
                {
                    _key = System.Configuration.ConfigurationManager.AppSettings["AdvancedEncryption_KEY"];
                }
                return _key;
            }
        }

        private static string _iv;

        /// <summary>  
        /// 获取向量  
        /// 例如：@"L+\~f4,Ir)b$=pkf"
        /// </summary>  
        private static string IV
        {
            get
            {
                if (_iv == null)
                {
                    _iv = System.Configuration.ConfigurationManager.AppSettings["AdvancedEncryption_IV"];
                }
                return _iv;
            }
        }

        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="plainStr">明文字符串</param>  
        /// <returns>密文</returns>  
        public static string AESEncrypt(string plainStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = null;
            Rijndael aes = Rijndael.Create();
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(mStream.ToArray());
                }
            }
            aes.Clear();
            return encrypt;
        }

        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="plainStr">明文字符串</param>  
        /// <param name="returnNull">加密失败时是否返回 null，false 返回 String.Empty</param>  
        /// <returns>密文</returns>  
        public static string AESEncrypt(string plainStr, bool returnNull)
        {
            string encrypt = AESEncrypt(plainStr);
            return returnNull ? encrypt : (encrypt == null ? String.Empty : encrypt);
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="encryptStr">密文字符串</param>  
        /// <returns>明文</returns>  
        public static string AESDecrypt(string encryptStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Convert.FromBase64String(encryptStr);

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            aes.Clear();
            return decrypt;
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="encryptStr">密文字符串</param>  
        /// <param name="returnNull">解密失败时是否返回 null，false 返回 String.Empty</param>  
        /// <returns>明文</returns>  
        public static string AESDecrypt(string encryptStr, bool returnNull)
        {
            string decrypt = AESDecrypt(encryptStr);
            return returnNull ? decrypt : (decrypt == null ? String.Empty : decrypt);
        }
        
    }
}
