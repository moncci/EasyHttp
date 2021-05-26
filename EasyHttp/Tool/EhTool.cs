using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EasyHttp.Tool
{
    internal class EhTool
    {
        private static Regex RegexBr = new Regex("(\\r\\n)", RegexOptions.IgnoreCase);
        public static Regex RegexFont = new Regex("<font color=\".*?\">([\\s\\S]+?)</font>", GetRegexCompiledOptions());

        /// <summary>
        /// 程序集文件版本
        /// </summary>
        private static FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        private static string TemplateCookieName = string.Format("dnttemplateid_{0}_{1}_{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);
        /// <summary>
        /// 根据阿拉伯数字返回月份的名称(可更改为某种语言)
        /// </summary>	
        public static string[] Monthes
        {
            get
            {
                return new string[]
                {
                    "January",
                    "February",
                    "March",
                    "April",
                    "May",
                    "June",
                    "July",
                    "August",
                    "September",
                    "October",
                    "November",
                    "December"
                };
            }
        }
        /// <summary>
        /// 得到正则编译参数设置
        /// </summary>
        /// <returns>参数设置</returns>
        public static RegexOptions GetRegexCompiledOptions()
        {
            return RegexOptions.None;
        }
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (StrIsNullOrEmpty(stringarray))
            {
                return false;
            }
            str = str.ToLower();
            string[] array = SplitString(stringarray.ToLower(), strsplit);
            for (int i = 0; i < array.Length; i++)
            {
                if (str.IndexOf(array[i]) > -1)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }
        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }
        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }
        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match match = RegexBr.Match(str);
            while (match.Success)
            {
                str = str.Replace(match.Groups[0].ToString(), "");
                match = match.NextMatch();
            }
            return str;
        }
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length *= -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex -= length;
                    }
                }
                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                if (length + startIndex <= 0)
                {
                    return "";
                }
                length += startIndex;
                startIndex = 0;
            }
            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }
            return str.Substring(startIndex, length);
        }
        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }
        /// <summary>
        /// 获得当前绝对路径--从跟节点找即 ~/
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetRootMapPath(string strPath)
        {
            //string rootDir = Directory.GetCurrentDirectory();
            string rootDir = AppContext.BaseDirectory;

            //dynamic type = typeof(JTool);
            //string rootDir = Path.GetDirectoryName(type.Assembly.Location);


            //IFileProvider f = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            //IDirectoryContents content = f.GetDirectoryContents(strPath);
            //foreach (IFileInfo item in content)
            //{

            //}
            string dir = string.Empty;
            if (!string.IsNullOrEmpty(strPath))
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart(new char[]
                    {
                    '\\'
                    });
                }
                dir = Path.Combine(rootDir, strPath);
            }
            else
            {
                dir = rootDir;
            }

            return dir;
            //if (HttpContext.Current != null)
            //{
            //    return HttpContent.Current.Server.MapPath("~/" + strPath);
            //}
            //strPath = strPath.Replace("/", "\\");
            //if (strPath.StartsWith("\\"))
            //{
            //    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart(new char[]
            //    {
            //        '\\'
            //    });
            //}
            //return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        //public static string GetMapPath(string strPath)
        //{
        //    if (HttpContext.Current != null)
        //    {
        //        return HttpContext.Current.Server.MapPath(strPath);
        //    }
        //    strPath = strPath.Replace("/", "\\");
        //    if (strPath.StartsWith("\\"))
        //    {
        //        strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart(new char[]
        //        {
        //            '\\'
        //        });
        //    }
        //    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        //}
        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }
        ///// <summary>
        ///// 以指定的ContentType输出指定文件文件
        ///// </summary>
        ///// <param name="filepath">文件路径</param>
        ///// <param name="filename">输出的文件名</param>
        ///// <param name="filetype">将文件输出时设置的ContentType</param>
        //public static void ResponseFile(string filepath, string filename, string filetype)
        //{
        //    Stream stream = null;
        //    byte[] buffer = new byte[10000];
        //    try
        //    {
        //        stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //        long num = stream.Length;
        //        HttpContext.Current.Response.ContentType = filetype;
        //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.UrlEncode(filename.Trim()).Replace("+", " "));
        //        while (num > 0L)
        //        {
        //            if (HttpContext.Current.Response.IsClientConnected)
        //            {
        //                int num2 = stream.Read(buffer, 0, 10000);
        //                HttpContext.Current.Response.OutputStream.Write(buffer, 0, num2);
        //                HttpContext.Current.Response.Flush();
        //                buffer = new byte[10000];
        //                num -= (long)num2;
        //            }
        //            else
        //            {
        //                num = -1L;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Current.Response.Write("Error : " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //        {
        //            stream.Close();
        //        }
        //    }
        //    HttpContext.Current.Response.End();
        //}
        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string a = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return a == "jpg" || a == "jpeg" || a == "png" || a == "bmp" || a == "gif";
        }
        /// <summary>
        /// int型转换为string型
        /// </summary>
        /// <returns>转换后的string类型结果</returns>
        public static string IntToStr(int intValue)
        {
            return Convert.ToString(intValue);
        }
        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);
            array = new MD5CryptoServiceProvider().ComputeHash(array);
            string text = "";
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i].ToString("x").PadLeft(2, '0');
            }
            return text;
        }
        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SHA256Managed sHA256Managed = new SHA256Managed();
            byte[] inArray = sHA256Managed.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }
        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;
            int byteCount = Encoding.Default.GetByteCount(str);
            int length = str.Length;
            int num = 0;
            int num2 = 0;
            if (byteCount > len)
            {
                for (int i = 0; i < length; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)
                    {
                        num += 2;
                    }
                    else
                    {
                        num++;
                    }
                    if (num > len)
                    {
                        num2 = i;
                        break;
                    }
                    if (num == len)
                    {
                        num2 = i + 1;
                        break;
                    }
                }
                if (num2 >= 0)
                {
                    result = str.Substring(0, num2) + p_TailString;
                }
            }
            else
            {
                result = str;
            }
            return result;
        }
        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string text = p_SrcString;
            byte[] bytes = Encoding.UTF8.GetBytes(p_SrcString);
            char[] chars = Encoding.UTF8.GetChars(bytes);
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if ((c > 'ࠀ' && c < '一') || (c > '가' && c < '힣'))
                {
                    string result;
                    if (p_StartIndex >= p_SrcString.Length)
                    {
                        result = "";
                    }
                    else
                    {
                        result = p_SrcString.Substring(p_StartIndex, (p_Length + p_StartIndex > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                    }
                    return result;
                }
            }
            if (p_Length >= 0)
            {
                byte[] bytes2 = Encoding.Default.GetBytes(p_SrcString);
                if (bytes2.Length > p_StartIndex)
                {
                    int num = bytes2.Length;
                    if (bytes2.Length > p_StartIndex + p_Length)
                    {
                        num = p_Length + p_StartIndex;
                    }
                    else
                    {
                        p_Length = bytes2.Length - p_StartIndex;
                        p_TailString = "";
                    }
                    int num2 = p_Length;
                    int[] array = new int[p_Length];
                    int num3 = 0;
                    for (int j = p_StartIndex; j < num; j++)
                    {
                        if (bytes2[j] > 127)
                        {
                            num3++;
                            if (num3 == 3)
                            {
                                num3 = 1;
                            }
                        }
                        else
                        {
                            num3 = 0;
                        }
                        array[j] = num3;
                    }
                    if (bytes2[num - 1] > 127 && array[p_Length - 1] == 1)
                    {
                        num2 = p_Length + 1;
                    }
                    byte[] array2 = new byte[num2];
                    Array.Copy(bytes2, p_StartIndex, array2, 0, num2);
                    text = Encoding.Default.GetString(array2);
                    text += p_TailString;
                }
            }
            return text;
        }
        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }
        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string GetSpacesString(int spacesCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < spacesCount; i++)
            {
                stringBuilder.Append(" &nbsp;&nbsp;");
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^[\\w\\.]+([-]\\w+)*@[A-Za-z0-9-_]+[\\.][A-Za-z0-9-_]");
        }
        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }
        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, "^(http|https)\\://([a-zA-Z0-9\\.\\-]+(\\:[a-zA-Z0-9\\.&%\\$\\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\\-]+\\.)*[a-zA-Z0-9\\-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\\:[0-9]+)*(/($|[a-zA-Z0-9\\.\\,\\?\\'\\\\\\+&%\\$#\\=~_\\-]+))*$");
        }
        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }
        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, "[A-Za-z0-9\\+\\/\\=]");
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
        }
        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, "^\\s*$|^c:\\\\con\\\\con$|[%,\\*\"\\s\\t\\<\\>\\&]|游客|^Guest");
        }
        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), "[^\\w\\.@-]", "");
        }
        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] array = url.Split(new char[]
            {
                '/'
            });
            return array[array.Length - 1].Split(new char[]
            {
                '?'
            })[0];
        }
        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string result;
            if (str == null)
            {
                result = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                result = str;
            }
            return result;
        }
        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }
            if (datetimestr.Equals(""))
            {
                return replacestr;
            }
            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }
        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays((double)relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            DateTime dateTime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out dateTime))
            {
                return dateTime.ToString(formatStr);
            }
            return "N/A";
        }
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        //public static string GetRealIP()
        //{
        //    return RequestHelper.GetIP();
        //}
        /// <summary>
        /// 改正sql语句中的转义字符
        /// </summary>
        public static string mashSQL(string str)
        {
            if (str != null)
            {
                return str.Replace("'", "'");
            }
            return "";
        }
        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSQL(string str)
        {
            if (str != null)
            {
                return str.Replace("'", "''");
            }
            return "";
        }
        ///// <summary>
        ///// 转换为静态html
        ///// </summary>
        //public void transHtml(string path, string outpath)
        //{
        //    Page page = new Page();
        //    StringWriter stringWriter = new StringWriter();
        //    page.Server.Execute(path, stringWriter);
        //    FileStream fileStream;
        //    if (File.Exists(page.Server.MapPath("") + "\\" + outpath))
        //    {
        //        File.Delete(page.Server.MapPath("") + "\\" + outpath);
        //        fileStream = File.Create(page.Server.MapPath("") + "\\" + outpath);
        //    }
        //    else
        //    {
        //        fileStream = File.Create(page.Server.MapPath("") + "\\" + outpath);
        //    }
        //    byte[] bytes = Encoding.Default.GetBytes(stringWriter.ToString());
        //    fileStream.Write(bytes, 0, bytes.Length);
        //    fileStream.Close();
        //}
        /// <summary>
        /// 转换为简体中文
        /// </summary>
        //public static string ToSChinese(string str)
        //{
        //    return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        //}
        ///// <summary>
        ///// 转换为繁体中文
        ///// </summary>
        //public static string ToTChinese(string str)
        //{
        //    return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        //}
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (StrIsNullOrEmpty(strContent))
            {
                return new string[0];
            }
            if (strContent.IndexOf(strSplit) < 0)
            {
                return new string[]
                {
                    strContent
                };
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] array = new string[count];
            string[] array2 = SplitString(strContent, strSplit);
            for (int i = 0; i < count; i++)
            {
                if (i < array2.Length)
                {
                    array[i] = array2[i];
                }
                else
                {
                    array[i] = string.Empty;
                }
            }
            return array;
        }
        /// <summary>
        /// 过滤字符串数组中每个元素为合适的大小
        /// 当长度小于minLength时，忽略掉,-1为不限制最小长度
        /// 当长度大于maxLength时，取其前maxLength位
        /// 如果数组中有null元素，会被忽略掉
        /// </summary>
        /// <param name="minLength">单个元素最小长度</param>
        /// <param name="maxLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] PadStringArray(string[] strArray, int minLength, int maxLength)
        {
            if (minLength > maxLength)
            {
                int num = maxLength;
                maxLength = minLength;
                minLength = num;
            }
            int num2 = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (minLength > -1 && strArray[i].Length < minLength)
                {
                    strArray[i] = null;
                }
                else
                {
                    if (strArray[i].Length > maxLength)
                    {
                        strArray[i] = strArray[i].Substring(0, maxLength);
                    }
                    num2++;
                }
            }
            string[] array = new string[num2];
            int num3 = 0;
            int num4 = 0;
            while (num3 < strArray.Length && num4 < array.Length)
            {
                if (strArray[num3] != null && strArray[num3] != string.Empty)
                {
                    array[num4] = strArray[num3];
                    num4++;
                }
                num3++;
            }
            return array;
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int maxElementLength)
        {
            string[] array = SplitString(strContent, strSplit);
            if (!ignoreRepeatItem)
            {
                return array;
            }
            return DistinctStringArray(array, maxElementLength);
        }
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int minElementLength, int maxElementLength)
        {
            string[] strArray = SplitString(strContent, strSplit);
            if (ignoreRepeatItem)
            {
                strArray = DistinctStringArray(strArray);
            }
            return PadStringArray(strArray, minElementLength, maxElementLength);
        }
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem)
        {
            return SplitString(strContent, strSplit, ignoreRepeatItem, 0);
        }
        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < strArray.Length; i++)
            {
                string text = strArray[i];
                string text2 = text;
                if (maxElementLength > 0 && text2.Length > maxElementLength)
                {
                    text2 = text2.Substring(0, maxElementLength);
                }
                hashtable[text2.Trim()] = text;
            }
            string[] array = new string[hashtable.Count];
            hashtable.Keys.CopyTo(array, 0);
            return array;
        }
        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }
        /// <summary>
        /// 替换html字符
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }
        /// <summary>
        /// 进行指定的替换(脏字过滤)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            string[] array = SplitString(bantext, "\r\n");
            for (int i = 0; i < array.Length; i++)
            {
                string oldValue = array[i].Substring(0, array[i].IndexOf("="));
                string newValue = array[i].Substring(array[i].IndexOf("=") + 1);
                str = str.Replace(oldValue, newValue);
            }
            return str;
        }
        /// <summary>
        /// 获得伪静态页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            return GetStaticPageNumbers(curPage, countPage, url, expname, extendPage, 0);
        }
        /// <summary>
        /// 获得伪静态页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="forumrewrite">当前版块是否使用URL重写</param>
        /// <returns>页码html</returns>
        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage, int forumrewrite)
        {
            int num = 1;
            string value = string.Concat(new string[]
            {
                "<a href=\"",
                url,
                "-1",
                expname,
                "\">&laquo;</a>"
            });
            string value2 = string.Concat(new object[]
            {
                "<a href=\"",
                url,
                "-",
                countPage,
                expname,
                "\">&raquo;</a>"
            });
            if (forumrewrite == 1)
            {
                value = string.Concat(new string[]
                {
                    "<a href=\"",
                    url,
                    "/1/list",
                    expname,
                    "\">&laquo;</a>"
                });
                value2 = string.Concat(new object[]
                {
                    "<a href=\"",
                    url,
                    "/",
                    countPage,
                    "/list",
                    expname,
                    "\">&raquo;</a>"
                });
            }
            if (forumrewrite == 2)
            {
                value = "<a href=\"" + url + "/\">&laquo;</a>";
                value2 = string.Concat(new object[]
                {
                    "<a href=\"",
                    url,
                    "/",
                    countPage,
                    "/\">&raquo;</a>"
                });
            }
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            int num2;
            if (countPage > extendPage)
            {
                if (curPage - extendPage / 2 > 0)
                {
                    if (curPage + extendPage / 2 < countPage)
                    {
                        num = curPage - extendPage / 2;
                        num2 = num + extendPage - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num = num2 - extendPage + 1;
                        value2 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    value = "";
                }
            }
            else
            {
                num = 1;
                num2 = countPage;
                value = "";
                value2 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(value);
            for (int i = num; i <= num2; i++)
            {
                if (i == curPage)
                {
                    stringBuilder.Append("<span>");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</span>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"");
                    if (forumrewrite == 1)
                    {
                        stringBuilder.Append(url);
                        if (i != 1)
                        {
                            stringBuilder.Append("/");
                            stringBuilder.Append(i);
                        }
                        stringBuilder.Append("/list");
                        stringBuilder.Append(expname);
                    }
                    else
                    {
                        if (forumrewrite == 2)
                        {
                            stringBuilder.Append(url);
                            stringBuilder.Append("/");
                            if (i != 1)
                            {
                                stringBuilder.Append(i);
                                stringBuilder.Append("/");
                            }
                        }
                        else
                        {
                            stringBuilder.Append(url);
                            if (i != 1)
                            {
                                stringBuilder.Append("-");
                                stringBuilder.Append(i);
                            }
                            stringBuilder.Append(expname);
                        }
                    }
                    stringBuilder.Append("\">");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</a>");
                }
            }
            stringBuilder.Append(value2);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 获得帖子的伪静态页码显示链接
        /// </summary>
        /// <param name="expname"></param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPostPageNumbers(int countPage, string url, string expname, int extendPage)
        {
            int num = 1;
            int num2 = 1;
            string value = string.Concat(new string[]
            {
                "<a href=\"",
                url,
                "-1",
                expname,
                "\">&laquo;</a>"
            });
            string value2 = string.Concat(new object[]
            {
                "<a href=\"",
                url,
                "-",
                countPage,
                expname,
                "\">&raquo;</a>"
            });
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            int num3;
            if (countPage > extendPage)
            {
                if (num2 - extendPage / 2 > 0)
                {
                    if (num2 + extendPage / 2 < countPage)
                    {
                        num = num2 - extendPage / 2;
                        num3 = num + extendPage - 1;
                    }
                    else
                    {
                        num3 = countPage;
                        num = num3 - extendPage + 1;
                        value2 = "";
                    }
                }
                else
                {
                    num3 = extendPage;
                    value = "";
                }
            }
            else
            {
                num = 1;
                num3 = countPage;
                value = "";
                value2 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(value);
            for (int i = num; i <= num3; i++)
            {
                stringBuilder.Append("<a href=\"");
                stringBuilder.Append(url);
                stringBuilder.Append("-");
                stringBuilder.Append(i);
                stringBuilder.Append(expname);
                stringBuilder.Append("\">");
                stringBuilder.Append(i);
                stringBuilder.Append("</a>");
            }
            stringBuilder.Append(value2);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, "page");
        }
        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, null);
        }
        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <param name="anchor">锚点</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            if (pagetag == "")
            {
                pagetag = "page";
            }
            int num = 1;
            if (url.IndexOf("?") > 0)
            {
                url += "&";
            }
            else
            {
                url += "?";
            }
            string text = string.Concat(new string[]
            {
                "<a href=\"",
                url,
                "&",
                pagetag,
                "=1"
            });
            string text2 = string.Concat(new object[]
            {
                "<a href=\"",
                url,
                "&",
                pagetag,
                "=",
                countPage
            });
            if (anchor != null)
            {
                text += anchor;
                text2 += anchor;
            }
            text += "\">&laquo;</a>";
            text2 += "\">&raquo;</a>";
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            int num2;
            if (countPage > extendPage)
            {
                if (curPage - extendPage / 2 > 0)
                {
                    if (curPage + extendPage / 2 < countPage)
                    {
                        num = curPage - extendPage / 2;
                        num2 = num + extendPage - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num = num2 - extendPage + 1;
                        text2 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    text = "";
                }
            }
            else
            {
                num = 1;
                num2 = countPage;
                text = "";
                text2 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(text);
            for (int i = num; i <= num2; i++)
            {
                if (i == curPage)
                {
                    stringBuilder.Append("<span>");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</span>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"");
                    stringBuilder.Append(url);
                    stringBuilder.Append(pagetag);
                    stringBuilder.Append("=");
                    stringBuilder.Append(i);
                    if (anchor != null)
                    {
                        stringBuilder.Append(anchor);
                    }
                    stringBuilder.Append("\">");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</a>");
                }
            }
            stringBuilder.Append(text2);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }
        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 返回指定目录下的非 UTF8 字符集文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <returns>文件名的字符串数组</returns>
        public static string[] FindNoUTF8File(string Path)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            FileInfo[] files = directoryInfo.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension.ToLower().Equals(".htm"))
                {
                    FileStream fileStream = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read);
                    bool flag = IsUTF8(fileStream);
                    fileStream.Close();
                    if (!flag)
                    {
                        stringBuilder.Append(files[i].FullName);
                        stringBuilder.Append("\r\n");
                    }
                }
            }
            return SplitString(stringBuilder.ToString(), "\r\n");
        }
        /// <summary>
        /// 判断文件流是否为UTF8字符集
        /// </summary>
        /// <param name="sbInputStream">文件流</param>
        /// <returns>判断结果</returns>
        private static bool IsUTF8(FileStream sbInputStream)
        {
            bool flag = true;
            long length = sbInputStream.Length;
            byte b = 0;
            int num = 0;
            while ((long)num < length)
            {
                byte b2 = (byte)sbInputStream.ReadByte();
                if ((b2 & 128) != 0)
                {
                    flag = false;
                }
                if (b == 0)
                {
                    if (b2 >= 128)
                    {
                        do
                        {
                            b2 = (byte)(b2 << 1);
                            b += 1;
                        }
                        while ((b2 & 128) != 0);
                        b -= 1;
                        if (b == 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if ((b2 & 192) != 128)
                    {
                        return false;
                    }
                    b -= 1;
                }
                num++;
            }
            return b <= 0 && !flag;
        }
        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }
            return bytes.ToString() + "Bytes";
        }
        /// <summary>
        /// 将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int SafeInt32(object objNum)
        {
            if (objNum == null)
            {
                return 0;
            }
            string text = objNum.ToString();
            if (!IsNumeric(text))
            {
                return 0;
            }
            if (text.ToString().Length <= 9)
            {
                return int.Parse(text);
            }
            if (text.StartsWith("-"))
            {
                return -2147483648;
            }
            return 2147483647;
        }
        /// <summary>
        /// 返回相差的秒数
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Time).AddSeconds((double)Sec);
            if (timeSpan.TotalSeconds > 2147483647.0)
            {
                return 2147483647;
            }
            if (timeSpan.TotalSeconds < -2147483648.0)
            {
                return -2147483648;
            }
            return (int)timeSpan.TotalSeconds;
        }
        /// <summary>
        /// 返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (StrIsNullOrEmpty(time))
            {
                return 1;
            }
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse(time).AddMinutes((double)minutes);
            if (timeSpan.TotalMinutes > 2147483647.0)
            {
                return 2147483647;
            }
            if (timeSpan.TotalMinutes < -2147483648.0)
            {
                return -2147483648;
            }
            return (int)timeSpan.TotalMinutes;
        }
        /// <summary>
        /// 返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (StrIsNullOrEmpty(time))
            {
                return 1;
            }
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse(time).AddHours((double)hours);
            if (timeSpan.TotalHours > 2147483647.0)
            {
                return 2147483647;
            }
            if (timeSpan.TotalHours < -2147483648.0)
            {
                return -2147483648;
            }
            return (int)timeSpan.TotalHours;
        }
        /// <summary>
        /// 建立文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public static bool CreateDir(string name)
        //{
        //    return JTool.MakeSureDirectoryPathExists(name);
        //}
        /// <summary>
        /// 为脚本替换特殊字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(string str)
        {
            return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"");
        }
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }
        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){2}((2[0-4]\\d|25[0-5]|[01]?\\d\\d?|\\*)\\.)(2[0-4]\\d|25[0-5]|[01]?\\d\\d?|\\*)$");
        }
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] array = SplitString(ip, ".");
            for (int i = 0; i < iparray.Length; i++)
            {
                string[] array2 = SplitString(iparray[i], ".");
                int num = 0;
                for (int j = 0; j < array2.Length; j++)
                {
                    if (array2[j] == "*")
                    {
                        return true;
                    }
                    if (array.Length <= j || !(array2[j] == array[j]))
                    {
                        break;
                    }
                    num++;
                }
                if (num == 4)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获得Assembly版本号
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyVersion()
        {
            return string.Format("{0}.{1}.{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);
        }
        /// <summary>
        /// 获得Assembly产品名称
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyProductName()
        {
            return AssemblyFileVersion.ProductName;
        }
        /// <summary>
        /// 获得Assembly产品版权
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyCopyright()
        {
            return AssemblyFileVersion.LegalCopyright;
        }
        ///// <summary>
        ///// 创建目录
        ///// </summary>
        ///// <param name="name">名称</param>
        ///// <returns>创建是否成功</returns>
        //[DllImport("dbgHelp", SetLastError = true)]
        //private static extern bool MakeSureDirectoryPathExists(string name);
        ///// <summary>
        ///// 写cookie值
        ///// </summary>
        ///// <param name="strName">名称</param>
        ///// <param name="strValue">值</param>
        //public static void WriteCookie(string strName, string strValue)
        //{
        //    HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
        //    if (httpCookie == null)
        //    {
        //        httpCookie = new HttpCookie(strName);
        //    }
        //    httpCookie.Value = strValue;
        //    HttpContext.Current.Response.AppendCookie(httpCookie);
        //}
        ///// <summary>
        ///// 写cookie值
        ///// </summary>
        ///// <param name="strName">名称</param>
        ///// <param name="strValue">值</param>
        //public static void WriteCookie(string strName, string key, string strValue)
        //{
        //    HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
        //    if (httpCookie == null)
        //    {
        //        httpCookie = new HttpCookie(strName);
        //    }
        //    httpCookie[key] = strValue;
        //    HttpContext.Current.Response.AppendCookie(httpCookie);
        //}
        ///// <summary>
        ///// 写cookie值
        ///// </summary>
        ///// <param name="strName">名称</param>
        ///// <param name="strValue">值</param>
        ///// <param name="strValue">过期时间(分钟)</param>
        //public static void WriteCookie(string strName, string strValue, int expires)
        //{
        //    HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
        //    if (httpCookie == null)
        //    {
        //        httpCookie = new HttpCookie(strName);
        //    }
        //    httpCookie.Value = strValue;
        //    httpCookie.Expires = DateTime.Now.AddMinutes((double)expires);
        //    HttpContext.Current.Response.AppendCookie(httpCookie);
        //}
        ///// <summary>
        ///// 读cookie值
        ///// </summary>
        ///// <param name="strName">名称</param>
        ///// <returns>cookie值</returns>
        //public static string GetCookie(string strName)
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
        //    {
        //        return HttpContext.Current.Request.Cookies[strName].Value.ToString();
        //    }
        //    return "";
        //}
        ///// <summary>
        ///// 读cookie值
        ///// </summary>
        ///// <param name="strName">名称</param>
        ///// <returns>cookie值</returns>
        //public static string GetCookie(string strName, string key)
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
        //    {
        //        return HttpContext.Current.Request.Cookies[strName][key].ToString();
        //    }
        //    return "";
        //}
        ///// <summary>
        ///// 得到论坛的真实路径
        ///// </summary>
        ///// <returns></returns>
        //public static string GetTrueForumPath()
        //{
        //    string text = HttpContext.Current.Request.Path;
        //    if (text.LastIndexOf("/") != text.IndexOf("/"))
        //    {
        //        text = text.Substring(text.IndexOf("/"), text.LastIndexOf("/") + 1);
        //    }
        //    else
        //    {
        //        text = "/";
        //    }
        //    return text;
        //}
        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, "(\\d{4})-(\\d{1,2})-(\\d{1,2})");
        }
        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            return Regex.Replace(content, "<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, "(\\<|\\s+)o([a-z]+\\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "(script|frame|form|meta|behavior|style)([\\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }
        /// <summary>
        /// 将用户组Title中的font标签去掉
        /// </summary>
        /// <param name="title">用户组Title</param>
        /// <returns></returns>
        public static string RemoveFontTag(string title)
        {
            Match match = RegexFont.Match(title);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return title;
        }
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            return Validator.IsNumeric(Expression);
        }
        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string HTML)
        {
            Regex regex = new Regex("</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
            return regex.Replace(HTML, "");
        }
        public static bool IsDouble(object Expression)
        {
            return Validator.IsDouble(Expression);
        }
        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            return EhConvert.ObjectToBool(expression, defValue);
        }
        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            return EhConvert.StrToBool(expression, defValue);
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            return EhConvert.ObjectToInt(expression, defValue);
        }
        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression, int defValue)
        {
            return EhConvert.StrToInt(expression, defValue);
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int? ObjectToNullabelInt(object expression)
        {
            return EhConvert.ObjectToNullabelInt(expression);
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int? StrToNullabelInt(string expression)
        {
            return EhConvert.StrToNullabelInt(expression);
        }
        /// <summary>
        /// Object型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjectToFloat(object strValue, float defValue)
        {
            return EhConvert.ObjectToFloat(strValue, defValue);
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            return EhConvert.StrToFloat(strValue, defValue);
        }
        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            return Validator.IsNumericArray(strNumber);
        }
        public static string AdDeTime(int times)
        {
            return DateTime.Now.AddMinutes((double)times).ToString();
        }
        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*$");
        }
        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry dictionaryEntry in NewHash)
            {
                try
                {
                    string[] array = SplitString(dictionaryEntry.Value.ToString(), "\r\n");
                    string[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string text = array2[i];
                        string a;
                        if (text != "" && (a = ruletype.Trim().ToLower()) != null)
                        {
                            if (!(a == "email"))
                            {
                                if (!(a == "ip"))
                                {
                                    if (a == "timesect")
                                    {
                                        string[] array3 = text.Split(new char[]
                                        {
                                            '-'
                                        });
                                        if (!IsTime(array3[1].ToString()) || !IsTime(array3[0].ToString()))
                                        {
                                            throw new Exception();
                                        }
                                    }
                                }
                                else
                                {
                                    if (!IsIPSect(text.ToString()))
                                    {
                                        throw new Exception();
                                    }
                                }
                            }
                            else
                            {
                                if (!IsValidDoEmail(text.ToString()))
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    key = dictionaryEntry.Key.ToString();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (!(str == ""))
            {
                return str.Substring(0, str.Length - 1);
            }
            return "";
        }
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && File.Exists(destFileName))
            {
                return false;
            }
            bool result;
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }
        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    File.Copy(targetFileName, backupTargetFileName, true);
                }
                File.Delete(targetFileName);
                File.Copy(backupFileName, targetFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }
        /// <summary>
        /// 获取记录模板id的cookie名称
        /// </summary>
        /// <returns></returns>
        public static string GetTemplateCookieName()
        {
            return TemplateCookieName;
        }
        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] array = SBCCase.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(array, i, 1);
                if (bytes.Length == 2 && bytes[1] == 255)
                {
                    bytes[0] = (byte)(bytes[0] + 32);
                    bytes[1] = 0;
                    array[i] = Encoding.Unicode.GetChars(bytes)[0];
                }
            }
            return new string(array);
        }
        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(string color)
        {
            color = color.TrimStart(new char[]
            {
                '#'
            });
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            int length = color.Length;
            char[] array;
            int red;
            int green;
            int blue;
            if (length == 3)
            {
                array = color.ToCharArray();
                red = Convert.ToInt32(array[0].ToString() + array[0].ToString(), 16);
                green = Convert.ToInt32(array[1].ToString() + array[1].ToString(), 16);
                blue = Convert.ToInt32(array[2].ToString() + array[2].ToString(), 16);
                return Color.FromArgb(red, green, blue);
            }
            if (length != 6)
            {
                return Color.FromName(color);
            }
            array = color.ToCharArray();
            red = Convert.ToInt32(array[0].ToString() + array[1].ToString(), 16);
            green = Convert.ToInt32(array[2].ToString() + array[3].ToString(), 16);
            blue = Convert.ToInt32(array[4].ToString() + array[5].ToString(), 16);
            return Color.FromArgb(red, green, blue);
        }
        /// <summary>
        /// 转换长文件名为短文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="repstring"></param>
        /// <param name="leftnum"></param>
        /// <param name="rightnum"></param>
        /// <param name="charnum"></param>
        /// <returns></returns>
        public static string ConvertSimpleFileName(string fullname, string repstring, int leftnum, int rightnum, int charnum)
        {
            string fileExtName = GetFileExtName(fullname);
            if (StrIsNullOrEmpty(fileExtName))
            {
                throw new Exception("字符串不含有扩展名信息");
            }
            int num = fullname.LastIndexOf('.');
            string text = fullname.Substring(0, num);
            int length = text.Length;
            string result;
            if (num > charnum)
            {
                string text2 = text.Substring(0, leftnum);
                string text3 = text.Substring(length - rightnum, rightnum);
                if (repstring == "" || repstring == null)
                {
                    result = text2 + text3 + "." + fileExtName;
                }
                else
                {
                    result = string.Concat(new string[]
                    {
                        text2,
                        repstring,
                        text3,
                        ".",
                        fileExtName
                    });
                }
            }
            else
            {
                result = fullname;
            }
            return result;
        }
        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(DataTable dt)
        {
            return DataTableToJson(dt, true);
        }
        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");
            string[] array = new string[dt.Columns.Count];
            int num = 0;
            string text = "{{";
            foreach (DataColumn dataColumn in dt.Columns)
            {
                array[num] = dataColumn.Caption.ToLower().Trim();
                text = text + "'" + dataColumn.Caption.ToLower().Trim() + "':";
                string text2 = dataColumn.DataType.ToString().Trim().ToLower();
                if (text2.IndexOf("int") > 0 || text2.IndexOf("deci") > 0 || text2.IndexOf("floa") > 0 || text2.IndexOf("doub") > 0 || text2.IndexOf("bool") > 0)
                {
                    object obj = text;
                    text = string.Concat(new object[]
                    {
                        obj,
                        "{",
                        num,
                        "}"
                    });
                }
                else
                {
                    object obj2 = text;
                    text = string.Concat(new object[]
                    {
                        obj2,
                        "'{",
                        num,
                        "}'"
                    });
                }
                text += ",";
                num++;
            }
            if (text.EndsWith(","))
            {
                text = text.Substring(0, text.Length - 1);
            }
            text += "}},";
            num = 0;
            object[] array2 = new object[array.Length];
            foreach (DataRow dataRow in dt.Rows)
            {
                string[] array3 = array;
                for (int i = 0; i < array3.Length; i++)
                {
                    string arg_1EF_0 = array3[i];
                    array2[num] = dataRow[array[num]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    string a;
                    if ((a = array2[num].ToString()) != null)
                    {
                        if (!(a == "True"))
                        {
                            if (a == "False")
                            {
                                array2[num] = "false";
                            }
                        }
                        else
                        {
                            array2[num] = "true";
                        }
                    }
                    num++;
                }
                num = 0;
                stringBuilder.Append(string.Format(text, array2));
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            if (dt_dispose)
            {
                dt.Dispose();
            }
            return stringBuilder.Append("\r\n];");
        }
        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            return str == null || str.Trim() == string.Empty;
        }
        /// <summary>
        /// 是否为数值串列表，各数值间用","间隔
        /// </summary>
        /// <param name="numList"></param>
        /// <returns></returns>
        public static bool IsNumericList(string numList)
        {
            return !StrIsNullOrEmpty(numList) && IsNumericArray(numList.Split(new char[]
            {
                ','
            }));
        }
        /// <summary>
        /// 检查颜色值是否为3/6位的合法颜色
        /// </summary>
        /// <param name="color">待检查的颜色</param>
        /// <returns></returns>
        public static bool CheckColorValue(string color)
        {
            if (StrIsNullOrEmpty(color))
            {
                return false;
            }
            color = color.Trim().Trim(new char[]
            {
                '#'
            });
            return (color.Length == 3 || color.Length == 6) && !Regex.IsMatch(color, "[^0-9a-f]", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 获取ajax形式的分页链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="callback">回调函数</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns></returns>
        public static string GetAjaxPageNumbers(int curPage, int countPage, string callback, int extendPage)
        {
            string text = "page";
            int num = 1;
            string text2 = "<a href=\"###\" onclick=\"" + string.Format(callback, "&" + text + "=1");
            string text3 = "<a href=\"###\" onclick=\"" + string.Format(callback, string.Concat(new object[]
            {
                "&",
                text,
                "=",
                countPage
            }));
            text2 += "\">&laquo;</a>";
            text3 += "\">&raquo;</a>";
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            int num2;
            if (countPage > extendPage)
            {
                if (curPage - extendPage / 2 > 0)
                {
                    if (curPage + extendPage / 2 < countPage)
                    {
                        num = curPage - extendPage / 2;
                        num2 = num + extendPage - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num = num2 - extendPage + 1;
                        text3 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    text2 = "";
                }
            }
            else
            {
                num = 1;
                num2 = countPage;
                text2 = "";
                text3 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(text2);
            for (int i = num; i <= num2; i++)
            {
                if (i == curPage)
                {
                    stringBuilder.Append("<span>");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</span>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"###\" onclick=\"");
                    stringBuilder.Append(string.Format(callback, text + "=" + i));
                    stringBuilder.Append("\">");
                    stringBuilder.Append(i);
                    stringBuilder.Append("</a>");
                }
            }
            stringBuilder.Append(text3);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 20000;
            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            return streamReader.ReadToEnd();
        }
        /// <summary>
        /// 转换时间为unix时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Math.Floor((date - d).TotalSeconds);
        }
        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        }
        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }
        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (StrIsNullOrEmpty(target))
            {
                target = source;
            }
            else
            {
                target = target + mergechar + source;
            }
            return target;
        }
        /// <summary>
        /// 清除UBB标签
        /// </summary>
        /// <param name="sDetail">帖子内容</param>
        /// <returns>帖子内容</returns>
        public static string ClearUBB(string sDetail)
        {
            return Regex.Replace(sDetail, "\\[[^\\]]*?\\]", string.Empty, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        //public static string GetRootUrl(string forumPath)
        //{
        //    int port = HttpContext.Current.Request.Url.Port;
        //    return string.Format("{0}://{1}{2}{3}", new object[]
        //    {
        //        HttpContext.Current.Request.Url.Scheme,
        //        HttpContext.Current.Request.Url.Host.ToString(),
        //        (port == 80 || port == 0) ? "" : (":" + port),
        //        forumPath
        //    });
        //}
        /// <summary>
        /// 获取指定文件的扩展名
        /// </summary>
        /// <param name="fileName">指定文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileExtName(string fileName)
        {
            if (StrIsNullOrEmpty(fileName) || fileName.IndexOf('.') <= 0)
            {
                return "";
            }
            fileName = fileName.ToLower().Trim();
            return fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
        }
        public static string GetHttpWebResponse(string url)
        {
            return GetHttpWebResponse(url, string.Empty);
        }
        /// <summary>
        /// http POST请求url
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="method_name"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private static string GetHttpWebResponse(string url, string postData)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = (long)postData.Length;
            httpWebRequest.Timeout = 20000;
            HttpWebResponse httpWebResponse = null;
            string result;
            try
            {
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                streamWriter.Write(postData);
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            finally
            {
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
            return result;
        }
        public static string NewGuid32()
        {
            string text = Guid.NewGuid().ToString();
            int num = text.IndexOf('-', 0);
            int num2 = text.IndexOf('-', num + 1);
            int num3 = text.IndexOf('-', num2 + 1);
            int num4 = text.IndexOf('-', num3 + 1);
            text = string.Concat(new string[]
            {
                text.Substring(0, num),
                text.Substring(num + 1, num2 - num - 1),
                text.Substring(num2 + 1, num3 - num2 - 1),
                text.Substring(num3 + 1, num4 - num3 - 1),
                text.Substring(num4 + 1)
            });
            return text.ToUpper();
        }
        public static string NewGuid32L()
        {
            string text = Guid.NewGuid().ToString();
            int num = text.IndexOf('-', 0);
            int num2 = text.IndexOf('-', num + 1);
            int num3 = text.IndexOf('-', num2 + 1);
            int num4 = text.IndexOf('-', num3 + 1);
            text = string.Concat(new string[]
            {
                text.Substring(0, num),
                text.Substring(num + 1, num2 - num - 1),
                text.Substring(num2 + 1, num3 - num2 - 1),
                text.Substring(num3 + 1, num4 - num3 - 1),
                text.Substring(num4 + 1)
            });
            return text.ToLower();
        }
    }
}
