using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Responses
{
    public class GetUploadFileUrlResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
