using EasyHttp.Tool;
using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    public class GuidNullJsonConverter : JsonConverter<Guid?>
    {
        public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrWhiteSpace(reader.GetString()))
                {
                    return null;
                }
                else
                {
                    if (Guid.TryParse(reader.GetString(), out Guid value))
                        return value;
                }
            }

            return reader.GetGuid();
        }

        public override void Write(Utf8JsonWriter writer, Guid? value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                writer.WriteStringValue(JToolConvert.GetNoNullString(value));
            }
        }
    }
}
