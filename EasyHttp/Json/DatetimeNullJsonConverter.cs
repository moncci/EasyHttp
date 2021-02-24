using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    public class DatetimeNullJsonConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 获取或设置DateTime格式
        /// <para>默认为: yyyy-MM-dd HH:mm:ss</para>
        /// </summary>
        public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrWhiteSpace(reader.GetString()))
                {
                    return null;
                }
                else
                {
                    return DateTime.Parse(reader.GetString());
                }
            }

            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                //writer.WriteStringValue(value?.ToString(this.DateTimeFormat));
            }
            else
            {
                writer.WriteStringValue(value?.ToString(this.DateTimeFormat));
            }
        }
    }
}
