using EasyHttp.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EasyHttp
{
    public static class EasyHtmlHelper
    {
        #region 移除注释
        /// <summary>
        /// 移除注释
        /// </summary>
        /// <param name="html">原始html</param>
        /// <returns>新的html</returns>
        public static string RemoveComment(this string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (htmlDoc == null)
            {
                return "";
            }
            HtmlNodeCollection obj = htmlDoc.DocumentNode.SelectNodes("//comment()");
            if (obj != null)
            {
                HtmlNode[] list = obj.ToArray();
                if (list != null && list.Length > 0)
                {
                    foreach (HtmlNode comment in list)
                    {
                        comment.Remove();
                    }
                }
            }

            return htmlDoc.DocumentNode.OuterHtml;
        }
        #endregion

        #region 移除样式代码
        /// <summary>
        /// 移除样式代码
        /// </summary>
        /// <param name="html">原始html</param>
        /// <returns>新的html</returns>
        public static string RemoveStyle(this string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (htmlDoc == null)
            {
                return "";
            }
            HtmlNode[] list = htmlDoc.DocumentNode.Descendants("style").ToArray();
            if (list != null && list.Length > 0)
            {
                foreach (var style in list)
                {
                    style.Remove();
                }
            }

            return htmlDoc.DocumentNode.OuterHtml;
        }
        #endregion

        #region 移除脚本代码
        /// <summary>
        /// 移除脚本代码
        /// </summary>
        /// <param name="html">原始html</param>
        /// <returns>新的html</returns>
        public static string RemoveScript(this string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (htmlDoc == null)
            {
                return "";
            }
            HtmlNode[] list = htmlDoc.DocumentNode.Descendants("script").ToArray();
            if (list != null && list.Length > 0)
            {
                foreach (var style in list)
                {
                    style.Remove();
                }
            }

            return htmlDoc.DocumentNode.OuterHtml;
        }
        #endregion

        
    }
}
