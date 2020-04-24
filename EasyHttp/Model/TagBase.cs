using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Model
{
    /// <summary>
    /// 标签基类
    /// </summary>
    public class TagBase
    {
        /// <summary>
        /// html
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// 当前元素
        /// </summary>
        public HtmlNode Node { get; set; }
    }
}
