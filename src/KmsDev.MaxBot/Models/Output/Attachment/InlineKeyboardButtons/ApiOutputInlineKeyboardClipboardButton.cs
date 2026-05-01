using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiOutputInlineKeyboardClipboardButton : ApiOutputInlineKeyboardButtonBase
    {
        /// <summary>
        /// Текст, который копируется в буфер обмена после нажатия на кнопку
        /// </summary>
        [JsonPropertyName("payload")]
        public string Payload { get; set; }
    }
}
