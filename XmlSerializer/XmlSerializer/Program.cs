using System;
using System.IO;
using System.Xml.Serialization;

namespace XmlSerializerPoC
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // RunSpecificFloatingPointObjectSerializationCase();
            RunGenericXmlSerializationCase();
            RunGenericXmlDeserializationCase();
        }

        private static void RunGenericXmlSerializationCase()
        {
            Payload p = new Payload()
            {
                Id = Guid.NewGuid(),
                Name = "newName",
                Score = 18.18646848416,
                Value = 56
            };

            XmlSerializer xs = new XmlSerializer(typeof(Payload));
            using (StringWriter textWriter = new StringWriter())
            {
                xs.Serialize(textWriter, p);
                Console.WriteLine(textWriter.ToString());
            }
        }

        private static void RunGenericXmlDeserializationCase()
        {
            string xmlPayload = @"<?xml version=""1.0"" encoding=""utf - 16""?>
                <Payload>
                    <Id>8b2f5dd1-3144-4e57-b1ce-54f23c26de8a</Id>
                    <Name>newName</Name>
                    <Value>56</Value>
                    <Score>18.19</Score>
                </Payload>";

            using (TextReader textReader = new StringReader(xmlPayload))
            {
                Payload p = (Payload)new XmlSerializer(typeof(Payload)).Deserialize(textReader);
                Console.WriteLine("\nPayload (" + p.Id + "): { " + p.Name + " : " + p.Value + "; " + p.Score + " }");
            }
        }

        private static void RunSpecificFloatingPointObjectSerializationCase()
        {
            AltPayload alt = new AltPayload()
            {
                Label = "Valor",
                Xfp = 4.5600156486468
            };

            using (StringWriter textWriter = new StringWriter())
            {
                // Original Printing
                Console.WriteLine("To XML: {" + alt.Label + ", " + alt.Xfp.Value + "}\n");
                XmlSerializer xs = new XmlSerializer(typeof(AltPayload));
                xs.Serialize(textWriter, alt);
                Console.WriteLine(textWriter.ToString());

                // Printing after SUM & change considered digits to 5
                alt.Xfp += 1;
                alt.Xfp.Decimals = 5;
                Console.WriteLine("\nPlus 1: {" + alt.Label + ", " + alt.Xfp.Value + "}\n");
                xs.Serialize(textWriter, alt);
                Console.WriteLine(textWriter.ToString());

                // Printing after SUBTRACTION & change considered digits to 3
                alt.Xfp -= new XmlFloatingPoint() { Value = .5 };
                alt.Xfp.Decimals = 3;
                Console.WriteLine("\nMinus 0.5: {" + alt.Label + ", " + alt.Xfp.Value + "}\n");
                xs.Serialize(textWriter, alt);
                Console.WriteLine(textWriter.ToString());

                // Printing after MULTIPLICATION & change considered digits to 0
                alt.Xfp *= new XmlFloatingPoint() { Value = 3 };
                alt.Xfp.Decimals = 0;
                Console.WriteLine("\nTimes 3: {" + alt.Label + ", " + alt.Xfp.Value + "}\n");
                xs.Serialize(textWriter, alt);
                Console.WriteLine(textWriter.ToString());

                // Printing after DIVISION & change considered digits back to 2
                alt.Xfp /= 2;
                alt.Xfp.Decimals = 2;
                Console.WriteLine("\nBy 2: {" + alt.Label + ", " + alt.Xfp.Value + "}\n");
                xs.Serialize(textWriter, alt);
                Console.WriteLine(textWriter.ToString());
            }
        }
    }
}
