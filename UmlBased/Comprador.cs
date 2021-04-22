using System;
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
		/// Guarda el descuento que aplica a este usuario.
		/// </summary>
		public Real Descuento
		{
			get
			{
				//aqui debemos calcular el descuento que se debe aplicar al usaurio.
				return 1;
			}
		}

		/// <summary>
		/// Guarda el carrito de este usuario, no es necesario ponerle el <see cref="DP"/> ya que el carrito no es algo que se deba guardar solo es temporal.
		/// </summary>
		public Carrito Carrito = new();

		/// <summary>
		/// 
		/// </summary>
		public void PagoCuotas()
		{
			//fecha de hoy
			var hoy = DateTime.Today;
			//casos de retorno, dias en los que no se paga nada.
			if (hoy.Day == 31 || (hoy.Day == 29 && hoy.Month == 2)) return;
			//ultimo dia del mes, 28 solo si es frebrero
			var lastday = hoy.Month == 2 ? 28 : 30;
			//obtenemos solo los pedidos que se deben pagar hoy, por ejemplo si hoy es 5 entonces se obtienen solo los pedidos que se deben pagar los 5.
			var deudas = PagosPendientes().FindAll
			(P =>
				 //el dia 28 de febrero se pagaran tambien los pedidos que se tengan que pagar los 29 y los 30
				 P.Fecha.Day == hoy.Day || P.Fecha.Day >= lastday
			);
			foreach (var pedido in deudas)
			{
				//removemos el dinero de la cuenta del usuario.
				Descontar(pedido.ValorCuota);
				//le decimos al pago que se ha procesado una cuota
				pedido.Pago.ProcesarCuota();
				//actualziamos el estado del pedido, de ser necesarios.
				if (pedido.Pago.Finalizado)
					pedido.Estado = EstadoPedido.Finalizado;
				//le damos el dinero al vendedor.
				pedido.Producto.ObtenerVendedor().SaldoDelta(pedido.ValorCuota);
			}
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