using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputUserWithPhoto : ApiInputUser
    {
        /// <summary>
        /// до 16000 символов
        /// Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// URL аватара пользователя или бота в уменьшенном размере
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; init; }

        /// <summary>
        /// URL аватара пользователя или бота в полном размере
        /// </summary>
        [JsonPropertyName("full_avatar_url")]
        public string? FullAvatarUrl { get; init; }
    }
}
