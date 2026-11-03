using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    [JsonConverter(typeof(MaxBotPolymorphContainerDeserializeJsonConverter<ApiInputMarkupElementPolymorphContainer>))]
    [MaxBotSourceGeneratePolymorphContainer("type")]
    public partial class ApiInputMarkupElementPolymorphContainer : IMaxBotPolymorphContainer
    {
        [MaxBotSourceGeneratePolymorphItem("strong")]
        public readonly ApiInputStrongMarkup? Strong;

        [MaxBotSourceGeneratePolymorphItem("emphasized")]
        public readonly ApiInputEmphasizedMarkup? Emphasized;

        [MaxBotSourceGeneratePolymorphItem("monospaced")]
        public readonly ApiInputMonospacedMarkup? Monospaced;

        [MaxBotSourceGeneratePolymorphItem("link")]
        public readonly ApiInputLinkMarkup? Link;

        [MaxBotSourceGeneratePolymorphItem("strikethrough")]
        public readonly ApiInputStrikethroughMarkup? Strikethrough;

        [MaxBotSourceGeneratePolymorphItem("underline")]
        public readonly ApiInputUnderlineMarkup? Underline;

        [MaxBotSourceGeneratePolymorphItem("heading")]
        public readonly ApiInputHeadingMarkup? Heading;

        [MaxBotSourceGeneratePolymorphItem("user_mention")]
        public readonly ApiInputUserMentionMarkup? UserMention;

        [MaxBotSourceGeneratePolymorphItem("highlighted")]
        public readonly ApiInputHighlightedMarkup? Highlighted;

        [MaxBotSourceGeneratePolymorphItem("quote")]
        public readonly ApiInputQuoteMarkup? Quote;

        public string? UnknowData { get; private set; }
    }
}
