using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASCIWebApp.Helpers
{
	public static class XmlCustomSerializer
	{
		public static void SerializeToXml<T>(T objectGraph, string filePath) where T : class
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

			using (StreamWriter writer = new StreamWriter(filePath))
			{
				xmlSerializer.Serialize(writer, objectGraph);
			}
		}

		public static T DeserializeFromXml<T>(string filePath) where T : class
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

			using (StreamReader reader = new StreamReader(filePath))
			{
				return xmlSerializer.Deserialize(reader) as T;
			}
		}

		public static T DeserializeFromXmlFile<T>(IFormFile file) where T : class
		{
			// get path for temp file to copy content from file in it
			var filePath = Path.GetTempFileName();

			// creates the temp file
			var tempFile = File.Create(filePath);

			// copy content from file to temp file
			file.CopyTo(tempFile);

			// dispose to avoid exception of FileStream
			tempFile.Dispose();

			return DeserializeFromXml<T>(filePath);
		}
	}
}
