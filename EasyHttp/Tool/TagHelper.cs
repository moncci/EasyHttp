using EasyHttp.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyHttp.Tool
{
    /// <summary>
    /// 标签帮助类
    /// </summary>
    public static class TagHelper
    {
        #region 根据多属性获取标签
        /// <summary>
        /// 根据多属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="attrList">属性链表</param>
        /// <returns></returns>
        public static List<T> GetTagListByAttrs<T>(this string html, List<TagAttr> attrList) where T : TagBase, new()
        {
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据多属性获取标签
        /// <summary>
        /// 根据多属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="attrList"></param>
        /// <returns></returns>
        public static List<T> GetTagListByAttrs<T>(this string html, TagType tagName, List<TagAttr> attrList) where T : TagBase, new()
        {
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据多属性获取标签
        /// <summary>
        /// 根据多属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="attrList">属性链表</param>
        /// <returns></returns>
        public static T GetTagByAttrs<T>(this string html, List<TagAttr> attrList) where T : TagBase, new()
        {
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据多属性获取标签
        /// <summary>
        /// 根据多属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="attrList">属性列表</param>
        /// <returns></returns>
        public static T GetTagByAttrs<T>(this string html, TagType tagName, List<TagAttr> attrList) where T : TagBase, new()
        {
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据属性获取标签
        /// <summary>
        /// 根据属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public static List<T> GetTagListByAttr<T>(this string html, string attrName, string attrValue) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = attrName, Value = attrValue });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据属性获取标签
        /// <summary>
        /// 根据属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public static List<T> GetTagListByAttr<T>(this string html, TagType tagName, string attrName, string attrValue) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = attrName, Value = attrValue });
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据属性获取标签
        /// <summary>
        /// 根据属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public static T GetTagByAttr<T>(this string html, string attrName, string attrValue) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = attrName, Value = attrValue });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据属性获取标签
        /// <summary>
        /// 根据属性获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public static T GetTagByAttr<T>(this string html, TagType tagName, string attrName, string attrValue) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = attrName, Value = attrValue });
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据属性name获取标签
        /// <summary>
        /// 根据属性name获取标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static List<T> GetTagListByAttrName<T>(this string html, string name) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "name", Value = name });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据属性name获取标签
        /// <summary>
        /// 根据属性name获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<T> GetTagListByAttrName<T>(this string html, TagType tagName, string name) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "name", Value = name });
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据属性name获取标签
        /// <summary>
        /// 根据属性name获取标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static T GetTagByAttrName<T>(this string html, string name) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "name", Value = name });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据属性name获取标签
        /// <summary>
        /// 根据属性name获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetTagByAttrName<T>(this string html, TagType tagName, string name) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "name", Value = name });
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据ID获取标签
        /// <summary>
        /// 根据ID获取标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static List<T> GetTagListById<T>(this string html, string id) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "id", Value = id });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据ID获取标签
        /// <summary>
        /// 根据ID获取标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static T GetTagById<T>(this string html, string id) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "id", Value = id });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据Class获取标签
        /// <summary>
        /// 根据Class获取标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="className">className</param>
        /// <returns></returns>
        public static List<T> GetTagListByClass<T>(this string html, string className) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "class", Value = className });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据Class获取标签
        /// <summary>
        /// 根据Class获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static List<T> GetTagListByClass<T>(this string html, TagType tagName, string className) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "class", Value = className });
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            return selectElementList;
        }
        #endregion

        #region 根据Class获取标签
        /// <summary>
        /// 根据Class获取标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="className">className</param>
        /// <returns></returns>
        public static T GetTagByClass<T>(this string html, string className) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "class", Value = className });
            List<T> selectElementList = GetTagList<T>(html, TagType.none, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 根据Class获取标签
        /// <summary>
        /// 根据Class获取标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static T GetTagByClass<T>(this string html, TagType tagName, string className) where T : TagBase, new()
        {
            List<TagAttr> attrList = new List<TagAttr>();
            attrList.Add(new TagAttr() { Name = "class", Value = className });
            List<T> selectElementList = GetTagList<T>(html, tagName, attrList);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 直接根据标签名获取标签（不是属性name，是标签名称）
        /// <summary>
        /// 直接根据标签名获取标签（不是属性name，是标签名称）
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="tagName">标签名称</param>
        /// <returns></returns>
        public static List<T> GetTagListByTagName<T>(this string html, TagType tagName) where T : TagBase, new()
        {
            List<T> selectElementList = GetTagList<T>(html, tagName, null);
            return selectElementList;
        }
        #endregion

        #region 直接根据标签名获取标签（不是属性name，是标签名称）
        /// <summary>
        /// 直接根据标签名获取标签（不是属性name，是标签名称）
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="tagName">标签名称</param>
        /// <returns></returns>
        public static T GetTagByTagName<T>(this string html, TagType tagName) where T : TagBase, new()
        {
            List<T> selectElementList = GetTagList<T>(html, tagName, null);
            if (selectElementList != null && selectElementList.Count > 0)
            {
                return selectElementList[0];
            }
            return null;
        }
        #endregion

        #region 获取所有的图片标签
        /// <summary>
        /// 获取所有的图片标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        //public static List<TagImg> GetAllTagImages(this string html)
        //{
        //    List<TagImg> selectElementList = GetTagList<TagImg>(html, TagType.img, null);
        //    return selectElementList;
        //}
        #endregion

        #region 获取标签
        /// <summary>
        /// 获取标签
        /// </summary>
        /// <typeparam name="T">标签对象</typeparam>
        /// <param name="html">html</param>
        /// <param name="tagType">标签类型</param>
        /// <param name="attrList">属性链表</param>
        /// <returns></returns>
        private static List<T> GetTagList<T>(string html, TagType tagType, List<TagAttr> attrList) where T : TagBase,new()
        {
            List<HtmlNode> selectNodeList = new List<HtmlNode>();
            List<T> selectElementList = null;
            if (!string.IsNullOrWhiteSpace(html))
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                if (htmlDoc == null)
                {
                    return null;
                }

                string tagName = GetTagNameString(tagType);
                HtmlNode[] list = null;
                if (string.IsNullOrEmpty(tagName))
                {
                    list = htmlDoc.DocumentNode.Descendants().ToArray();
                }
                else
                {
                    list = htmlDoc.DocumentNode.Descendants(tagName).ToArray();
                }

                if (list != null && list.Length > 0)
                {
                    foreach (HtmlNode node in list)
                    {
                        bool isCheck = true;
                        if (attrList != null && attrList.Count > 0)
                        {
                            foreach (TagAttr attr in attrList)
                            {
                                if (!string.IsNullOrEmpty(attr.Value))
                                {
                                    string attrValue = node.GetAttributeValue(attr.Name, "");
                                    if (attr.Name == "class")
                                    {
                                        string newClass = attrValue.Replace(" ", ",");
                                        string[] arrayClass = newClass.Split(',');
                                        if (!arrayClass.Contains(attr.Value))
                                        {
                                            isCheck = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (attrValue == attr.Value)
                                        {

                                        }
                                        else
                                        {
                                            isCheck = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (isCheck)
                        {
                            if (node.Name != "#text")
                            {
                                selectNodeList.Add(node);
                            }
                        }
                    }
                }

                if (selectNodeList != null && selectNodeList.Count > 0)
                {
                    selectElementList = new List<T>();
                    foreach (HtmlNode node in selectNodeList)
                    {
                        T tag = new T();
                        tag.IniNode(node);
                        selectElementList.Add(tag);
                    }
                }
            }

            return selectElementList;
        }
        #endregion

        #region 根据标签的类型获取标签的名称
        /// <summary>
        /// 根据标签的类型获取标签的名称
        /// </summary>
        /// <param name="tagType">标签类型</param>
        /// <returns></returns>
        private static string GetTagNameString(TagType tagType)
        {
            //标签的名称
            string name = "";
            switch (tagType)
            {
                case TagType.html:
                    name = "html";
                    break;
                case TagType.head:
                    name = "head";
                    break;
                case TagType.body:
                    name = "body";
                    break;
                case TagType.a:
                    name = "a";
                    break;
                case TagType.b:
                    name = "b";
                    break;
                case TagType.br:
                    name = "br";
                    break;
                case TagType.button:
                    name = "button";
                    break;
                case TagType.canvas:
                    name = "canvas";
                    break;
                case TagType.div:
                    name = "div";
                    break;
                case TagType.font:
                    name = "font";
                    break;
                case TagType.footer:
                    name = "footer";
                    break;
                case TagType.form:
                    name = "form";
                    break;
                case TagType.frame:
                    name = "frame";
                    break;
                case TagType.h1:
                    name = "h1";
                    break;
                case TagType.h2:
                    name = "h2";
                    break;
                case TagType.h3:
                    name = "h3";
                    break;
                case TagType.h4:
                    name = "h4";
                    break;
                case TagType.h5:
                    name = "h5";
                    break;
                case TagType.h6:
                    name = "h6";
                    break;
                case TagType.header:
                    name = "header";
                    break;
                case TagType.hr:
                    name = "hr";
                    break;
                case TagType.iframe:
                    name = "iframe";
                    break;
                case TagType.img:
                    name = "img";
                    break;
                case TagType.input:
                    name = "input";
                    break;
                case TagType.label:
                    name = "label";
                    break;
                case TagType.li:
                    name = "li";
                    break;
                case TagType.link:
                    name = "link";
                    break;
                case TagType.nav:
                    name = "nav";
                    break;
                case TagType.p:
                    name = "p";
                    break;
                case TagType.script:
                    name = "script";
                    break;
                case TagType.select:
                    name = "select";
                    break;
                case TagType.span:
                    name = "span";
                    break;
                case TagType.style:
                    name = "style";
                    break;
                case TagType.table:
                    name = "table";
                    break;
                case TagType.tbody:
                    name = "tbody";
                    break;
                case TagType.td:
                    name = "td";
                    break;
                case TagType.textarea:
                    name = "textarea";
                    break;
                case TagType.tfoot:
                    name = "tfoot";
                    break;
                case TagType.th:
                    name = "th";
                    break;
                case TagType.thead:
                    name = "thead";
                    break;
                case TagType.title:
                    name = "title";
                    break;
                case TagType.tr:
                    name = "tr";
                    break;
                case TagType.ul:
                    name = "ul";
                    break;
                case TagType.none:
                    name = "";
                    break;
                default:
                    name = "";
                    break;
            }

            return name;
        }
        #endregion
    }
}
