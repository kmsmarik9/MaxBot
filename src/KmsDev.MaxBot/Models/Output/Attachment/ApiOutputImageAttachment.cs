using OneOf;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiOutputImageAttachment : ApiOutputAttachmentBase
    {
        /// <summary>
        /// Можно задать одно из:
        /// <br/>
        /// 1. Любой внешний URL изображения, которое вы хотите прикрепить
        /// <br/>
        /// 2. Токен существующего вложения
        /// <br/>
        /// 3. Токены существующих вложений
        /// </summary>
        [JsonIgnore]
        public OneOf<UrlSource, TokenSource, TokensSource>? Source
        {
            get
            {
                if (!string.IsNullOrEmpty(Payload.Url))
                {
                    return new UrlSource(Payload.Url);
                }

                if (!string.IsNullOrEmpty(Payload.Token))
                {
                    return new TokenSource(Payload.Token);
                }

                if(Payload.Photos?.Length > 0)
                {
                    return new TokensSource(Payload.Photos);
                }

                 return null;
            }

            set
            {
                Payload.Url = null;
                Payload.Token = null;
                Payload.Photos = null;

                if (value.HasValue)
                {
                    value.Value.Switch
                    (
                        urlSource => Payload.Url = urlSource.value,
                        tokenSource => Payload.Token = tokenSource.value,
                        tokensSource => Payload.Photos = tokensSource.value
                    );
                }
            }
        }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("url")]
            public string? Url { get; set; }

            [JsonPropertyName("token")]
            public string? Token { get; set; }

            [JsonPropertyName("photos")]
            public string[]? Photos { get; set; }
        }

        public record UrlSource(string value);
        public record TokenSource(string value);
        public record TokensSource(string[] value);
    }
}
