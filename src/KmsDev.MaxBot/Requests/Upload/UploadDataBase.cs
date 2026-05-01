using KmsDev.MaxBot.Responses;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests.Upload
{
    public class UploadDataBase<TResponse> : RequestBase<TResponse>
        where TResponse : IMaxBotResponse
    {
        [JsonIgnore]
        public string UploadUrl { get; set; }

        [JsonIgnore]
        public string? FileName { get; set; }

        [JsonIgnore]
        public Stream Stream { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Post,
                Url = UploadUrl,
                IncludeAuthorizationToken = false
            };
        }

        public override HttpContent? GetRequestHttpContent()
        {
            var resultContent = new MultipartFormDataContent();

            var streamContent = new StreamContent(Stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream"); //new MediaTypeHeaderValue("multipart/form-data");

            resultContent.Add(streamContent, "data", FileName ?? Guid.NewGuid().ToString());

            return resultContent;
        }
    }
}
