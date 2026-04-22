using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiOutputStickerAttachment : ApiOutputAttachmentBase
    {
        /// <summary>
        /// Код стикера
        /// </summary>
        [JsonIgnore]
        public string Code
        {
            get { return Payload.Code; }
            set { Payload.Code = value; }
        }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
        }
    }
}
