using OneOf;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiOutputContactAttachment : ApiOutputAttachmentBase
    {
        /// <summary>
        /// Можно задать одно из:
        /// <br/>
        /// 1. ID контакта
        /// <br/>
        /// 2. username контакта
        /// </summary>
        [JsonIgnore]
        public OneOf<string, long>? Contact
        {
            get
            {
                if (Payload.ContactId.HasValue)
                {
                    return Payload.ContactId.Value;
                }

                if (!string.IsNullOrEmpty(Payload.ContactName))
                {
                    return Payload.ContactName;
                }

                return null;
            }

            set
            {
                Payload.ContactName = null;
                Payload.ContactId = null;

                if (value.HasValue)
                {
                    value.Value.Switch
                    (
                        contactName => Payload.ContactName = contactName,
                        contactId => Payload.ContactId = contactId
                    );
                }
            }
        }


        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("name")]
            public string? ContactName { get; set; }

            [JsonPropertyName("contact_id")]
            public long? ContactId { get; set; }
        }
    }
}
