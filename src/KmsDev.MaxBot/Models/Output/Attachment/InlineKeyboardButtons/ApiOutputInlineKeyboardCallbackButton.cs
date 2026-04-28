using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiOutputInlineKeyboardCallbackButton : ApiOutputInlineKeyboardButtonBase
    {
        /// <summary>
        ///  Токен кнопки. до 1024 символов
        /// </summary>
        [JsonPropertyName("payload")]
        public string Payload { get; set; }
    }
}
