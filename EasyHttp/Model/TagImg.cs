using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Model
{
    /// <summary>
    /// Img标签
    /// </summary>
    public class TagImg : TagBase
    {
        public TagImg()
        {
        }
        public TagImg(HtmlNode node) : base(node)
        {
        }

        public TagImg(string html) : base(html)
        {
        }

        /// <summary>
        /// 图片url
        /// </summary>
        public string Src
        {
            get
            {
                return GetAttrByName("src"); 
            }
        }
    }
}
