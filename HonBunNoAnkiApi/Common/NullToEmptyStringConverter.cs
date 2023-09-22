using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace HonbunNoAnkiApi.Common
{
    public class NullToEmptyStringConverter : JsonConverter<string>
    {
        // Override default null handling
        public override bool HandleNull => true;

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.TokenType == JsonTokenType.Null ? "" : reader.GetString();

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value ?? "");
    }
}
