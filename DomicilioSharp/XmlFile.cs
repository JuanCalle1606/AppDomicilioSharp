using System.IO;
using System.Xml.Linq;
using KYLib.Data.Converters;
using KYLib.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DomicilioSharp
{
	internal class XmlFile : IDataFile
	{
		public static XmlFile Default = new();

		/// <inheritdoc/>
		public JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings();

		/// <summary>
		/// Crea un nuevo <see cref="JsonFile"/> para la serialización y deserialización en formato Json.
		/// </summary>
		public XmlFile()
		{
			Settings.Converters.Add(new NumberConverter());
			Settings.Converters.Add(new StringEnumConverter());
			Settings.NullValueHandling = NullValueHandling.Ignore;
		}

		#region IDataFile

		/// <inheritdoc/>
		public string Extension => ".xml";

		/// <inheritdoc/>
		public object Deserialize(string source) =>
			throw new System.NotImplementedException();

		/// <inheritdoc/>
		public T Deserialize<T>(string source) =>
			throw new System.NotImplementedException();

		/// <inheritdoc/>
		public object Load(string path) =>
			throw new System.NotImplementedException();

		/// <inheritdoc/>
		public T Load<T>(string path) =>
			throw new System.NotImplementedException();

		/// <inheritdoc/>
		public void Save(object source, string path)
		{
			using StreamWriter file = File.CreateText(path);
			string json = Serialize(source);
			XNode node = JsonConvert.DeserializeXNode(json, "Root");
			file.Write(node.ToString());
		}

		/// <inheritdoc/>
		public string Serialize(object source) =>
			JsonConvert.SerializeObject(source, Settings);

		#endregion
	}
}