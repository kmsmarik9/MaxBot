using KmsDev.MaxBot.Models;

namespace KmsDev.MaxBot
{ 
    public interface IMaxBotMessageRequestWithAttachments
    {
        public List<ApiOutputAttachmentBase>? Attachments { get; set; }
    }
}
