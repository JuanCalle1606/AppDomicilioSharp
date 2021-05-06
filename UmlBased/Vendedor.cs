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
		private List<Small> calificacion = new();

		/// <summary>
		/// Menu de productos que ofrece este vendedor.
		/// </summary>
		public List<Producto> Menu { get; private set; } = new();

		/// <summary>
		/// Promedio de las calificaciones.
		/// </summary>
		/*TODO: Cambiar el tipo de Real a Float*/
		public Real Calificacion => Mathf.MeanOf<Small, Real>(calificacion);

		/// <summary>
		/// Lista de horarios en las que este vendedor ofrece atención al cliente.
		/// </summary>
		public List<DateTime> AtencionCliente { get; private set; } = new();

		/// <summary>
		/// Lista de pedidos que le han realizado a este vendedor.
		/// </summary>
		public List<Pedido> Pedidos { get; private set; } = new();

		/// <summary>
		/// Agrega un pedido a este vendedor para que lo despache.
		/// </summary>
		/// <param name="pedido">Pedido a agregar</param>
		public bool AgregarPedido(Pedido pedido)
		{
			Pedidos.Add(pedido);
			return true;
		}

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
			return true;
		}

		/// <summary>
		/// Agrega varios productos al menu de este vendedor.
		/// </summary>
		/// <param name="producto">Productos a agregar</param>
		public bool AgregarProductos(List<Producto> productos)
		{
			Menu.AddRange(productos);
			return true;
		}

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