using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domicilios.DataBase
{
	[JsonObject(MemberSerialization.OptIn)]
	/// <summary>
	/// Clase base para todos los elementos que representan un fila en una abse de datos.
	/// </summary>
	public abstract class DBClass
	{
		[JsonProperty]
		/// <summary>
		/// Obtiene la id del elemento en la base.
		/// </summary>
		public uint Id { get; }

		/// <summary>
		/// Obtiene el nombre de la tabla a la que este elemento pertenece
		/// </summary>
		public string Table { get; protected set; }

		[JsonConstructor]
		protected DBClass(string table) => Table = table;

	}
}
