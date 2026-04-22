using System.Text.Json;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full
{
    internal class UnixDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        internal static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var milliseconds = reader.GetInt64();
            if (milliseconds > 0)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
                //return _unixEpoch.AddMilliseconds(milliseconds);
            }

            if (milliseconds == 0)
            {
                return _unixEpoch;
            }

            throw new JsonException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {typeToConvert}.");
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

                //long milliseconds = (long)(value.ToUniversalTime() - _unixEpoch).TotalMilliseconds;
                //if (milliseconds >= 0)
                //{
                //    writer.WriteNumberValue(milliseconds);
                //}
                
                //throw new JsonException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            }
        }
    }
}
