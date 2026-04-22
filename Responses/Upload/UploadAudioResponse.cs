using System.Xml.Serialization;

namespace KmsDev.MaxBot.Full.Responses
{
    [XmlRoot("retval")]
    public class UploadAudioResponse : IMaxBotXmlResponse
    {
        [XmlText]
        public int Retval { get; set; }
    }
}
