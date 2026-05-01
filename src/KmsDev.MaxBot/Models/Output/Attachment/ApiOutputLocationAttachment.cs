using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiOutputLocationAttachment : ApiOutputAttachmentBase
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
