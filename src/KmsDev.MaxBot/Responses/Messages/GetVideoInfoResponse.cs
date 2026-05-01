using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class GetVideoInfoResponse : IMaxBotJsonResponse
    {
        /// <summary>
        /// URL-ы для скачивания или воспроизведения видео. Может быть null, если видео недоступно
        /// </summary>
        [JsonPropertyName("urls")]
        public UrlData? Urls { get; init; }

        /// <summary>
        /// Миниатюра видео
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public ThumbnailData? Thumbnails { get; init; }

        /// <summary>
        /// Ширина видео
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; init; }

        /// <summary>
        /// Высота видео
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; init; }

        /// <summary>
        /// Длина видео в секундах
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; init; }

        public class UrlData
        {
            /// <summary>
            /// URL видео в разрешении 1080p, если доступно
            /// </summary>
            [JsonPropertyName("mp4_1080")]
            public string? Mp4_1080 { get; init; }

            /// <summary>
            /// URL видео в разрешении 720p, если доступно
            /// </summary>
            [JsonPropertyName("mp4_720")]
            public string? Mp4_720 { get; init; }

            /// <summary>
            /// URL видео в разрешении 480p, если доступно
            /// </summary>
            [JsonPropertyName("mp4_480")]
            public string? Mp4_480 { get; init; }

            /// <summary>
            /// URL видео в разрешении 360p, если доступно
            /// </summary>
            [JsonPropertyName("mp4_360")]
            public string? Mp4_360 { get; init; }

            /// <summary>
            /// URL видео в разрешении 240p, если доступно
            /// </summary>
            [JsonPropertyName("mp4_240")]
            public string? Mp4_240 { get; init; }

            /// <summary>
            /// URL видео в разрешении 144p, если доступно
            /// </summary>
            [JsonPropertyName("mp4_144")]
            public string? Mp4_144 { get; init; }

            /// <summary>
            /// URL трансляции, если доступна
            /// </summary>
            [JsonPropertyName("hls")]
            public string? Hls { get; init; }
        }

        public class ThumbnailData
        {
            /// <summary>
            /// Уникальный ID этого изображения
            /// </summary>
            [JsonPropertyName("photo_id")]
            public long PhotoId { get; init; }

            [JsonPropertyName("token")]
            public string Token { get; init; }

            /// <summary>
            /// URL изображения
            /// </summary>
            [JsonPropertyName("url")]
            public string Url { get; init; }
        }
    }
}
