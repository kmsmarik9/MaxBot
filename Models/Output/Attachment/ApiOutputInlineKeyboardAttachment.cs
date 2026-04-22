using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiOutputInlineKeyboardAttachment : ApiOutputAttachmentBase
    {
        [JsonIgnore]
        public ApiOutputInlineKeyboardButtonBase[][] Buttons
        {
            get { return Payload.Buttons; }
            set { Payload.Buttons = value; }
        }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("buttons")]
            public ApiOutputInlineKeyboardButtonBase[][] Buttons { get; set; } = [];
        }
    }
}
