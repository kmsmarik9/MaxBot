using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class ModifyMembersResponse : SimpleQueryResultResponse
    {
        [JsonPropertyName("failed_user_ids")]
        public List<long>? FailedUserIds { get; init; }

        [JsonPropertyName("failed_user_details")]
        public List<ApiFailedUserDetails>? FailedUserDetails { get; init; }
    }
}
