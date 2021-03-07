using System;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace AppDomicilioSharp.UMLBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una compra realizada que esta vinculada a un unico producto.
	/// </summary>
	public interface ICompra
	{
		/// <summary>
		/// Identificador unico de esta compra.
		/// </summary>
		[DP] long Id { get; init; }

		/**
		 * TODO:
		 * - Cuando se cree la clase Producto agregar aqui la propiedad "producto".
		 */

		/// <summary>
		/// Cantidad de productos en la compra.
		/// </summary>
		[DP] short Cantidad { get; init; }

		/// <summary>
		/// Indica la fecha en la que se realizo esta compra.
		/// </summary>
		[DP] DateTime Fecha { get; init; }

		/// <summary>
		/// El precio total(con descuentos y domicilios) que se va a pagar por esta compra.
		/// </summary>
		[DP] double Precio { get; init; }
	}
}