﻿using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyHttp.Json
{
    internal class DecimalNullJsonConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrWhiteSpace(reader.GetString()))
                {
                    return null;
                }
                else
                {
                    if (decimal.TryParse(reader.GetString(), out decimal value))
                        return value;
                }
            }

            return reader.GetDecimal();
        }

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                //writer.WriteStringValue(JToolConvert.GetNoNullString(value));
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
