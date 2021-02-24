using EasyHttp.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EasyHttp.Tool
{
    /// <summary>
    /// json转换
    /// </summary>
    public static class JsonConvertTool
    {
        private static JsonSerializerOptions _option = null;
        /// <summary>
        /// 配置项目
        /// </summary>
        public static JsonSerializerOptions Option
        {
            get
            {
                if (_option == null)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    _option = options;

                    IniOption(_option);
                }
                return _option;
            }
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="options"></param>
        public static void IniOption(JsonSerializerOptions options)
        {
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            options.IgnoreNullValues = true;
            options.PropertyNamingPolicy = new KeepJsonNamingPolicy();
            options.Converters.Add(new BoolJsonConverter());
            options.Converters.Add(new BoolNullJsonConverter());
            options.Converters.Add(new ByteJsonConverter());
            options.Converters.Add(new ByteNullJsonConverter());
            options.Converters.Add(new DatetimeJsonConverter());
            options.Converters.Add(new DatetimeNullJsonConverter());
            options.Converters.Add(new DateTimeOffsetJsonConverter());
            options.Converters.Add(new DateTimeOffsetNullJsonConverter());
            options.Converters.Add(new DecimalJsonConverter());
            options.Converters.Add(new DecimalNullJsonConverter());
            options.Converters.Add(new DoubleJsonConverter());
            options.Converters.Add(new DoubleNullJsonConverter());
            options.Converters.Add(new GuidJsonConverter());
            options.Converters.Add(new GuidNullJsonConverter());
            options.Converters.Add(new Int16JsonConverter());
            options.Converters.Add(new Int16NullJsonConverter());
            options.Converters.Add(new Int32JsonConverter());
            options.Converters.Add(new Int32NullJsonConverter());
            options.Converters.Add(new Int64JsonConverter());
            options.Converters.Add(new Int64NullJsonConverter());
            options.Converters.Add(new SByteJsonConverter());
            options.Converters.Add(new SByteNullJsonConverter());
            options.Converters.Add(new SingleJsonConverter());
            options.Converters.Add(new SingleNullJsonConverter());
            options.Converters.Add(new UInt16JsonConverter());
            options.Converters.Add(new UInt16NullJsonConverter());
            options.Converters.Add(new UInt32JsonConverter());
            options.Converters.Add(new UInt32NullJsonConverter());
            options.Converters.Add(new UInt64JsonConverter());
            options.Converters.Add(new UInt64NullJsonConverter());
        }


        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json)
        {
            //JsonSerializer serializer = new JsonSerializer();
            //StringReader sr = new StringReader(json);
            //object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            //T t = (T)o;
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            else
            {
                T t = JsonSerializer.Deserialize<T>(json, Option);
                return t;
            }
        }

        /// <summary>
        /// 转换为json
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ObjectToJson(this object o)
        {
            //string json = JsonConvert.SerializeObject(o);
            if (o == null)
            {
                return string.Empty;
            }
            else
            {
                string json = JsonSerializer.Serialize(o, Option);
                return json;
            }
        }
    }
}
