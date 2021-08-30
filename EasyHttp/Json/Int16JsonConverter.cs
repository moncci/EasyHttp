using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    internal class Int16JsonConverter : JsonConverter<Int16>
    {
        public override Int16 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (Int16.TryParse(reader.GetString(), out Int16 value))
                    return value;
            }

            return reader.GetInt16();
        }

        public override void Write(Utf8JsonWriter writer, Int16 value, JsonSerializerOptions options)
        {
            //writer.WriteStringValue(value.ToString());
            writer.WriteNumberValue(value);
        }
    }
}
