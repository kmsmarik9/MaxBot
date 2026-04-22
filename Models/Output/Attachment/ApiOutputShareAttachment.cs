using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiOutputShareAttachment : ApiOutputAttachmentBase
    {
        [JsonIgnore]
        public string? Url
        {
            get { return Payload.Url; }
            set { Payload.Url = value; }
        }

        [JsonIgnore]
        public string? Token
        {
            get { return Payload?.Token; }
            set { Payload.Url = value; }
        }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        internal class PayloadData
        {
            /// <summary>
            /// URL, прикрепленный к сообщению в качестве предпросмотра медиа
            /// </summary>
            [JsonPropertyName("url")]
            public string? Url { get; set; }

            /// <summary>
            /// Токен вложения
            /// </summary>
            [JsonPropertyName("token")]
            public string? Token { get; set; }
        }
    }
}
