using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class UploadFileResponse : IMaxBotJsonResponse
    {
        //[JsonPropertyName("fileId")]
        //public long FileId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
