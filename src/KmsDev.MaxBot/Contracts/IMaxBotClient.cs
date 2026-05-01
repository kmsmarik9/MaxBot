using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;

namespace KmsDev.MaxBot
{
    public interface IMaxBotClient
    {
        string BotHash { get; }

        CancellationToken SelfCancellationToken { get; }

        MaxBotClientApiContainer Api { get; }

        (bool IsValid, MaxBotWebappUserDataModel? User, string? Error) ValidateWebappInitData(string initData);

        Task<TResponse> SendRequestAsync<TResponse, TResilienceSettings>(IMaxBotRequest<TResponse> request, TResilienceSettings resilienceSettings, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse
            where TResilienceSettings: IMaxBotRequestResilienceSettings;
    }
}
