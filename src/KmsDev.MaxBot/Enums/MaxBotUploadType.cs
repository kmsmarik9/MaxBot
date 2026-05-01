using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Enums
{
    [JsonConverter(typeof(MaxBotUploadType))]
    public enum MaxBotUploadType
    {
        /// <summary>
        /// любые типы файлов
        /// </summary>
        [EnumValueByString("file")]
        File = 0,

        /// <summary>
        /// JPG, JPEG, PNG, GIF, TIFF, BMP, HEIC
        /// </summary>
        [EnumValueByString("image")]
        Image = 10,

        /// <summary>
        /// MP4, MOV, MKV, WEBM, MATROSKA
        /// </summary>
        [EnumValueByString("video")]
        Video = 20,

        /// <summary>
        /// MP3, WAV, M4A и другие
        /// </summary>
        [EnumValueByString("audio")]
        Audio = 30
    }
}
