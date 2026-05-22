using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputLocationAttachment
    {
        /// <summary>
        /// Широта
        /// </summary>
        [JsonPropertyName("latitude")]
        public double Latitude { get; init; }

        /// <summary>
        /// Долгота
        /// </summary>
        [JsonPropertyName("longitude")]
        public double Longitude { get; init; }
    }
}
