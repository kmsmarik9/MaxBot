using KmsDev.MaxBot.Full.Models;

namespace KmsDev.MaxBot.Full
{
    public static class MaxBotMessageRequestExtensions
    {
        public static TRequest WithInlineKeyboardAttachment<TRequest>(this TRequest request, params ApiOutputInlineKeyboardButtonBase[][] buttons)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                new ApiOutputInlineKeyboardAttachment
                {
                    Buttons = buttons
                }
            ];

            return request;
        }

        public static TRequest WithLocationAttachment<TRequest>(this TRequest request, ApiOutputLocationAttachment locationAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                locationAttachment
            ];

            return request;
        }

        public static TRequest WithShareAttachment<TRequest>(this TRequest request, ApiOutputShareAttachment shareAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                shareAttachment
            ];

            return request;
        }

        public static TRequest WithContactAttachment<TRequest>(this TRequest request, ApiOutputContactAttachment contactAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                contactAttachment
            ];

            return request;
        }

        public static TRequest WithStickerAttachment<TRequest>(this TRequest request, ApiOutputStickerAttachment stickerAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                stickerAttachment
            ];

            return request;
        }

        public static TRequest WithFileAttachment<TRequest>(this TRequest request, ApiOutputFileAttachment fileAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                fileAttachment
            ];

            return request;
        }

        public static TRequest WithAudioAttachment<TRequest>(this TRequest request, ApiOutputAudioAttachment audioAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                audioAttachment
            ];

            return request;
        }

        public static TRequest WithVideoAttachment<TRequest>(this TRequest request, ApiOutputVideoAttachment videoAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                videoAttachment
            ];

            return request;
        }

        public static TRequest WithImageAttachment<TRequest>(this TRequest request, ApiOutputImageAttachment imageAttachment)
            where TRequest : IMaxBotMessageRequestWithAttachments
        {
            request.Attachments =
            [
                imageAttachment
            ];

            return request;
        }
    }
}
