using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EasyHttp.Json
{
    /// <summary>
    /// 保持和原属性名称大小写相同的名称
    /// </summary>
    public class KeepJsonNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// 转换名称 直接返回原名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string ConvertName(string name)
        {
            return name;
        }
    }
}
