using EasyHttp.Def;
using EasyHttp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyHttp.Test
{
    [TestClass]
    public class UnitTestEasyHttp1
    {
        [TestMethod]
        public void TestGetHtml()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.sc.gov.cn/",
                Method = MethodDef.GET,
                ContentType = "text/html"
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            string html = result.GetHtml();
            Console.WriteLine(html);
        }

        [TestMethod]
        public void TestGetHtmlSimple()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.sc.gov.cn/",
                Method = MethodDef.GET,
                ContentType = "text/html"
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            string html = result.GetHtmlSimple();
            Console.WriteLine(html);
        }

        [TestMethod]
        public void TestGetTagById()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.sc.gov.cn/",
                Method = MethodDef.GET,
                ContentType = "text/html"
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            List<TagBase> listElemnet = result.GetTagById("demo_newsFocus");
            if (listElemnet != null)
            {
                foreach (var item in listElemnet)
                {
                    Console.WriteLine(item.Html);
                }
            }
        }

        [TestMethod]
        public void TestGetAllImg()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.sc.gov.cn/",
                Method = MethodDef.GET,
                ContentType = "text/html"
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            List<TagImg> listElemnet = result.GetAllImgTag();
            if (listElemnet != null)
            {
                foreach (var item in listElemnet)
                {
                    Console.WriteLine(item.Src);
                }
            }
        }

        [TestMethod]
        public void TestGetAllA()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.sc.gov.cn/",
                Method = MethodDef.GET,
                ContentType = "text/html"
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            List<TagA> listElemnet = result.GetAllATag();
            if (listElemnet != null)
            {
                foreach (var item in listElemnet)
                {
                    Console.WriteLine(item.Html);
                }
            }
        }

        [TestMethod]
        public void TestTianqi()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.weather.com.cn/",
                Method = MethodDef.GET,
                ContentType = "text/html"
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            string html = result.GetHtmlSimple();


            RequestObject reqObj2 = new RequestObject()
            {
                Url = "http://d1.weather.com.cn/weather_index/101272001.html?_=1575967810099",
                Method = MethodDef.GET,
                ContentType = "text/html",
                Referer = result.ResponseUri,
                Encoding = Encoding.UTF8
            };

            EasyRequest request2 = new EasyRequest();
            ResponseObject result2 = request2.GetResponse(reqObj2);
        }

        [TestMethod]
        public void RegTest()
        {
            string text = "var loginUserId = \"8aasfadf8aasfadf8aasfadf8aasfadf\";";

            string pattern = "var loginUserId = \"(?<userid>[a-z|0-9]{32})\";"; //匹配URL的模式,并分组
            Match mc = Regex.Match(text, pattern); //满足pattern的匹配集合
            string userid = mc.Groups["userid"].Value;
        }
    }
}
