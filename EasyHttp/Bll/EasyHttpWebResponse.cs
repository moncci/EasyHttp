using EasyHttp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace EasyHttp.Bll
{
    internal class EasyHttpWebResponse:IDisposable
    {
        /// <summary>
        /// 请求参数对象
        /// </summary>
        private RequestObject _requestObject = null;

        /// <summary>
        /// 相应对象
        /// </summary>
        private HttpWebResponse _response = null;

        /// <summary>
        /// 构造方式
        /// </summary>
        /// <param name="response"></param>
        /// <param name="requestObject"></param>
        internal EasyHttpWebResponse(HttpWebResponse response, RequestObject requestObject)
        {
            _response = response;
            _requestObject = requestObject;
        }

        /// <summary>
        /// 创建相应对象
        /// </summary>
        /// <returns></returns>
        internal static EasyHttpWebResponse CreateResponse(HttpWebResponse response, RequestObject reqObj)
        {
            EasyHttpWebResponse result = null;
            result = new EasyHttpWebResponse(response, reqObj);
            return result;
        }

        public void Dispose()
        {
            _response.Dispose();
            _requestObject = null;
        }

        public ResponseObject GetResposeObject()
        {
            if (_requestObject == null)
            {
                throw new EasyHttpException("请求参数对象不能为空。");
            }
            ResponseObject result = new ResponseObject();
            result.RequestObject = _requestObject;
            result.CharacterSet = _response.CharacterSet;
            result.StatusCode = _response.StatusCode;
            result.ResponseUri = _response.ResponseUri.ToString();
            result.StatusDescription = _response.StatusDescription;
            result.Headers = _response.Headers;
            if (_response.Cookies != null)
            {
                result.CookieCollection = _response.Cookies;
            }
            if (_response.Headers["set-cookie"] != null)
            {
                result.Cookie = _response.Headers["set-cookie"];
            }
            if (_requestObject.IsUpdateCookie)
            {
                _requestObject.Cookie = result.Cookie;
                _requestObject.CookieCollection = result.CookieCollection;
            }
            byte[] @byte = GetByte();
            result.OriginalByteData = @byte;
            return result;
        }

        /// <summary>
        /// 获取相应的内容的字节数组信息
        /// </summary>
        /// <returns></returns>
        private byte[] GetByte()
        {
            byte[] result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                if (_requestObject.AutomaticDecompression == DecompressionMethods.GZip)
                {
                    new GZipStream(_response.GetResponseStream(), CompressionMode.Decompress).CopyTo(memoryStream, 10240);
                }
                else
                {
                    if (_response.ContentEncoding != null && _response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        new GZipStream(_response.GetResponseStream(), CompressionMode.Decompress).CopyTo(memoryStream, 10240);
                    }
                    else
                    {
                        _response.GetResponseStream().CopyTo(memoryStream, 10240);
                    }
                }
                result = memoryStream.ToArray();
            }
            return result;
        }
    }
}
