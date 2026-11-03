using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputInlineKeyboardAttachment
    {
        [JsonIgnore]
        public List<List<ApiInputInlineKeyboardButtonPolymorphContainer>> Buttons => Payload?.Buttons ?? [];

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData? Payload { get; init; }

        internal class PayloadData
        {
            [JsonPropertyName("buttons")]
            public List<List<ApiInputInlineKeyboardButtonPolymorphContainer>> Buttons { get; init; } = [];
        }
    }
}
