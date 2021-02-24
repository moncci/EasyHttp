using EasyHttp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EasyHttp.Def;
using EasyHttp.Enum;

namespace EasyHttp
{
    /// <summary>
    /// 请求信息
    /// </summary>
    public class RequestObject
    {
        /// <summary>
        /// 请求的地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 本地的请求ip和端口
        /// </summary>
        public IPEndPoint IPEndPoint { get; set; }

        /// <summary>
        /// 自动解压缩方式
        /// </summary>
        private DecompressionMethods _automaticDecompression = DecompressionMethods.None;
        /// <summary>
        /// 自动解压缩方式
        /// </summary>
        public DecompressionMethods AutomaticDecompression
        {
            get { return _automaticDecompression; }
            set { _automaticDecompression = value; }
        }

        /// <summary>
        /// 证书链表
        /// </summary>
        public List<X509Certificate> CertificateList { get; set; } = new List<X509Certificate>();

        /// <summary>
        /// 请求头
        /// </summary>
        private WebHeaderCollection _headers = new WebHeaderCollection();
        /// <summary>
        /// 请求头
        /// </summary>

        public WebHeaderCollection Headers
        {
            get { return _headers; }
        }

        /// <summary>
        /// 代理ip 格式192.168.1.1:9999
        /// </summary>
        public string ProxyIp { get; set; }
        /// <summary>
        /// 代理用户名
        /// </summary>
        public string ProxyUserName { get; set; }
        /// <summary>
        /// 代理密码
        /// </summary>
        public string ProxyPassword { get; set; }

        /// <summary>
        /// 自定义代理
        /// </summary>
        public WebProxy WebProxy { get; set; }

        /// <summary>
        /// 用于请求的 HTTP 版本。 默认值为 Version11
        /// 使用 HttpVersion 类的 Version10 和 Version11 字段
        /// </summary>
        public Version ProtocolVersion { get; set; } = HttpVersion.Version11;

        /// <summary>
        /// 该值确定是否使用 100-Continue 行为。
        /// 如果 POST 请求需要 100-Continue 响应，则为 true；否则为 false。 默认值为 true。
        /// </summary>
        public bool Expect100Continue { get; set; } = true;
        /// <summary>
        /// 获取或设置请求的方法
        /// </summary>
        public string Method { get; set; } = MethodDef.GET;
        /// <summary>
        /// 获取或设置 System.Net.HttpWebRequest.GetResponse 和 System.Net.HttpWebRequest.GetRequestStream
        /// 方法的超时值（以毫秒为单位）。
        /// 请求超时前等待的毫秒数。默认值是 100,000 毫秒（100 秒）。
        /// </summary>
        public int Timeout { get; set; } = 100000;
        /// <summary>
        /// 获取或设置一个值，该值指示是否与 Internet 资源建立持续型连接。
        /// 如果对 Internet 资源的请求所包含的 HTTP Connection 标头带有 Keep-alive 这一值，则为 true；否则为 false。
        /// 默认值为 true。
        /// </summary>
        public bool KeepAlive { get; set; } = true;
        /// <summary>
        /// 获取或设置写入或读取流时的超时（以毫秒为单位）。
        /// 在写入超时或读取超时之前的毫秒数。默认值为 300,000 毫秒（5 分钟）。
        /// </summary>
        public int ReadWriteTimeout { get; set; } = 300000;

