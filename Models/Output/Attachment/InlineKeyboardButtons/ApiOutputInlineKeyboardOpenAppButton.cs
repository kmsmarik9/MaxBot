using OneOf;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiOutputInlineKeyboardOpenAppButton : ApiOutputInlineKeyboardButtonBase
    {
        /// <summary>
        /// Можно задать одно из:
        /// <br/>
        /// 1. Публичное имя (username без @) бота или ссылка на него
        /// <br/>
        /// 2. Идентификатор бота
        /// </summary>
        [JsonIgnore]
        public OneOf<string, long>? BotTarget
        {
            get
            {
                if (BotId.HasValue)
                {
                    return BotId.Value;
                }

                if (!string.IsNullOrEmpty(BotName))
                {
                    return BotName!;
                }

                return null;
            }

            set
            {
                BotName = null;
                BotId = null;

                if (value.HasValue)
                {
                    value.Value.Switch
                    (
                        botName => BotName = botName,
                        botId => BotId = botId
                    );
                }
            }
        }

        /// <summary>
        /// Публичное имя (username) бота или ссылка на него, чьё мини-приложение надо запустить
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("web_app")]
        internal string? BotName { get; set; }

        /// <summary>
        /// Идентификатор бота, чьё мини-приложение надо запустить
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("contact_id")]
        internal long? BotId { get; set; }

        /// <summary>
        /// Параметр запуска, который будет передан в initData мини-приложения
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }
    }
}
