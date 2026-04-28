using KmsDev.MaxBot.Models;

namespace KmsDev.MaxBot.Responses
{
    public class SendMessageResponse : IMaxBotJsonResponse
    {
        public ApiInputMessage Message { get; set; }
    }
}