        /// <summary>
        /// 获取或设置要在 HTTP 请求中独立于请求 URI 使用的 Host 标头值。
        /// HTTP 请求中的 Host 标头值。
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 获取或设置 If-Modified-Since HTTP 标头的值。
        /// 包含 HTTP If-Modified-Since 标头内容的 System.DateTime。 默认值是当前日期和时间。
        /// </summary>
        public DateTime? IfModifiedSince { get; set; } = null;
        /// <summary>
        /// 获取或设置 Accept HTTP 标头的值。
        /// Accept HTTP 标头的值。 
        /// </summary>
        public string Accept { get; set; } = "text/html, application/xhtml+xml, */*";
        /// <summary>
        /// 获取或设置 Content-type HTTP 标头的值。
        /// Content-type HTTP 标头的值。 
        /// </summary>
        public string ContentType { get; set; } = "text/html";
        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; } = UserAgentDef.Ie9;
        /// <summary>
        /// 采用指定的编码解析数据（如果不设则根据相应数据来自动适配编码）
        /// </summary>  
        public Encoding Encoding { get; set; }
        /// <summary>
        /// post编码
        /// </summary>
        public Encoding PostEncoding { get; set; } = Encoding.Default;
        /// <summary>
        /// 获取或设置请求的身份验证信息。
        /// 包含与该请求关联的身份验证凭据的 System.Net.ICredentials。
        /// </summary>
        public ICredentials ICredentials { get; set; } = CredentialCache.DefaultCredentials;
        /// <summary>
        /// cookie类型
        /// </summary>
        public CookieType CookieType { get; set; } = CookieType.String;
        /// <summary>
        /// 字符串Cookie
        /// </summary>
        public string Cookie { get; set; }
        /// <summary>
        /// cookie链表
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        /// <summary>
        /// cookie容器
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// 请求来源,比如是从百度跳转及为http://www.baidu.com 默认值为 null。
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示请求是否应跟随重定向响应。
        /// 如果请求应自动遵循来自 Internet 资源的重定向响应，则为 true；否则为 false。 默认值为 true。
        /// </summary>
        public bool AllowAutoRedirect { get; set; } = true;
        /// <summary>
        /// 获取或设置请求将跟随的重定向的最大数目。
        /// 请求将跟随的重定向响应的最大数目。 默认值为 50。
        /// </summary>
        public int MaximumAutomaticRedirections { get; set; } = 50;
        /// <summary>
        /// post数据类型 默认string
        /// </summary>
        //public PostDataType PostDataType { get; set; } = PostDataType.String;
        /// <summary>
        /// post byte字节数组
        /// </summary>
        //public byte[] PostdataByte { get; set; }
        /// <summary>
        /// post数据
        /// </summary>
        //public string Postdata { get; set; }
        /// <summary>
        /// 请求负载
        /// </summary>
        public byte[] Payload { get; private set; }
        /// <summary>
        /// 允许最大连接数
        /// </summary>
        public int ConnectionLimit { get; set; } = 1024;
        /// <summary>
        /// 获取或设置由 System.Net.ServicePointManager 对象管理的 System.Net.ServicePoint 对象所使用的安全协议。
        /// </summary>
        public SecurityProtocolType SecurityProtocol { get; set; }
        /// <summary>
        /// 是否更新cookie
        /// </summary>
        public bool IsUpdateCookie { get; set; }
        /// <summary>
        /// 设置证书
        /// </summary>
        /// <param name="path">证书路径</param>
        /// <param name="password">密码</param>
        public void SetCertificate(string path, string password = "")
        {
            CertificateList = new List<X509Certificate>();
            AddCertificatePrivate(path, password);
        }

        /// <summary>
        /// 添加证书
        /// </summary>
        /// <param name="path">证书路径</param>
        /// <param name="password">密码</param>
        public void AddCertificate(string path, string password = "")
        {
            AddCertificatePrivate(path, password);
        }

        /// <summary>
        /// 设置请求负载
        /// </summary>
        /// <param name="value">请求内容</param>
        public void SetPayload(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                Payload = PostEncoding.GetBytes(value);
            }
        }

        /// <summary>
        /// 设置请求负载
        /// </summary>
        /// <param name="value">请求内容</param>
        public void SetPayload(byte[] value)
        {
            if (value != null && value.Length > 0)
            {
                Payload = value;
            }
        }

        /// <summary>
        /// 设置请求负载
        /// </summary>
        /// <param name="value">请求内容</param>
        public void SetPayloadFilePath(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader streamReader = new StreamReader(filePath, PostEncoding))
                    {
                        Payload = PostEncoding.GetBytes(streamReader.ReadToEnd());
                    }
                }
            }
        }


        /// <summary>
        /// 添加证书
        /// </summary>
        /// <param name="path"></param>
        /// <param name="password"></param>
        private void AddCertificatePrivate(string path, string password = "")
        {
            if (File.Exists(path))
            {
                if (string.IsNullOrEmpty(password))
                {
                    X509Certificate cer = new X509Certificate(path);
                    CertificateList.Add(cer);
                }
                else
                {
                    X509Certificate cer = new X509Certificate(path, password);
                    CertificateList.Add(cer);
                }
            }
            else
            {
                throw new EasyHttpException("证书文件不存在。");
            }
        }
    }
}
