using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Responses
{
    public class UploadImageResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("photos")]
        public Dictionary<string, PhotoData> Photos { get; set; }

        public class PhotoData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }
    }
}
