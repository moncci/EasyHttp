using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Tool
{
    /// <summary>
    /// cookie缓存
    /// </summary>
    public class CookieCache
    {
        private object _lockObj = new Object();

        private static CookieCache _instance = null;
        public static CookieCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CookieCache();
                }
                return _instance;
            }
        }

        private CookieCache()
        {

        }


        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="demainUrl">现勘系统登录地址域</param>
        /// <param name="userNo">登录用户名</param>
        public void SetCookie(string cookie, string demainUrl, string userNo)
        {
            Console.WriteLine("设置Cookie:" + cookie);
            if (!string.IsNullOrWhiteSpace(cookie))
            {
                lock (_lockObj)
                {
                    string filePath = GetFileName(demainUrl, userNo);
                    Console.WriteLine("setCache:" + filePath);
                    if (File.Exists(filePath))
                    {
                        ClearText(filePath);
                    }
                    using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(cookie);
                    }
                }
            }
        }
        /// <summary>
        /// 清空文本
        /// </summary>
        /// <param name="path"></param>
        private void ClearText(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
            }
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <returns></returns>
        public string GetCookie(string demainUrl, string userNo)
        {
            lock (_lockObj)
            {
                string filePath = GetFileName(demainUrl, userNo);
                //Console.WriteLine("getCache:" + filePath);
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="demainUrl">域名</param>
        /// <param name="userNo">用户名</param>
        public void DeleteCookie(string demainUrl, string userNo)
        {
            lock (_lockObj)
            {
                string filePath = GetFileName(demainUrl, userNo);
                //Console.WriteLine("getCache:" + filePath);
                if (File.Exists(filePath))
                {
                    EhLog.WriteDebugLog($"删除Cookie:{demainUrl},{userNo}");
                    File.Delete(filePath);
                }
            }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        /// <param name="demainUrl">url域</param>
        /// <param name="userNo">用户名</param>
        /// <returns></returns>
        private string GetFileName(string demainUrl, string userNo)
        {
            string dirPath = EhTool.GetRootMapPath("cookies");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName = EhBusinessStatic.BusinessName + "_" + demainUrl + "_" + userNo + ".txt";
            fileName = fileName.Replace(":", "").Replace("/", "");
            string filePath = Path.Combine(dirPath, fileName);
            return filePath;
        }
    }
}
