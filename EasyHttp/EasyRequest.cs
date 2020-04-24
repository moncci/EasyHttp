using EasyHttp.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EasyHttp.Bll;
using System.IO.Compression;
using System.Text.RegularExpressions;
using EasyHttp.Def;

namespace EasyHttp
{
    /// <summary>
    /// http请求类
    /// </summary>
    public class EasyRequest
    {                
        public EasyRequest()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);
        }

        public ResponseObject GetResponse(RequestObject reqObj)
        {
            //if (obj.AllowAutoRedirect && obj.AutoRedirectCookie)
            //{
            //    HttpResult httpResult = null;
            //    for (int i = 0; i < 100; i++)
            //    {
            //        obj.AllowAutoRedirect = false;
            //        httpResult = this.httpbase.GetHtml(obj);
            //        if (string.IsNullOrWhiteSpace(httpResult.RedirectUrl))
            //        {
            //            break;
            //        }
            //        obj.URL = httpResult.RedirectUrl;
            //        obj.Method = "GET";
            //        if (obj.ResultCookieType == ResultCookieType.String)
            //        {
            //            obj.Cookie += httpResult.Cookie;
            //        }
            //        else
            //        {
            //            obj.CookieCollection.Add(httpResult.CookieCollection);
            //        }
            //    }
            //    return httpResult;
            //}
            //return this.httpbase.GetHtml(item);

            EasyHttpWebRequest request = null;

            ResponseObject httpResult = new ResponseObject();
            try
            {
                request = CreateRequest(reqObj);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                {
                    Cookie = string.Empty,
                    Headers = null,
                    StatusDescription = "配置参数时出错：" + ex.Message
                };
            }
            try
            {
                using (EasyHttpWebResponse response = (EasyHttpWebResponse)request.GetResponse())
                {
                    httpResult = response.GetResposeObject();
                }
            }
            catch (WebException ex2)
            {
                httpResult.StatusDescription = ex2.Message;
                if (ex2.Response != null)
                {
                    using (HttpWebResponse response = (HttpWebResponse)ex2.Response)
                    {
                        using (EasyHttpWebResponse easyResponse = EasyHttpWebResponse.CreateResponse(response, reqObj))
                        {
                            httpResult = easyResponse.GetResposeObject();
                        }
                    }
                }
            }
            catch (Exception ex3)
            {
                httpResult.StatusDescription = ex3.Message;
            }
            return httpResult;
        }

        /// <summary>
        /// 创建请求对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private EasyHttpWebRequest CreateRequest(RequestObject obj)
        {
            EasyHttpWebRequest request = EasyHttpWebRequest.Create(obj);
            return request;
        }

        /// <summary>
        /// 忽略证书认证错误处理的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool OnRemoteCertificateValidationCallback(
          Object sender,
          X509Certificate certificate,
          X509Chain chain,
          SslPolicyErrors sslPolicyErrors)
        {
            // 认证正常，没有错误
            return true;
        }       
    }
}
