using EasyHttp.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace EasyHttp.Bll
{
    internal class EasyHttpWebRequest
    {
        private Encoding _encoding = Encoding.Default;
        private Encoding _postEncoding = Encoding.Default;
        /// <summary>
        /// 请求参数对象
        /// </summary>
        private RequestObject _requestObject;
        /// <summary>
        /// 请求对象
        /// </summary>
        HttpWebRequest _request;

        private EasyHttpWebRequest()
        {
        }

        internal static EasyHttpWebRequest Create(RequestObject obj)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(obj.Url);
            EasyHttpWebRequest http = new EasyHttpWebRequest();
            http._request = request;
            http._requestObject = obj;
            http.Ini();
            return http;
        }

        internal EasyHttpWebResponse GetResponse()
        {
            EasyHttpWebResponse result = null;
            HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
            result = new EasyHttpWebResponse(response, _requestObject);
            return result;
        }

        private void Ini()
        {
            if (_requestObject.IPEndPoint != null)
            {
                _request.ServicePoint.BindIPEndPointDelegate = new BindIPEndPoint(OnBindIPEndPoint);
            }
            _request.AutomaticDecompression = _requestObject.AutomaticDecompression;
            SetCertificate(_requestObject);
            if (_requestObject.Headers != null && _requestObject.Headers.Count > 0)
            {
                _request.Headers = _requestObject.Headers;
            }
            SetProxy(_requestObject);
            if (_requestObject.ProtocolVersion != null)
            {
                _request.ProtocolVersion = _requestObject.ProtocolVersion;
            }
            _request.ServicePoint.Expect100Continue = _requestObject.Expect100Continue;
            _request.Method = _requestObject.Method;
            _request.Timeout = _requestObject.Timeout;
            _request.KeepAlive = _requestObject.KeepAlive;
            _request.ReadWriteTimeout = _requestObject.ReadWriteTimeout;
            if (!string.IsNullOrWhiteSpace(_requestObject.Host))
            {
                _request.Host = _requestObject.Host;
            }
            if (_requestObject.IfModifiedSince.HasValue)
            {
                _request.IfModifiedSince = _requestObject.IfModifiedSince.Value;
            }
            _request.Accept = _requestObject.Accept;
            _request.ContentType = _requestObject.ContentType;
            _request.UserAgent = _requestObject.UserAgent;
            _encoding = _requestObject.Encoding;
            _request.Credentials = _requestObject.ICredentials;
            SetCookie(_requestObject);
            _request.Referer = _requestObject.Referer;
            _request.AllowAutoRedirect = _requestObject.AllowAutoRedirect;
            if (_requestObject.MaximumAutomaticRedirections > 0)
            {
                _request.MaximumAutomaticRedirections = _requestObject.MaximumAutomaticRedirections;
            }
            SetPostData(_requestObject);
            if (_requestObject.ConnectionLimit > 0)
            {
                _request.ServicePoint.ConnectionLimit = _requestObject.ConnectionLimit;
            }
            if (_requestObject.SecurityProtocol > SecurityProtocolType.SystemDefault)
            {
                ServicePointManager.SecurityProtocol = _requestObject.SecurityProtocol;
            }
        }

        private IPEndPoint OnBindIPEndPoint(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
        {
            return _requestObject.IPEndPoint;
        }

        /// <summary>
        /// 设置证书
        /// </summary>
        /// <param name="obj"></param>
        private void SetCertificate(RequestObject obj)
        {
            if (obj.CertificateList != null && obj.CertificateList.Count > 0)
            {
                foreach (var cef in obj.CertificateList)
                {
                    _request.ClientCertificates.Add(cef);
                }
            }
        }

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="obj"></param>
        private void SetProxy(RequestObject obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.ProxyIp))
            {

                if (obj.ProxyIp.Contains(":"))
                {
                    string[] array = obj.ProxyIp.Split(':');
                    WebProxy webProxy = new WebProxy(array[0].Trim(), Convert.ToInt32(array[1].Trim()));
                    webProxy.Credentials = new NetworkCredential(obj.ProxyUserName, obj.ProxyPassword);
                    _request.Proxy = webProxy;
                }
                else
                {
                    WebProxy webProxy = new WebProxy(obj.ProxyIp, false);
                    webProxy.Credentials = new NetworkCredential(obj.ProxyUserName, obj.ProxyPassword);
                    _request.Proxy = webProxy;
                }
            }
            else
            {
                if (obj.WebProxy != null)
                {
                    _request.Proxy = obj.WebProxy;
                }
            }
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="obj"></param>
        private void SetCookie(RequestObject obj)
        {
            switch (obj.CookieType)
            {
                case Enum.CookieType.String:
                    if (!string.IsNullOrWhiteSpace(obj.Cookie))
                    {
                        _request.Headers[HttpRequestHeader.Cookie] = obj.Cookie;
                    }
                    break;
                case Enum.CookieType.CookieCollection:
                    if (obj.CookieCollection != null && obj.CookieCollection.Count > 0)
                    {
                        _request.CookieContainer = new CookieContainer();
                        _request.CookieContainer.Add(obj.CookieCollection);
                        return;
                    }
                    break;
                case Enum.CookieType.CookieContainer:
                    if (obj.CookieContainer != null)
                    {
                        _request.CookieContainer = obj.CookieContainer;
                    }
                    break;
            }
        }

        /// <summary>
        /// 设置post数据
        /// </summary>
        /// <param name="obj"></param>
        private void SetPostData(RequestObject obj)
        {
            if (_request.Method.Trim().ToLower() != "get")
            {
                if (obj.PostEncoding != null)
                {
                    _postEncoding = obj.PostEncoding;
                }
                byte[] array = null;
                if (obj.PostDataType == PostDataType.Byte && obj.PostdataByte != null && obj.PostdataByte.Length != 0)
                {
                    array = obj.PostdataByte;
                }
                else
                {
                    if (obj.PostDataType == PostDataType.FilePath && !string.IsNullOrWhiteSpace(obj.Postdata))
                    {
                        StreamReader streamReader = new StreamReader(obj.Postdata, _postEncoding);
                        array = _postEncoding.GetBytes(streamReader.ReadToEnd());
                        streamReader.Close();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(obj.Postdata))
                        {
                            array = _postEncoding.GetBytes(obj.Postdata);
                        }
                    }
                }
                if (array != null)
                {
                    _request.ContentLength = (long)array.Length;
                    _request.GetRequestStream().Write(array, 0, array.Length);
                    return;
                }
                _request.ContentLength = 0L;
            }
        }
    }
}
