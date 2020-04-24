using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Def
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    internal class RegexDef
    {
        /// <summary>
        /// 提取编码
        /// </summary>
        internal static readonly string Enconding = "<meta[^<]*charset=([^<]*)[\"']";
    }
}
