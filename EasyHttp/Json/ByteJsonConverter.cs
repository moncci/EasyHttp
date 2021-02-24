using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    public class ByteJsonConverter : JsonConverter<byte>
    {
        public override byte Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (byte.TryParse(reader.GetString(), out byte value))
                    return value;
            }

            return reader.GetByte();
        }

        public override void Write(Utf8JsonWriter writer, byte value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
