using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    public class EnumByStringConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private static readonly Dictionary<T, string> _enumToString = [];
        private static readonly Dictionary<string, T> _stringToEnum = new(StringComparer.OrdinalIgnoreCase);

        static EnumByStringConverter()
        {
            var type = typeof(T);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.GetValue(null) is T enumValue)
                {
                    var attr = field.GetCustomAttribute<EnumValueByStringAttribute>();
                    string stringRepr = attr?.StringValue ?? enumValue.ToString();

                    _enumToString[enumValue] = stringRepr;
                    _stringToEnum[stringRepr] = enumValue;
                }
            }
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //if (reader.TokenType != JsonTokenType.String)
            //    throw new JsonException($"Ожидалась строка, получено {reader.TokenType}");

            var stringValue = reader.GetString() ?? string.Empty;
            if (_stringToEnum?.TryGetValue(stringValue, out var enumValue) ?? false)
            {
                return enumValue;
            }

            throw new JsonException($"Unknown value '{stringValue}' for enum {typeof(T).Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (_enumToString.TryGetValue(value, out var stringValue))
            {
                writer.WriteStringValue(stringValue);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
