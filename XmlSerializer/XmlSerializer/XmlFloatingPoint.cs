using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XmlSerializerPoC
{
    /// <summary>
    /// A representation of a numeric value for XML (De)Serialization, considering the required amount of decimal digits.
    /// </summary>
    public class XmlFloatingPoint : IXmlSerializable
    {
        /// <summary>
        /// The numeric value to be used under a XML (De)Serializing context.
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// The amount of decimal digits considered upon XML Serialization.
        /// </summary>
        public int Decimals { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string readValue = reader.ReadContentAsString();
            Value = double.Parse(readValue);
            Decimals = readValue.Length - (readValue.IndexOf('.') + 1);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(Value.ToString("N" + Decimals, CultureInfo.InvariantCulture));
        }

        public static implicit operator XmlFloatingPoint(double value)
        {
            return new XmlFloatingPoint(value);
        }

        public XmlFloatingPoint() { }

        public XmlFloatingPoint(double value, int decimals = 2)
        {
        }

        public static XmlFloatingPoint operator +(XmlFloatingPoint operatorA, XmlFloatingPoint operatorB)
        {
            return new XmlFloatingPoint()
            {
                Value = operatorA.Value + operatorB.Value,
                Decimals = operatorA.Decimals
            };
        }

        public static XmlFloatingPoint operator +(XmlFloatingPoint operatorA, object operatorB)
        {
            if (IsNumber(operatorB))
            {
                return new XmlFloatingPoint()
                {
                    Value = operatorA.Value + Convert.ToDouble(operatorB),
                    Decimals = operatorA.Decimals
                };
            }
            else
            {
                return operatorA;
            }
        }

        public static XmlFloatingPoint operator -(XmlFloatingPoint operatorA, XmlFloatingPoint operatorB)
        {
            return new XmlFloatingPoint()
            {
                Value = operatorA.Value - operatorB.Value,
                Decimals = operatorA.Decimals
            };
        }

        public static XmlFloatingPoint operator -(XmlFloatingPoint operatorA, object operatorB)
        {
            if (IsNumber(operatorB))
            {
                return new XmlFloatingPoint()
                {
                    Value = operatorA.Value - Convert.ToDouble(operatorB),
                    Decimals = operatorA.Decimals
                };
            }
            else
            {
                return operatorA;
            }
        }

        public static XmlFloatingPoint operator *(XmlFloatingPoint operatorA, XmlFloatingPoint operatorB)
        {
            return new XmlFloatingPoint()
            {
                Value = operatorA.Value * operatorB.Value,
                Decimals = operatorA.Decimals
            };
        }

        public static XmlFloatingPoint operator *(XmlFloatingPoint operatorA, object operatorB)
        {
            if (IsNumber(operatorB))
            {
                return new XmlFloatingPoint()
                {
                    Value = operatorA.Value * Convert.ToDouble(operatorB),
                    Decimals = operatorA.Decimals
                };
            }
            else
            {
                return operatorA;
            }
        }

        public static XmlFloatingPoint operator /(XmlFloatingPoint operatorA, XmlFloatingPoint operatorB)
        {
            return new XmlFloatingPoint()
            {
                Value = operatorA.Value / operatorB.Value,
                Decimals = operatorA.Decimals
            };
        }

        public static XmlFloatingPoint operator /(XmlFloatingPoint operatorA, object operatorB)
        {
            if (IsNumber(operatorB))
            {
                return new XmlFloatingPoint()
                {
                    Value = operatorA.Value / Convert.ToDouble(operatorB),
                    Decimals = operatorA.Decimals
                };
            }
            else
            {
                return operatorA;
            }
        }

        private static bool IsNumber(object input)
        {
            if (input == null)
            {
                return false;
            }

            return double.TryParse(Convert.ToString(input, CultureInfo.InvariantCulture), System.Globalization.NumberStyles.Any, NumberFormatInfo.InvariantInfo, out double number);
        }
    }
}
