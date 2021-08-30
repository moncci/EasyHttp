using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    internal class UInt64NullJsonConverter : JsonConverter<UInt64?>
    {
        public override UInt64? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrWhiteSpace(reader.GetString()))
                {
                    return null;
                }
                else
                {
                    if (UInt64.TryParse(reader.GetString(), out UInt64 value))
                        return value;
                }
            }

            return reader.GetUInt64();
        }

        public override void Write(Utf8JsonWriter writer, UInt64? value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                //writer.WriteStringValue(JToolConvert.GetNoNullString(value));
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
