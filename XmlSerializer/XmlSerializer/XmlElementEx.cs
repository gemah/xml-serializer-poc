using System.Xml.Serialization;

namespace XmlSerializerPoC
{
    internal class XmlElementAttributeEx : XmlElementAttribute
    {
        public XmlElementAttributeEx(string elementName, string format) : base(elementName)
        {
            Format = format;
        }

        public string Format { get; }
    }
}
