using System.Collections.Generic;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una cuenta de un cliente 
	/// </summary>
	public sealed class Comprador : Usuario
	{
		/// <summary>
		/// El tipo de este cliente
		/// </summary>
		[DP] public TipoComprador Tipo { get; private set; }

		/// <summary>
		/// Guarda todos los pedidos que ha realizado este cliente.
		/// </summary>
		[DP("Historial")] public List<Pedido> HistorialPedidos { get; } = new();

		/// <summary>
		/// Guarda el carrito de este usuario, no es necesario ponerle el <see cref="DP"/> ya que el carrito no es algo que se deba guardar solo es temporal.
		/// </summary>
		public Carrito Carrito = new();

		/// <summary>
		/// 
		/// </summary>
		public void PagoCuotas()
		{
			//Logica: Cada inicio de mes el programa principal llama a esta función en cada cliente para que itere su historial de pedidos y descuente una cuota de cada producto.
		}

		/// <summary>
		/// Busca y devuelve todas los pedidos que aun no ha pagado este cliente.
		/// </summary>
		public List<Pedido> PagosPendientes() => HistorialPedidos.FindAll
		(
			//devolvemos los elementos que tengan el estado en entregado o en pagado(1 cuota) y es de varias cuotas.
			P => P.Estado == EstadoPedido.Entregado || (P.Estado == EstadoPedido.Pagado && P.Cuotas > 1)
		);

		/// <summary>
		/// Abona un saldo en un pedido especifico. Esta función se usara por los clientes cuando quiera pagar algo entre sus pedidos.
		/// </summary>
		/// <param name="pedido">Factura en la cual se quiere abonar.</param>
		/// <param name="saldo">Saldo a abonar.</param>
		/// <returns>Devuelve un valor indicando si ha sido posible o no abonar en esa factura.</returns>
		public bool Abonar(Pedido pedido, Real saldo)
		{
			return true;
		}
	}
}