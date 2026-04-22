namespace KmsDev.MaxBot.Full.Exceptions
{
    public class MaxBotRequestException : Exception
    {
        public MaxBotRequestException(string? message): base(message)
        {

        }

        public MaxBotRequestException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
