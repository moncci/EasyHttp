using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    internal class UInt16JsonConverter : JsonConverter<UInt16>
    {
        public override UInt16 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (UInt16.TryParse(reader.GetString(), out UInt16 value))
                    return value;
            }

            return reader.GetUInt16();
        }

        public override void Write(Utf8JsonWriter writer, UInt16 value, JsonSerializerOptions options)
        {
            //writer.WriteStringValue(value.ToString());
            writer.WriteNumberValue(value);
        }
    }
}
