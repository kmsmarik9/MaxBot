using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputMessageStat
    {
        [JsonPropertyName("views")]
        public required long Views { get; init; } = 0;
    }
}
