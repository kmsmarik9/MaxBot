using KmsDev.MaxBot.Full.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Responses
{
    public class GetUpdatesResponse : IMaxBotJsonResponse
    {
        /// <summary>
        /// Страница обновлений
        /// </summary>
        [JsonPropertyName("updates")]
        public IReadOnlyList<ApiInputUpdateMessagePolymorphContainer> Updates { get; init; } = [];

        /// <summary>
        /// Указатель на следующую страницу данных
        /// </summary>
        [JsonPropertyName("marker")]
        public long? Marker { get; init; }
    }
}
