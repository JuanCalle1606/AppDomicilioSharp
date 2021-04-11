using System.Collections.Generic;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una cuenta de un establecimiento 
	/// </summary>
	public sealed class Carrito
	{
		/// <summary>
		/// Representa el costo de los pedidos almacenados en el carrito mas el IVA.
		/// </summary>
		private double total;

		/// <summary>
		/// Representa una lista de los pedidos del cliente.
		/// </summary>
		private List<Pedido> pedidos;

		/// <summary>
		/// Representa el costo de los pedidos almacenados en el carrito.
		/// </summary>
		private double costo;

		public double Total { get => total; }
		public List<Pedido> Pedidos { get => pedidos; }
		public double CostoCuota { get => costo; }

		public Carrito()
		{


		}

		/// <summary>
		/// Agrega un pedido al carrito.
		/// </summary>
		/// <param name="pedido">Pedido que se desea aregar al carrito.</param>
		/// <returns>Devuelve un booleano que indica si se pudo agregar el pedido.</returns>
		public bool Agregar(Pedido pedido)
		{
			Pedidos.Add(pedido);
			return true;
		}



		public bool Pagar() //Esta funcion debe ir en el comprador puesto que se necesita acceso 
		{                   //a su saldo y de acuerdo con el diagrama UML la clase carrito no puede
			return true;    //ver a la clase comprador
		}

		/// <summary>
		/// Elimina un pedido del carrito.
		/// </summary>
		/// <param name="pedido">Pedido que se desea elimiar del carrito.</param>
		/// <returns>Devuelve un booleano que indica si se pudo eliminar el pedido.</returns>
		public bool Eliminar(Pedido pedido)
		{
			Pedidos.Remove(pedido);
			return true;
		}
	}
}