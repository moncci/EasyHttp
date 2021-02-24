using EasyHttp;
using EasyHttp.Def;
using EasyHttp.Model;
using EasyHttp.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class WeatherTest
    {
        private static WeatherTest _instance;

        public static WeatherTest Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new WeatherTest();
                }
                return _instance; }
        }

        public void Get()
        {
            List<string> urls = new List<string>();
            //http://www.ccgp-sichuan.gov.cn/view/srplatform/portal/index.html 四川政府采购网
            urls.Add("http://www.weather.com.cn/textFC/hb.shtml");//华北
            urls.Add("http://www.weather.com.cn/textFC/db.shtml");//东北
            urls.Add("http://www.weather.com.cn/textFC/hd.shtml");//华东
            urls.Add("http://www.weather.com.cn/textFC/hz.shtml");//华中
            urls.Add("http://www.weather.com.cn/textFC/hn.shtml");//华南
            urls.Add("http://www.weather.com.cn/textFC/xb.shtml");//西北
            urls.Add("http://www.weather.com.cn/textFC/xn.shtml");//西南
            urls.Add("http://www.weather.com.cn/textFC/gat.shtml");//港澳台
            RequestObject reqObj = new RequestObject()
            {
                Url = urls[7],
                Method = MethodDef.GET,
                ContentType = RequestContentTypeDef.TEXT_HTML
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            string html = result.GetHtmlSimple();
            List<TagBase> table = TagHelper.GetTagListByTagName<TagBase>(html, TagType.table);
            Console.WriteLine(html);
        }

    }
}