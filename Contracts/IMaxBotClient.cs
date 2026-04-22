using KmsDev.MaxBot.Full.Models;
using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;

namespace KmsDev.MaxBot.Full
{
    public interface IMaxBotClient
    {
        ulong BotHash { get; }

        CancellationToken SelfCancellationToken { get; }

        MaxBotClientApiContainer Api { get; }

        (bool IsValid, MaxBotWebappUserDataModel? User, string? Error) ValidateWebappInitData(string initData);

        Task<TResponse> SendRequestAsync<TResponse, TResilienceSettings>(IMaxBotRequest<TResponse> request, TResilienceSettings resilienceSettings, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse
            where TResilienceSettings: IMaxBotRequestResilienceSettings;
    }
}
