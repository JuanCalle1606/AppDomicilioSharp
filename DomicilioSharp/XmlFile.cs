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
			JsonConvert.DeserializeObject(source, Settings);

		/// <inheritdoc/>
		public T Deserialize<T>(string source) =>
			JsonConvert.DeserializeObject<T>(source, Settings);

		/// <inheritdoc/>
		public object Load(string path)
		{
			string realpath = ValidatePath(path);
			string content = File.ReadAllText(realpath);
			return JsonConvert.DeserializeObject(content, Settings);
		}

		/// <inheritdoc/>
		public T Load<T>(string path)
		{
			string realpath = ValidatePath(path);
			string content = File.ReadAllText(realpath);
			return JsonConvert.DeserializeObject<T>(content, Settings);
		}

		/// <summary>
		/// Usamos esta función para ver si existe el archivo solicitado.
		/// </summary>
		private string ValidatePath(string path)
		{
			string realpath;
			//primero vemos si el archivo especificado existe, esto se hace por si el nombre pasado no tiene la extension del archivo
			if (File.Exists(path))
				realpath = path;
			// ahora vemos si existe el archivo pero con extension especifica
			else if (File.Exists(path + Extension))
				realpath = path + Extension;
			//si no existe se genera error
			else
				throw new FileNotFoundException("El archivo especificado no existe por lo que no puede ser cargado", path);
			return realpath;
		}

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