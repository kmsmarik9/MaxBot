using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;

namespace KmsDev.MaxBot
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
