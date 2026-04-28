using KmsDev.MaxBot.Responses;
using System.Text.Json;

namespace KmsDev.MaxBot
{
    internal sealed class MaxBotJsonResponseParser : IMaxBotResponseParser
    {
        public async Task<TResponse> ParseAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse
        {
            var text = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<TResponse>(text, MaxBotConstantsInternal.ResponseJsonSerializerOptions)!;
        }
    }
}
