using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiOutputFileAttachment : ApiOutputAttachmentBase
    {
        /// <summary>
        /// Токен — уникальный ID загруженного медиафайла
        /// </summary>
        [JsonIgnore]
        public string Token
        {
            get { return Payload.Token; }
            set { Payload.Token = value; }
        }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }
    }
}
