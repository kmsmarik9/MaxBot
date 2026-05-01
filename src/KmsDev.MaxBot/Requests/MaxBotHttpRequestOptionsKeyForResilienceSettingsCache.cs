using Polly;
using System.Diagnostics.CodeAnalysis;

namespace KmsDev.MaxBot.Requests
{
    public static class MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<TSettings>
        where TSettings : IMaxBotRequestResilienceSettings
    {
        public readonly static HttpRequestOptionsKey<TSettings> OptionKey = new(nameof(TSettings));

        public static void SetSettings(HttpRequestOptions requestOptions, TSettings settings)
        {
            requestOptions.Set(OptionKey, settings);
        }

        public static bool TryGetFromContext(ResilienceContext resilienceContext, [MaybeNullWhen(false)] out TSettings settings)
        {
            var options = resilienceContext.GetRequestMessage()?.Options;
            if(options != null)
            {
                if(options.TryGetValue(OptionKey, out settings))
                {
                    return true;
                }
            }

            settings = default;
            return false;
        }
    }
}
