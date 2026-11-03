using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    [JsonConverter(typeof(EnumByStringConverter<ApiAttachmentButtonType>))]
    public enum ApiAttachmentButtonType
    {
        //[EnumValueByString("callback")]
        //Callback = 10,

        //[EnumValueByString("link")]
        //Link = 20,

        [EnumValueByString("request_geo_location")]
        RequestGeoLocation = 30,

        [EnumValueByString("request_contact")]
        RequestContact = 40,

        [EnumValueByString("open_app")]
        OpenApp = 50,

        [EnumValueByString("message")]
        Message = 60,

        [EnumValueByString("clipboard")]
        Clipboard = 70
    }
}
