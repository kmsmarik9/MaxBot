using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputSubscription
    {
        /// <summary>
        /// URL вебхука
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; init; }

        /// <summary>
        /// Unix-время, когда была создана подписка
        /// </summary>
        [JsonPropertyName("time")]
        public DateTimeOffset Time { get; init; }

        [JsonPropertyName("update_types")]
        public List<MaxBotUpdateType> UpdateTypes { get; init; } = [];
    }
}
