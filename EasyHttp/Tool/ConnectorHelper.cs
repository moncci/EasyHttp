using EasyHttp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyHttp.Tool
{
    /// <summary>
    /// 接口帮助类
    /// </summary>
    public class ConnectorHelper
    {
        #region 获取域名
        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DemainModel GetDemain(string url)
        {
            string newurl = GetDomainIP(url);
            if (string.IsNullOrWhiteSpace(newurl))
            {
                return null;
            }
            else
            {
                string[] array = newurl.Split(':');
                if (array.Length == 2)
                {
                    DemainModel m = new DemainModel();
                    m.Title = array[0];
                    m.Demain = array[1].Replace("//", "");
                    if (m.Title == "http")
                    {
                        m.Port = 80;
                    }
                    else
                    {
                        m.Port = 443;
                    }
                    return m;
                }
                else if (array.Length == 3)
                {
                    DemainModel m = new DemainModel();
                    m.Title = array[0];
                    m.Demain = array[1].Replace("//", "");
                    m.Port = int.Parse(array[2]);
                    return m;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 获取域名
        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetDomainIP(string url)
        {
            string ipAddress = url.Trim().ToLower();
            if (!ipAddress.StartsWith("http"))
            {
                ipAddress = "http://" + ipAddress;
            }

            string title = "http://";
            if (ipAddress.StartsWith("https"))
            {
                title = "https://";
            }

            //string p = "(http|https)://(?<domain>[^/]*)";
            string p = "(http|https)://(?<domain>[^/]*)";
            Regex reg = new Regex(p, RegexOptions.IgnoreCase);
            Match m = reg.Match(ipAddress);
            try
            {
                string result = m.Groups["domain"].Value;
                return title + result;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
        #endregion

    }
}
