using KmsDev.MaxBot.Full.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Responses
{
    public class GetSubscriptionsResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("subscriptions")]
        public List<ApiInputSubscription> Subscriptions { get; init; } = [];
    }
}
