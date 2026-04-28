using KmsDev.MaxBot.Responses;
using System.Xml.Serialization;

namespace KmsDev.MaxBot
{
    internal sealed class MaxBotXmlResponseParser : IMaxBotResponseParser
    {
        public async Task<TResponse> ParseAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse
        {
            var text = await response.Content.ReadAsStringAsync(cancellationToken);

            var serializer = new XmlSerializer(typeof(TResponse));

            using var reader = new StringReader(text);
            return (TResponse)serializer.Deserialize(reader)!;
        }
    }
}
