using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Def
{
    /// <summary>
    /// 请求方法(常用)
    /// </summary>
    public class MethodDef
    {
        /// <summary>
        /// 向特定的资源发出请求。
        /// </summary>
        public const string GET = "GET";
        /// <summary>
        /// 向指定资源提交数据进行处理请求（例如提交表单或者上传文件）。数据被包含在请求体中。POST请求可能会导致新的资源的创建和/或已有资源的修改。 
        /// </summary>
        public const string POST = "POST";
        /// <summary>
        /// 向指定资源位置上传其最新内容。
        /// </summary>
        public const string PUT = "PUT";
        /// <summary>
        /// 与PUT方法类似，但PATCH方法通常应用于局部更新。
        /// </summary>
        public const string PATCH = "PATCH";
        /// <summary>
        /// 请求服务器删除Request-URI所标识的资源。 
        /// </summary>
        public const string DELETE = "DELETE";
    }
}
