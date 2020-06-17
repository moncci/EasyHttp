using EasyHttp.Def;
using EasyHttp.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using EasyHttp.Tool;

namespace EasyHttp
{
    /// <summary>
    /// 相应对象
    /// </summary>
    public class ResponseObject
    {
        #region 请求参数对象
        /// <summary>
        /// 请求参数对象
        /// </summary>
        private RequestObject _requestObject;
        /// <summary>
        /// 请求参数对象
        /// </summary>
        internal RequestObject RequestObject
        {
            get
            {
                return _requestObject;
            }
            set
            {
                _requestObject = value;
                if (value != null)
                {
                    _encoding = value.Encoding;
                }
                else
                {
                    _encoding = null;
                }
            }
        }
        #endregion
        /// <summary>
        /// 数据编码
        /// </summary>
        private Encoding _encoding;
        /// <summary>
        /// 文档字符编码
        /// </summary>
        internal string CharacterSet { get; set; }
        /// <summary>
        /// HTTP 状态代码的值
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 相应url
        /// </summary>
        public string ResponseUri { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// 相应头
        /// </summary>
        public WebHeaderCollection Headers { get; set; }
        /// <summary>
        /// 相应Cookies
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        /// <summary>
        /// 相应Cookie
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// 相应原始数据
        /// </summary>
        private byte[] _originalByteData;
        /// <summary>
        /// 相应原始数据
        /// </summary>
        internal byte[] OriginalByteData
        {
            get
            {
                return _originalByteData;
            }
            set
            {
                _originalByteData = value;
                SetEncoding(_originalByteData);
                if (string.IsNullOrWhiteSpace(_htmlOriginal))
                {
                    _htmlOriginal = _encoding.GetString(OriginalByteData);
                }
                if (!string.IsNullOrWhiteSpace(_htmlOriginal))
                {
                    _htmlSimple = _htmlOriginal
                        .RemoveComment()
                        .RemoveStyle()
                        .RemoveScript();
                }
            }
        }

        /// <summary>
        /// 原始的html数据
        /// </summary>
        private string _htmlOriginal = null;
        /// <summary>
        /// 简化后的html串
        /// </summary>
        private string _htmlSimple = null;

        #region 设定解析数据的编码
        /// <summary>
        /// 设定解析数据的编码
        /// </summary>
        /// <param name="ResponseByte">相应数据</param>

        private void SetEncoding(byte[] ResponseByte)
        {
            if (ResponseByte != null)
            {
                if (_encoding == null)
                {
                    Match match = Regex.Match(Encoding.Default.GetString(ResponseByte), RegexDef.Enconding, RegexOptions.IgnoreCase);
                    //meta Charset设定的编码
                    string encodingMetaCharset = string.Empty;
                    if (match != null && match.Groups.Count > 0)
                    {
                        encodingMetaCharset = match.Groups[1].Value.ToLower().Trim();
                    }

                    //Reaponse Charset设定的编码
                    string encodingReaponseCharset = string.Empty;
                    if (!string.IsNullOrWhiteSpace(CharacterSet))
                    {
                        encodingReaponseCharset = CharacterSet.Trim().Replace("\"", "").Replace("'", "");
                    }
                    if (encodingMetaCharset.Length > 2)
                    {
                        try
                        {
                            _encoding = Encoding.GetEncoding(encodingMetaCharset.Replace("\"", string.Empty).Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim());
                            return;
                        }
                        catch
                        {
                            if (string.IsNullOrWhiteSpace(encodingReaponseCharset))
                            {
                                _encoding = Encoding.UTF8;
                            }
                            else
                            {
                                _encoding = Encoding.GetEncoding(encodingReaponseCharset);
                            }
                            return;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(encodingReaponseCharset))
                    {
                        _encoding = Encoding.UTF8;
                        return;
                    }
                    _encoding = Encoding.GetEncoding(encodingReaponseCharset);
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取字节数组数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetByteData()
        {
            return OriginalByteData;
        }

        /// <summary>
        /// 获取字符串数据
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return _htmlOriginal;
        }

        /// <summary>
        /// 获取简化字符串数据（去除注释、style、script）
        /// </summary>
        /// <returns></returns>
        public string GetHtmlSimple()
        {
            return _htmlSimple;
        }

        #region 根据元素id获取标签
        /// <summary>
        /// 根据元素id获取标签
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>选择的标签</returns>
        public TagBase GetTagById(string id)
        {
            return _htmlSimple.GetTagById<TagBase>(id);            
        }
        #endregion

        #region 获取所有A标签
        /// <summary>
        /// 获取所有A标签
        /// </summary>
        /// <returns>选择的元素</returns>
        public List<TagA> GetAllATag()
        {
            return _htmlSimple.GetTagListByTagName<TagA>(TagType.a);            
        }
        #endregion

        #region 获取所有img标签
        /// <summary>
        /// 获取所有img标签
        /// </summary>
        /// <returns>选择的元素</returns>
        public List<TagImg> GetAllImgTag()
        {
            return _htmlSimple.GetTagListByTagName<TagImg>(TagType.img);            
        }
        #endregion
        
    }
}
