using System.Xml.Serialization;

namespace KmsDev.MaxBot.Responses
{
    [XmlRoot("retval")]
    public class UploadAudioResponse : IMaxBotXmlResponse
    {
        [XmlText]
        public int Retval { get; set; }
    }
}
