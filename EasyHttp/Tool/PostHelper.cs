using EasyHttp.Def;
using EasyHttp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EasyHttp.Tool
{
    /// <summary>
    /// post帮助类
    /// </summary>
    public static class PostHelper
    {
        /// <summary>
        /// 获取上传文件请求对象(form-data)
        /// </summary>
        /// <param name="reqObjTemp"></param>
        /// <param name="formItems">文件字段</param>
        /// <returns></returns>
        public static RequestObject PostFormFile(RequestObject reqObjTemp, List<FormItemModel> formItems)
        {
            return PostFormFileIn(reqObjTemp, formItems);
        }

        /// <summary>
        /// 获取文件byte数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] GetFileContent(string fileName)
        {
            return GetFileContentIn(fileName);
        }

        /// <summary>
        /// 获取post请求数据(application/x-www-form-urlencoded模式)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">上传对象</param>
        /// <returns></returns>
        public static string GetPostDataFormWwwUrlencoded<T>(T model)
        {
            return GetPostDataFormWwwUrlencodedIn<T>(model);
        }

        /// <summary>
        /// 获取post请求数据(application/x-www-form-urlencoded模式)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">上传对象</param>
        /// <returns></returns>
        private static string GetPostDataFormWwwUrlencodedIn<T>(T model)
        {
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo info in ps)
            {
                Attribute tempAttribute = info.GetCustomAttribute(typeof(PostNameAttribute));
                string name = string.Empty;
                if (tempAttribute != null)
                {
                    PostNameAttribute customAttribute = (PostNameAttribute)tempAttribute;
                    name = customAttribute.PostName;
                }
                else
                {
                    name = info.Name;
                }
                string value = EhConvert.GetNoNullString(info.GetValue(model, null));
                value = System.Web.HttpUtility.UrlEncode(value);
                sb.Append(name + "=" + value + "&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 获取上传文件请求对象(form-data)
        /// </summary>
        /// <param name="reqObjTemp"></param>
        /// <param name="formItems">文件字段</param>
        /// <returns></returns>
        private static RequestObject PostFormFileIn(RequestObject reqObjTemp, List<FormItemModel> formItems)
        {
            RequestObject reqObj = reqObjTemp;
            if (reqObj == null)
            {
                reqObj = new RequestObject();
            }
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");//分隔符

            reqObj.Method = MethodDef.POST;
            reqObj.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);

            //请求流
            var postStream = new MemoryStream();
            #region 处理Form表单请求内容
            //是否用Form上传文件
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: text/plain" +
                    "\r\n\r\n";
                //文本数据模板
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        //上传文件
                        formdata = string.Format(
                            fileFormdataTemplate,
                            item.Key, //表单键
                            item.FileName);
                    }
                    else
                    {
                        //上传文本
                        formdata = string.Format(
                            dataFormdataTemplate,
                            item.Key,
                            item.Value);
                    }

                    //统一处理
                    byte[] formdataBytes = null;
                    //第一行不需要换行
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容
                    if (item.FileContent != null && item.FileContent.Length > 0)
                    {
                        postStream.Write(item.FileContent, 0, item.FileContent.Length);
                    }
                }
                //结尾
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);
                //reqObj.PostDataType = Enum.PostDataType.Byte;
                reqObj.SetPayload(postStream.ToArray());

            }
            else
            {
                reqObj.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion
            return reqObj;
        }

        /// <summary>
        /// 获取文件byte数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static byte[] GetFileContentIn(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    byte[] buffur = new byte[fs.Length];
                    fs.Read(buffur, 0, (int)fs.Length);
                    return buffur;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
