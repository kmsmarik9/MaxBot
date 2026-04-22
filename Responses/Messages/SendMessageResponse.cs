using KmsDev.MaxBot.Full.Models;

namespace KmsDev.MaxBot.Full.Responses
{
    public class SendMessageResponse : IMaxBotJsonResponse
    {
        public ApiInputMessage Message { get; set; }
    }
}
