using System.Xml.Serialization;

namespace KmsDev.MaxBot.Responses
{
    [XmlRoot("retval")]
    public class UploadVideoResponse : IMaxBotXmlResponse
    {
        [XmlText]
        public int Retval { get; set; }
    }
}
