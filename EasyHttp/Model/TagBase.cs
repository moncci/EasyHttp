using EasyHttp.Tool;
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
        public string Html { get; private set; }
        /// <summary>
        /// 当前元素
        /// </summary>
        public HtmlNode Node { get; private set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName
        {
            get
            {
                return Node.Name;
            }
        }
        /// <summary>
        /// 标签id
        /// </summary>
        public string Id
        {
            get
            {
                return Node.Id;
            }
        }
        /// <summary>
        /// 标签属性Name
        /// </summary>
        public string Name
        {
            get
            {
                return GetAttrByName("name");
            }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get
            {
                return GetAttrByName("value");
            }
        }

        /// <summary>
        /// 内部文本
        /// </summary>
        public string InnerText
        {
            get
            {
                if (Node != null)
                {
                    string text = Node.InnerText;
                    if (string.IsNullOrEmpty(text))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return text.EasyTrimEx();
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 内部Html
        /// </summary>
        public string InnerHtml
        {
            get
            {
                if (Node != null)
                {
                    string html = Node.InnerHtml;
                    if (string.IsNullOrEmpty(html))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return html.Trim();
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public string GetAttrByName(string name)
        {
            if (Node != null)
            {
                return Node.GetAttributeValue(name, "");
            }
            else
            {
                return "";
            }
        }

        public TagBase()
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node">HtmlNode</param>
        public TagBase(HtmlNode node)
        {
            Node = node;
            Html = node.OuterHtml;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="html">html</param>
        public TagBase(string html)
        {
            IniHtml(html);
        }

        /// <summary>
        /// 初始化node
        /// </summary>
        /// <param name="node"></param>
        internal void IniNode(HtmlNode node)
        {
            Node = node;
            Html = node.OuterHtml;
        }

        /// <summary>
        /// 初始化html
        /// </summary>
        /// <param name="html"></param>
        internal void IniHtml(string html)
        {
            Html = html;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode nodeTemp = htmlDoc.DocumentNode;
            if (nodeTemp.Name == "#document")
            {
                Node = nodeTemp.FirstChild;
            }
            else
            {
                Node = nodeTemp;
            }
        }

        /// <summary>
        /// 获取子标签
        /// </summary>
        /// <returns></returns>
        public List<HtmlNode> GetChildNodes()
        {
            List<HtmlNode> list = new List<HtmlNode>();
            if (Node != null && Node.ChildNodes != null && Node.ChildNodes.Count > 0)
            {
                foreach (HtmlNode item in Node.ChildNodes)
                {
                    if (item.Name != "#text")
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}
