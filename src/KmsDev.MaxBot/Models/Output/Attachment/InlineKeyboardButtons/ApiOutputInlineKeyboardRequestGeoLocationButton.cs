using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiOutputInlineKeyboardRequestGeoLocationButton : ApiOutputInlineKeyboardButtonBase
    {
        /// <summary>
        /// Если true, отправляет местоположение без запроса подтверждения пользователя
        /// </summary>
        [JsonPropertyName("quick")]
        public bool Quick { get; set; } = false;
    }
}
