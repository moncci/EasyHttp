using EasyHttp;
using EasyHttp.Def;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
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
            Console.WriteLine("Hello World!");
        }
    }
}
