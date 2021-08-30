using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    internal class UInt32JsonConverter : JsonConverter<UInt32>
    {
        public override UInt32 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (UInt32.TryParse(reader.GetString(), out UInt32 value))
                    return value;
            }

            return reader.GetUInt32();
        }

        public override void Write(Utf8JsonWriter writer, UInt32 value, JsonSerializerOptions options)
        {
            //writer.WriteStringValue(value.ToString());
            writer.WriteNumberValue(value);
        }
    }
}
