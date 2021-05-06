using System;
using System.Collections.Generic;
using KYLib.Extensions;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una cuenta de un vendedor.
	/// </summary>
	public sealed class Vendedor : Usuario
	{
		/// <summary>
		/// Lista de calificaciones que le han dado a este vendedor.
		/// </summary>
		[DP] private List<Small> calificacion = new();

		/// <summary>
		/// Menu de productos que ofrece este vendedor.
		/// </summary>
		[DP] public List<Producto> Menu { get; private set; } = new();

		/// <summary>
		/// Promedio de las calificaciones.
		/// </summary>
		/*TODO: Cambiar el tipo de Real a Float*/
		public Real Calificacion => calificacion.Count == 0 ? 0 : Mathf.MeanOf<Small, Real>(calificacion);

		/// <summary>
		/// Lista de pedidos que le han realizado a este vendedor.
		/// </summary>
		[DP] public List<Pedido> Pedidos { get; private set; } = new();

		public bool Cancelar(Pedido pedido) =>
			pedido.Cancelar() && Pedidos.Remove(pedido);

		/// <summary>
		/// Agrega unos pedidos a este vendedor para que los despache.
		/// </summary>
		/// <param name="pedidos">Pedidos a agregar</param>
		public bool AgregarPedidos(IEnumerable<Pedido> pedidos)
		{
			Pedidos.AddRange(pedidos);
			return true;
		}

		/// <summary>
		/// Agrega un producto al menu de este vendedor.
		/// </summary>
		/// <param name="producto">Producto a agregar</param>
		public bool AgregarProducto(Producto producto)
		{
			Menu.Add(producto);
			DomiciliosApp.Instance.Productos.Add(producto);
			return true;
		}

		public bool RemoverProducto(Producto producto) =>
			Menu.Remove(producto) && DomiciliosApp.Instance.Productos.Remove(producto);

		/// <summary>
		/// Agrega una puntuacion a este producto.
		/// </summary>
		/// <param name="puntuacion">Calificacion a agregar.</param>
		public bool Calificar(Small puntuacion)
		{
			calificacion.Add(puntuacion);
			return true;
		}
	}
}