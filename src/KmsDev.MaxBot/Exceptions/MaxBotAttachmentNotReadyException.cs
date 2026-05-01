namespace KmsDev.MaxBot.Exceptions
{
    public class MaxBotAttachmentNotReadyException : MaxBotRequestException
    {
        public MaxBotAttachmentNotReadyException(string? message = null) : base(message)
        {
        }
    }
}
