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
    public class DoubleJsonConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                double value = JToolConvert.GetNoNullDouble(reader.GetString());
                return value;
            }

            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            //writer.WriteStringValue(value.ToString());
            writer.WriteNumberValue(value);
        }
    }
}
