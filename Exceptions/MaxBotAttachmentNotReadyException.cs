namespace KmsDev.MaxBot.Full.Exceptions
{
    public class MaxBotAttachmentNotReadyException : MaxBotRequestException
    {
        public MaxBotAttachmentNotReadyException(string? message = null) : base(message)
        {
        }
    }
}
