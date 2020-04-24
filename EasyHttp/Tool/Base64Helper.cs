using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Tool
{
    /// <summary>
    /// Base64帮助类
    /// </summary>
    public static class Base64Helper
    {
        #region 将字符串编码为base64
        /// <summary>
        /// 将字符串编码为base64
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string EncodeBase64(string source, Encoding encoding)
        {
            string encode = "";
            byte[] bytes = encoding.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return encode;
        }
        #endregion

        public static string DecodeBase64(string base64,Encoding encoding)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(base64);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return decode;
        }
    }
}
