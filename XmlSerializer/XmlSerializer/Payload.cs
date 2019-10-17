using System;

namespace XmlSerializerPoC
{
    public class Payload : FormattedXmlObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public double Score { get; set; }
    }
}
