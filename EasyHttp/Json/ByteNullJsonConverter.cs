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
    public class ByteNullJsonConverter : JsonConverter<byte?>
    {
        public override byte? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrWhiteSpace(reader.GetString()))
                {
                    return null;
                }
                else
                {
                    return byte.Parse(reader.GetString());
                }
            }
            return reader.GetByte();
        }

        public override void Write(Utf8JsonWriter writer, byte? value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                writer.WriteStringValue(EhConvert.GetNoNullString(value));
            }
        }
    }
}
