using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class GetCommentsResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("messages")]
        public required List<ApiInputCommentMessage> Messages { get; init; }
    }
}
