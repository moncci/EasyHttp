using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Tool
{
    /// <summary>
    /// MD5帮助类
    /// </summary>
    public class EhMd5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns>经过加密后的MD5字符</returns>
        public static string GetMd5(string content)
        {
            return GetMd5In(content);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns>经过加密后的MD5字节</returns>
        public static byte[] GetMd5(byte[] content)
        {
            return Encoding.UTF8.GetBytes(EhMd5.GetMd5(Encoding.UTF8.GetString(content)));
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        public static string GetMd5FromFile(string fileName)
        {
            return GetMd5FromFileIn(fileName);
        }

        /// <summary>
        /// 比较内容是否一致
        /// </summary>
        /// <param name="contentA">比较的内容</param>
        /// <param name="contentB">比较的内容</param>
        /// <returns></returns>
        public static bool Compare(string contentA, string contentB)
        {
            return EhMd5.GetMd5(contentA).Trim().ToUpper() == EhMd5.GetMd5(contentB).ToUpper().Trim();
        }

        /// <summary>
        /// 比较内容是否一致
        /// </summary>
        /// <param name="contentA">比较的内容</param>
        /// <param name="contentB">比较的内容</param>
        /// <returns></returns>
        public static bool Compare(byte[] contentA, byte[] contentB)
        {
            return EhMd5.Compare(Encoding.UTF8.GetString(contentA), Encoding.UTF8.GetString(contentB));
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns>经过加密后的MD5字符</returns>
        private static string GetMd5In(string content)
        {
            string text = "";
            System.Security.Cryptography.MD5 mD = System.Security.Cryptography.MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(content));
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i].ToString("x2");
            }
            return text;
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        private static string GetMd5FromFileIn(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5FromFile() fail,error:" + ex.Message);
            }
        }
    }
}
