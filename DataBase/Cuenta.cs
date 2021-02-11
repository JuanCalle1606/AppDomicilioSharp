using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace Domicilios.DataBase
{
	[JsonObject(MemberSerialization.OptIn)]
	/// <summary>
	/// 
	/// </summary>
	public abstract class Cuenta : DBClass
	{
		/// <summary>
		/// Obtiene el nombre de la cuenta.
		/// </summary>
		[JsonProperty] 
		public string Nombre { get; protected set; }

		/// <summary>
		/// Obtiene el correo registrado a la cuenta.
		/// </summary>
		[JsonProperty]
		public string Correo { get; protected set; }

		/// <summary>
		/// Obtiene la clave cifrada del usuario actual.
		/// </summary>
		[JsonProperty] 
		public string Clave { get; protected set; }

		[JsonProperty]
		public uint Ip { get; protected set; }

		[JsonProperty]
		public string Direccion { get; protected set; }

		[JsonProperty]
		public string Descripcion { get; protected set; }

		[JsonProperty]
		protected string creado;
		public DateTime Creado { get; protected set; }

		[JsonProperty]
		protected string imagen;
		public Image Imagen { get; protected set; }

		[JsonConstructor]
		protected Cuenta(string table) : base(table)
		{
		}
	}
}
