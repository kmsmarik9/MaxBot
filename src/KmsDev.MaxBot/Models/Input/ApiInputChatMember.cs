using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputChatMember : ApiInputUserWithPhoto
    {
        [JsonPropertyName("last_access_time")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset LastAccessTime { get; init; }

        [JsonPropertyName("is_owner")]
        public bool IsOwner { get; init; }

        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; init; }

        [JsonPropertyName("join_time")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset JoinTime { get; init; }

        /// <summary>
        /// Права администратора. null, если участник не администратор.
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<MaxBotChatAdminPermission>? Permissions { get; init; }

        [JsonPropertyName("alias")]
        public string? Alias { get; init; }
    }
}
