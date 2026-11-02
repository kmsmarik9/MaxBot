using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetUpdatesRequest : RequestBase<GetUpdatesResponse>
    {
        /// <summary>
        /// Максимальное количество обновлений для получения (1-1000). По умолчанию 100.
        /// </summary>
        [JsonIgnore]
        public int? Limit { get; set; } = 100;

        /// <summary>
        /// Таймаут long polling в секундах (0-90). Если не задан, сервер использует 30.
        /// Не заменяет таймаут HttpClient.
        /// </summary>
        [JsonIgnore]
        public int? Timeout { get; set; }

        /// <summary>
        /// Если передан, бот получит обновления, которые еще не были получены. Если не передан, получит все новые обновления
        /// </summary>
        [JsonIgnore]
        public long? Marker { get; set; }

        /// <summary>
        /// Список типов обновлений, которые бот хочет получить (например, message_created, message_callback).
        /// В query уходит одной строкой через запятую.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<MaxBotUpdateType>? Types { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = MaxBotRequestUrl.WithQuery
                (
                    "updates",
                    new()
                    {
                        ["limit"] = MaxBotRequestUrl.Number(Limit),
                        ["timeout"] = MaxBotRequestUrl.Number(Timeout),
                        ["marker"] = MaxBotRequestUrl.Number(Marker),
                        ["types"] = Types == null
                            ? null
                            : string.Join(',', Types.Select(EnumByStringConverter<MaxBotUpdateType>.ToWire))
                    }
                )
            };
        }
    }
}
