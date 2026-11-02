using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class GetMeResponse : ApiInputUserWithPhoto, IMaxBotJsonResponse
    {
        /// <summary>
        /// Команды, которые поддерживает бот. Не больше 32.
        /// </summary>
        [JsonPropertyName("commands")]
        public List<ApiBotCommand>? Commands { get; init; }
    }
}
