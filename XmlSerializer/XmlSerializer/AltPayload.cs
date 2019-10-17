using System.Xml.Serialization;

namespace XmlSerializerPoC
{
    [XmlRoot("AltPayload")]
    public class AltPayload
    {
        [XmlElement]
        public string Label { get; set; }
        [XmlElement]
        public XmlFloatingPoint Xfp { get; set; }
    }
}
