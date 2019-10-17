using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			// Direct XML parsing from File
			SimplestXmlParsing();
		}

		static void SimplestXmlParsing()
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("product.xml");

			XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("/Table/Product");
			List<Product> products = new List<Product>();

			foreach (XmlNode node in nodes)
			{
				Product prod = new Product();

				prod.id = int.Parse(node.SelectSingleNode("Id").InnerText);
				prod.name = node.SelectSingleNode("Name").InnerText;
				prod.price = double.Parse(node.SelectSingleNode("Price").InnerText);

				products.Add(prod);
			}

			foreach (Product p in products)
			{
				Console.WriteLine("Product " + p.id + ": " + p.name + "; R$" + p.price);
			}

			Console.ReadLine();
		}
	}

	class Product
	{
		[XmlElement("Id")] public int id;
		[XmlElement("Name")] public string name;
		[XmlElement("Price")] public double price;
	}
}
