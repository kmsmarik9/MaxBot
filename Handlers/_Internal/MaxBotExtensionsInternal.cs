using System.Text.Json;

namespace KmsDev.MaxBot.Full
{
    internal static class MaxBotExtensionsInternal
    {
        internal readonly static Lazy<JsonSerializerOptions> _jso = new(new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });

        internal static string SerializeToString<T>(this T value)
        {
            return JsonSerializer.Serialize(value, _jso.Value);
        }
    }
}
