using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class ChatMembersListResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("members")]
        public required List<ApiInputChatMember> Members { get; init; }

        [JsonPropertyName("marker")]
        public long? Marker { get; init; }
    }
}
