using System.Text.Json;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    internal class UnixDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var milliseconds = reader.GetInt64();
            if (milliseconds > 0)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
            }

            if (milliseconds == 0)
            {
                return DateTimeOffset.UnixEpoch;
            }

            throw new JsonException($"UnixDateTimeOffsetConverter Cannot convert value to {typeToConvert}.");
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            if (value == default)
            {
                writer.WriteNumberValue(0L);
            }
            else
            {
                writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
            }
        }
    }

    internal class NullableUnixDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
    {
        private static readonly UnixDateTimeOffsetConverter _inner = new();

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return _inner.Read(ref reader, typeof(DateTimeOffset), options);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            _inner.Write(writer, value.Value, options);
        }
    }
}
