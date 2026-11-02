using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    /// <summary>
    /// Команда бота. Имя от 1 до 64 символов, описание до 128.
    /// </summary>
    public class ApiBotCommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Описание команды. Может отсутствовать.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
