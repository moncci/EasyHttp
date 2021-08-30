using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Model
{
    /// <summary>
    /// 域名对象
    /// </summary>
    public class DemainModel
    {
        /// <summary>
        /// http / https
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string Demain { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 获取url
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            string url = Title + "://" + Demain;
            if (Title == "http")
            {
                if (Port != 80)
                {
                    url += ":" + Port;
                }
            }
            else if (Title == "https")
            {
                if (Port != 443)
                {
                    url += ":" + Port;
                }
            }
            return url;
        }
    }
}
