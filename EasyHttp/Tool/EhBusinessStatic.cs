using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Tool
{
    /// <summary>
    /// 业务静态类
    /// </summary>
    public static class EhBusinessStatic
    {
        private static string _businessName = "EasyHttp";
        /// <summary>
        /// 实现业务的名称
        /// </summary>
        internal static string BusinessName { get { return _businessName.ToLower(); } }

        /// <summary>
        /// 设置实现业务的名称
        /// </summary>
        /// <param name="businessName">业务的名称</param>
        public static void SetBusinessName(string businessName)
        {
            _businessName = businessName;
        }
    }
}
