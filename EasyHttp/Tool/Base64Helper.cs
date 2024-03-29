﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Tool
{
    /// <summary>
    /// Base64帮助类
    /// </summary>
    public static class Base64Helper
    {
        /// <summary>
        /// 将字符串编码为base64
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string EncodeBase64(string source, Encoding encoding)
        {
            return EncodeBase64In(source, encoding);
        }

        /// <summary>
        /// 将base64转为字符串
        /// </summary>
        /// <param name="base64">字符编码</param>
        /// <param name="encoding">原始字符串</param>
        /// <returns></returns>
        public static string DecodeBase64(string base64, Encoding encoding)
        {
            return DecodeBase64In(base64, encoding);
        }

        #region 将字符串编码为base64
        /// <summary>
        /// 将字符串编码为base64
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        internal static string EncodeBase64In(string source, Encoding encoding)
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

        internal static string DecodeBase64In(string base64,Encoding encoding)
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
