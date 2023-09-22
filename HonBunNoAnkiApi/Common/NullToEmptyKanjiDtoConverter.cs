using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.KanjiDtos;

namespace HonbunNoAnkiApi.Common
{
    public class NullToEmptyKanjiDtoConverter : JsonConverter<KanjiDto>
    {
        // Override default null handling
        public override bool HandleNull => true;

        public override KanjiDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            var kanjiDto = new KanjiDto();

            return kanjiDto;
        }


        public override void Write(Utf8JsonWriter writer, KanjiDto value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            //writer.WriteRawValue(value.ToString());
            //writer.WriteStringValue(value ?? "");
        }

    }
}
