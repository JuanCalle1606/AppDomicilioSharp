using System;
using System.Collections.Generic;
using KYLib.Extensions;
using KYLib.Interfaces;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una factura que se genera cuando se agrega un producto a un carrito.
	/// </summary>
	public sealed class Factura : ICompra
	{
		/// <inheritdoc/>
		[DP] public long Id { get; init; }

		/// <inheritdoc/>
		[DP] public Producto producto { get; init; }

		/// <inheritdoc/>
		[DP] public short Cantidad { get; init; }

		/// <inheritdoc/>
		[DP] public DateTime Fecha { get; init; }

		/// <inheritdoc/>
		[DP] public Real Precio { get; init; }


		/// <summary>
		/// Guarda el metodo de apgo que se usa en esta factura.
		/// </summary>
		[DP] public MetodoPago Pago { get; init; }


		/// <summary>
		/// Calcula cuanto falta para terminar de pagar esta factura.
		/// </summary>
		/// <returns>Un <see cref="Real"/> que es la cantidad de dinero que falta para poder termianr de pagar este producto.</returns>
		public Real CalcularResto()
		{
			return 0;
		}
	}
}