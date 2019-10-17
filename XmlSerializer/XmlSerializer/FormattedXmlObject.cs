using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XmlSerializerPoC
{
    public abstract class FormattedXmlObject : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                List<PropertyInfo> properties = GetType().GetProperties().ToList();
                foreach (PropertyInfo property in properties)
                {
                    Type propertyType = property.PropertyType;
                    object value;

                    if (property.Name == reader.Name && propertyType == typeof(Guid))
                    {
                        value = new Guid(reader.ReadElementContentAsString());
                    }
                    else
                    {
                        value = reader.ReadElementContentAs(propertyType, null);
                    }

                    property.SetValue(this, value);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            IList<PropertyInfo> properties = GetType().GetProperties().ToList();

            foreach (PropertyInfo property in properties)
            {
                XmlElementAttribute xmlElementAttribute = property.GetCustomAttribute<XmlElementAttribute>();
                string elementName = xmlElementAttribute != null ? xmlElementAttribute.ElementName : property.Name;

                if (property.PropertyType == typeof(double))
                {
                    string format = (xmlElementAttribute != null && xmlElementAttribute is XmlElementAttributeEx) ? ((XmlElementAttributeEx)xmlElementAttribute).Format : "N2";
                    double value = Convert.ToDouble(property.GetValue(this, null));
                    writer.WriteElementString(elementName, value.ToString(format, CultureInfo.InvariantCulture));
                }
                else
                {
                    writer.WriteElementString(elementName, property.GetValue(this, null).ToString());
                }
            }
        }
    }
}
