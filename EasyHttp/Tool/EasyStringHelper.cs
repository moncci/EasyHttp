using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Tool
{
    public static class EasyStringHelper
    {
        /// <summary>
        /// 去除空格
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static string EasyTrimExIn(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            else
            {
                string temp = text.Replace("\r", "");
                temp = temp.Replace("\n", "");
                temp = temp.Replace("\t", "");

                temp = temp.Trim();
                return temp;
            }
        }

        /// <summary>
        /// 去除空格
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EasyTrimEx(this string text)
        {
            return EasyTrimExIn(text);
        }
    }
}
