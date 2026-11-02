using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiFailedUserDetails
    {
        /// <summary>
        /// add.participant.privacy или add.participant.not.found
        /// </summary>
        [JsonPropertyName("error_code")]
        public required string ErrorCode { get; init; }

        [JsonPropertyName("user_ids")]
        public required List<long> UserIds { get; init; }
    }
}
