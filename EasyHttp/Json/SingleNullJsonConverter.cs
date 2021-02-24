using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    public class SingleNullJsonConverter : JsonConverter<Single?>
    {
        public override Single? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrWhiteSpace(reader.GetString()))
                {
                    return null;
                }
                else
                {
                    if (Single.TryParse(reader.GetString(), out Single value))
                        return value;
                }
            }

            return reader.GetSingle();
        }

        public override void Write(Utf8JsonWriter writer, Single? value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                //writer.WriteStringValue(JToolConvert.GetNoNullString(value));
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
