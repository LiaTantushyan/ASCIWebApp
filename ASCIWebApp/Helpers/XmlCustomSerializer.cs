using Microsoft.AspNetCore.Http;
using System.IO;
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
			var filePath = GetFilePath(file);

			return DeserializeFromXml<T>(filePath);
		}

		public static string GetFilePath(IFormFile formFile)
		{
			var filePath = Path.GetTempFileName();

			using (var stream = System.IO.File.Create(filePath))
			{
				formFile.CopyTo(stream);
			}

			return filePath;
		}
	}
}
