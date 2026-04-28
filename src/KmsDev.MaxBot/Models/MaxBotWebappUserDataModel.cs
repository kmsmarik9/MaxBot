using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class MaxBotWebappUserDataModel
    {
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public required string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        public string? UserName { get; set; }

        [JsonPropertyName("photo_url")]
        public string? PhotoUrl { get; set; }
    }
}
