using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    internal class SByteJsonConverter : JsonConverter<SByte>
    {
        public override SByte Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (SByte.TryParse(reader.GetString(), out SByte value))
                    return value;
            }

            return reader.GetSByte();
        }

        public override void Write(Utf8JsonWriter writer, SByte value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
