using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputMarkupElement
    {
        /// <summary>
        /// Позиция начала фрагмента в тексте
        /// </summary>
        [JsonPropertyName("from")]
        public int From { get; init; }

        /// <summary>
        /// Длина фрагмента
        /// </summary>
        [JsonPropertyName("length")]
        public int Length { get; init; }
    }

    public class ApiInputStrongMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputEmphasizedMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputMonospacedMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputStrikethroughMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputUnderlineMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputHeadingMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputHighlightedMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputQuoteMarkup : ApiInputMarkupElement
    {
    }

    public class ApiInputLinkMarkup : ApiInputMarkupElement
    {
        /// <summary>
        /// URL ссылки. От 1 до 2048 символов.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }
    }

    public class ApiInputUserMentionMarkup : ApiInputMarkupElement
    {
        [JsonPropertyName("user_link")]
        public string? UserLink { get; init; }

        [JsonPropertyName("user_id")]
        public long? UserId { get; init; }
    }
}
