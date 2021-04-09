using System.Collections.Generic;
using KYLib.Interfaces;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace AppDomicilioSharp.UMLBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una categoria que puede tener un producto o un establecimiento.
	/// </summary>
	public class Categoria : INameable
	{
		/// <summary>
		/// Nombre unico de esta categoria.
		/// </summary>
		[DP("Nombre")] public string Name { get; init; }

		/// <summary>
		/// Devuelve todos los productos que tienen una categoria especifica.
		/// </summary>
		/// <param name="name">Nombre de la categoria.</param>
		/// <returns>Lista de todos los productos que cumplen con la condición.</returns>
		public static List<Producto> AllByCatPro(string name)
		{
			return null;
		}

		/// <summary>
		/// Devuelve todos los productos que tienen una categoria especifica.
		/// </summary>
		/// <param name="cat">Categoria especifica a buscar.</param>
		/// <returns>Lista de todos los productos que cumplen con la condición.</returns>
		public static List<Producto> AllByCatPro(Categoria cat)
		{
			return null;
		}
	}
}