using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class GetSubscriptionsResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("subscriptions")]
        public List<ApiInputSubscription> Subscriptions { get; init; } = [];
    }
}
