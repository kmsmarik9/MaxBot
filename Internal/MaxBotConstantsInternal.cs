using System.Text.Json;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full
{
    internal static class MaxBotConstantsInternal
    {
        public static readonly JsonSerializerOptions RequestJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            IncludeFields = true
        };

        public static readonly JsonSerializerOptions ResponseJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            IncludeFields = true
        };
    }
}
