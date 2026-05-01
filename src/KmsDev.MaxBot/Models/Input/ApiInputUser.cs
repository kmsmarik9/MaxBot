using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputUser
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        [JsonPropertyName("user_id")]
        public required long UserId { get; init; }

        /// <summary>
        /// string - Отображаемое имя пользователя
        /// </summary>
        [JsonPropertyName("first_name")]
        public required string FirstName { get; init; }

        /// <summary>
        /// string [nullable] - Отображаемая фамилия пользователя
        /// </summary>
        [JsonPropertyName("last_name")]
        public string? LastName { get; init; }

        /// <summary>
        /// string - Уникальное публичное имя пользователя. Может быть null, если пользователь недоступен или имя не задано 
        /// </summary>
        [JsonPropertyName("username")]
        public string? UserName { get; init; }

        /// <summary>
        /// bool - true, если пользователь является ботом
        /// </summary>
        [JsonPropertyName("is_bot")]
        public required bool IsBot { get; init; }

        [JsonPropertyName("last_activity_time")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public required DateTimeOffset LastActivityTime { get; init; }
    }
}
