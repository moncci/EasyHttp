using EasyHttp.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Model
{
    /// <summary>
    /// A标签
    /// </summary>
    public class TagA : TagBase
    {
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 图片（文本标签为空）
        /// </summary>
        public TagImg Img { get; set; }
        /// <summary>
        /// 标签类型
        /// </summary>
        public AType Type { get; set; }
    }
}
