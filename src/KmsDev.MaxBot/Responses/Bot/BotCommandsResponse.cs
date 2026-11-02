using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class BotCommandsResponse : IMaxBotJsonResponse
    {
        /// <summary>
        /// Команды бота. Не больше 32.
        /// </summary>
        [JsonPropertyName("commands")]
        public List<ApiBotCommand>? Commands { get; init; }
    }
}
