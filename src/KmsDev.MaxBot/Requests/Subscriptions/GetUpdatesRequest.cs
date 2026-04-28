using KmsDev.MaxBot.Responses;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetUpdatesRequest : RequestBase<GetUpdatesResponse>
    {
        private static readonly MaxBotRequestHttpSettings _requestSettings = new()
        {
            Method = HttpMethod.Get,
            Url = "updates"
        };

        /// <summary>
        /// Максимальное количество обновлений для получения (1-1000). По умолчанию 100.
        /// </summary>
        public int? Limit { get; set; } = 100;


        /// <summary>
        /// Если передан, бот получит обновления, которые еще не были получены. Если не передан, получит все новые обновления
        /// </summary>
        public long? Marker { get; set; }

        /// <summary>
        /// Список типов обновлений, которые бот хочет получить (например, message_created, message_callback)
        /// </summary>
        //[JsonPropertyName("types")]
        //public IEnumerable<UpdateType>? Types { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return _requestSettings;
        }
    }
}