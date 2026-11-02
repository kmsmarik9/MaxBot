using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class SendCommentResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("message")]
        public required ApiInputCommentMessage Message { get; init; }
    }
}
