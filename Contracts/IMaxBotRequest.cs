using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;

namespace KmsDev.MaxBot.Full
{
    [GetRequestNameForInherit]
    public interface IMaxBotRequest
    {
        MaxBotRequestHttpSettings GetRequestHttpSettings();
        HttpContent? GetRequestHttpContent();
        string GetRequestName();
    }

    public interface IMaxBotRequest<TResponse> : IMaxBotRequest
        where TResponse: IMaxBotResponse
    {
        
    }
}
