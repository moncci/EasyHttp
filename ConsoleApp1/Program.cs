using EasyHttp;
using EasyHttp.Def;
using EasyHttp.Tool;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherTest.Instance.Get();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static int GetRandom(int min, int max)
        {
            int random = new Random(Guid.NewGuid().GetHashCode()).Next(min, max + 1);
            return random;
        }

        static void Test1()
        {
            RequestObject reqObj = new RequestObject()
            {
                Url = "http://www.sc.gov.cn/",
                Method = MethodDef.GET,
                ContentType = RequestContentTypeDef.TEXT_HTML
            };

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            string html = result.GetHtml();
            Console.WriteLine(html);
        }

        static void Test2()
        {
            TaskModel model = new TaskModel();
            model.ModuleName = "moduleName1";
            model.ModuleType = 1;
            model.TaskContent = "content1";
            model.TaskId = "id1";
            model.TaskState = "ok";
            model.TaskType = "type1";
            string json = model.ObjectToJson();

            RequestObject reqObj = new RequestObject()
            {
                Url = "http://localhost:62101/api/Version/post_test2",
                Method = MethodDef.POST,
                ContentType = RequestContentTypeDef.JSON,
            };
            reqObj.SetPayload(json);

            EasyRequest request = new EasyRequest();
            ResponseObject result = request.GetResponse(reqObj);
            string html = result.GetHtml();
            Console.WriteLine(html);
        }
    }
}
