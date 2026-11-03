using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputImage
    {
        [JsonPropertyName("url")]
        public required string Url { get; init; }
    }
}
