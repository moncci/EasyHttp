using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Tool
{
    /// <summary>
    /// 日志对象
    /// </summary>
    public class EhLog
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lockHelper = new object();

        private static string GetLogDir()
        {
            string dirPath = EhTool.GetRootMapPath("logfile.easyhttp." + EhBusinessStatic.BusinessName);
            return dirPath;
        }

        /// <summary>
        /// 法描述：写错误日志。
        /// </summary>
        /// <param name="strContent"></param>
        private static void WriteErrorLogTxt(string strContent)
        {
            lock (EhLog._lockHelper)
            {
                string text = GetLogDir();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, "error");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string str = DateTime.Now.ToString();
                StreamWriter streamWriter = new StreamWriter(text, true, Encoding.UTF8);
                streamWriter.WriteLine("【ERROR】发生时间:" + str);
                streamWriter.WriteLine("发生事件:" + strContent);
                streamWriter.WriteLine("");
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 法描述：写Debug日志。
        /// </summary>
        /// <param name="strContent"></param>
        private static void WriteDebugLogTxt(string strContent)
        {
            lock (EhLog._lockHelper)
            {
                string text = GetLogDir();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, "debug");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string str = DateTime.Now.ToString();
                StreamWriter streamWriter = new StreamWriter(text, true, Encoding.UTF8);
                streamWriter.WriteLine("【DEBUG】发生时间:" + str);
                streamWriter.WriteLine("发生事件:" + strContent);
                streamWriter.WriteLine("");
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 法描述：写Warning日志。
        /// </summary>
        /// <param name="strContent"></param>
        private static void WriteWarningLogTxt(string strContent)
        {
            lock (EhLog._lockHelper)
            {
                string text = GetLogDir();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, "warning");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string str = DateTime.Now.ToString();
                StreamWriter streamWriter = new StreamWriter(text, true, Encoding.UTF8);
                streamWriter.WriteLine("【WARNING】发生时间:" + str);
                streamWriter.WriteLine("发生事件:" + strContent);
                streamWriter.WriteLine("");
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 法描述：写Info日志。
        /// </summary>
        /// <param name="strContent"></param>
        private static void WriteInfoLogTxt(string strContent)
        {
            lock (EhLog._lockHelper)
            {
                string text = GetLogDir();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, "info");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string str = DateTime.Now.ToString();
                StreamWriter streamWriter = new StreamWriter(text, true, Encoding.UTF8);
                streamWriter.WriteLine("【INFO】发生时间:" + str);
                streamWriter.WriteLine("发生事件:" + strContent);
                streamWriter.WriteLine("");
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 法描述：写Info日志。
        /// </summary>
        /// <param name="strContent"></param>
        private static void WriteExceptionLogTxt(string strContent)
        {
            lock (EhLog._lockHelper)
            {
                string text = GetLogDir();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, "exception");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string str = DateTime.Now.ToString();
                StreamWriter streamWriter = new StreamWriter(text, true, Encoding.UTF8);
                streamWriter.WriteLine("【EXCEPTION】发生时间:" + str);
                streamWriter.WriteLine("发生事件:" + strContent);
                streamWriter.WriteLine("");
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 法描述：写Info日志。
        /// </summary>
        /// <param name="strContent"></param>
        private static void WriteRefreshRunLogTxt(string strContent)
        {
            lock (EhLog._lockHelper)
            {
                string text = GetLogDir();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, "refreshrun");
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                text = Path.Combine(text, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                string dtstr = DateTime.Now.ToString("HH:mm:ss");
                StreamWriter streamWriter = new StreamWriter(text, true, Encoding.UTF8);
                streamWriter.WriteLine(dtstr + " " + strContent);
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="strContent"></param>
        public static void WriteDebugLog(string strContent)
        {
            WriteDebugLogTxt(strContent);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="strContent"></param>
        public static void WriteErrorLog(string strContent)
        {
            WriteErrorLogTxt(strContent);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="strContent"></param>
        public static void WriteWarningLog(string strContent)
        {
            WriteWarningLogTxt(strContent);
        }

        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="strContent"></param>
        public static void WriteInfoLog(string strContent)
        {
            WriteInfoLogTxt(strContent);
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteExceptionLog(Exception ex)
        {
            string value = "错误信息:" + ex.Message + "\r\n" + "堆栈信息:" + ex.StackTrace;
            WriteExceptionLogTxt(value);
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteExceptionLog(Exception ex, string errinfo)
        {
            string value = errinfo + " 错误信息:" + ex.Message + "\r\n" + "堆栈信息:" + ex.StackTrace;
            WriteExceptionLogTxt(value);
        }


        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteRefreshRunLog(string info)
        {
            string value = info;
            WriteRefreshRunLogTxt(value);
        }
    }
}
