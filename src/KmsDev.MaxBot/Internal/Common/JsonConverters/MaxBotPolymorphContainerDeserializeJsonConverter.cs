using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    public class MaxBotPolymorphContainerDeserializeJsonConverter<T> : JsonConverter<T>
        where T : new()
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if (!root.TryGetProperty(MaxBotPolymorphContainerJsonConverterCache<T>.DiscriminatorField, out var discriminatorProp))
                {
                    throw new JsonException($"Missing discriminator field '{MaxBotPolymorphContainerJsonConverterCache<T>.DiscriminatorField}'");
                }

                var discriminator = discriminatorProp.GetString()?.ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(discriminator) || !MaxBotPolymorphContainerJsonConverterCache<T>.Map.TryGetValue(discriminator, out var field))
                {
                    throw new JsonException($"Unknown discriminator value '{discriminator}'");
                }

                var value = root.Deserialize(field.FieldType, options);

                var result = new T();

                field.SetValue(result, value);
                
                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal static class MaxBotPolymorphContainerJsonConverterCache<T>
    {
        public static readonly string DiscriminatorField;
        public static readonly Dictionary<string, FieldInfo> Map;

        static MaxBotPolymorphContainerJsonConverterCache()
        {
            var type = typeof(T);

            var containerAttr = type.GetCustomAttribute<MaxBotSourceGeneratePolymorphContainerAttribute>()
                ?? throw new InvalidOperationException($"No container attribute on {type.Name}");

            DiscriminatorField = containerAttr.DiscriminatorFieldName;

            Map = type
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Select(f => new
                {
                    Field = f,
                    Attr = f.GetCustomAttribute<MaxBotSourceGeneratePolymorphItemAttribute>()
                })
                .Where(x => x.Attr != null)
                .ToDictionary(
                    x => x.Attr!.DiscriminatorFieldValue,
                    x => x.Field
                );
        }
    }
}
