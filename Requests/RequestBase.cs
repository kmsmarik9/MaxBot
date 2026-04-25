using KmsDev.MaxBot.Full.Responses;
using System.Net.Http.Json;

namespace KmsDev.MaxBot.Full.Requests
{
    public abstract class RequestBase<TResponse> : IMaxBotRequest<TResponse>
        where TResponse: IMaxBotResponse
    {
        //через Metalama для каждого наследника будет сгенерирован свой override метод
        public virtual string GetRequestName()
        {
            throw new NotImplementedException();
        }

        public abstract MaxBotRequestHttpSettings GetRequestHttpSettings();

        public virtual HttpContent? GetRequestHttpContent()
        {
            return JsonContent.Create
            (
                inputValue: this,
                inputType: GetType(),
                options: MaxBotConstantsInternal.RequestJsonSerializerOptions,
                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            );
        }
    }
}