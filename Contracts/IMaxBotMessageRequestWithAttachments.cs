using KmsDev.MaxBot.Full.Models;

namespace KmsDev.MaxBot.Full
{ 
    public interface IMaxBotMessageRequestWithAttachments
    {
        public List<ApiOutputAttachmentBase>? Attachments { get; set; }
    }
}
