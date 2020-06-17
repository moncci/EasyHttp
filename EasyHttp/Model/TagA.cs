using EasyHttp.Enum;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyHttp.Model
{
    /// <summary>
    /// A标签
    /// </summary>
    public class TagA : TagBase
    {
        public TagA() 
        {
        }

        public TagA(HtmlNode node) : base(node)
        {
        }

        public TagA(string html) : base(html)
        {
        }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string Href
        {
            get
            {
                return GetAttrByName("href");
            }
        }
        /// <summary>
        /// 文本
        /// </summary>
        public string Text {
            get
            {
                HtmlNode[] listImg = Node.Descendants("img").ToArray();
                if (listImg != null && listImg.Length > 0)
                {
                    return string.Empty;
                }
                else
                {
                    return Node.InnerText;
                }
            }
        }
        /// <summary>
        /// 图片（文本标签为空）
        /// </summary>
        public TagImg Img
        {
            get
            {
                HtmlNode[] listImg = Node.Descendants("img").ToArray();
                if (listImg != null && listImg.Length > 0)
                {
                    TagImg img = new TagImg(listImg[0]);

                    return img;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 标签类型
        /// </summary>
        public AType Type {
            get
            {
                HtmlNode[] listImg = Node.Descendants("img").ToArray();
                if (listImg != null && listImg.Length > 0)
                {
                    return AType.Img;
                }
                else
                {
                    return AType.Text;
                }
            }
        }
    }
}
