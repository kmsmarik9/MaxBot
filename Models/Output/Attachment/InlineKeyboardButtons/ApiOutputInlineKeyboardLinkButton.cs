using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiOutputInlineKeyboardLinkButton : ApiOutputInlineKeyboardButtonBase
    {
        /// <summary>
        /// Ссылка. до 2048 символов
        /// </summary>
        [JsonPropertyName("url")]
        public string Link { get; set; }
    }
}
