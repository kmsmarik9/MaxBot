using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiChatAdmin
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("permissions")]
        public List<MaxBotChatAdminPermission> Permissions { get; set; } = [];

        [JsonPropertyName("alias")]
        public string? Alias { get; set; }
    }
}
