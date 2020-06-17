using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Tool
{
    public static class EasyStringHelper
    {
        public static string EasyTrimEx(this string text)
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
    }
}
