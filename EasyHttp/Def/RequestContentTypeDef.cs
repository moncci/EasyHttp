using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Def
{
    /// <summary>
    /// 默认请求的ContentType
    /// </summary>
    public class RequestContentTypeDef
    {
        /// <summary>
        /// ajax之form请求
        /// 最常见的POST提交数据方式
        /// 是最常用的一种请求编码方式，支持GET/POST等方法，所有数据变成键值对的形式 key1=value1&key2=value2
        /// 的形式，并且特殊字符需要转义成utf-8编号，如空格会变成 %20
        /// </summary>
        public const string FORM = "application/x-www-form-urlencoded";
        /// <summary>
        /// ajax之form上传
        /// 使用表单上传文件时，必须指定表单的 enctype属性值为 multipart/form-data. 请求体被分割成多部分，每部分使用 --boundary分割
        /// </summary>
        public const string FORM_DATA = "multipart/form-data";
        /// <summary>
        /// ajax之json
        /// 通过json的形式将数据发送给服务器
        /// </summary>
        public const string JSON = "application/json";
        /// <summary>
        /// ajax之xml
        /// </summary>
        public const string XML = "application/xml";

        /// <summary>
        /// 常见的页面资源类型(请求html页面)
        /// </summary>
        public const string TEXT_HTML = "text/html";
        /// <summary>
        /// 常见的页面资源类型(请求纯文本)
        /// </summary>
        public const string TEXT_PLAIN = "text/plain";
        /// <summary>
        /// 常见的页面资源类型(请求CSS)
        /// </summary>
        public const string TEXT_CSS = "text/css";
        /// <summary>
        /// 常见的页面资源类型(请求javascript)
        /// </summary>
        public const string TEXT_JAVASCRIPT = "text/javascript";
        /// <summary>
        /// 常见的页面资源类型(请求jpeg)
        /// </summary>
        public const string IMAGE_JPEG = "image/jpeg";
        /// <summary>
        /// 常见的页面资源类型(请求png)
        /// </summary>
        public const string IMAGE_PNG = "image/png";
        /// <summary>
        /// 常见的页面资源类型(请求gif)
        /// </summary>
        public const string IMAGE_GIF = "image/gif";
    }
}
