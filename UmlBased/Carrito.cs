using System.Collections.Generic;
using System.Linq;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Carrito que almacena una lista de pedidos.
	/// </summary>
	public sealed class Carrito
	{

		/// <summary>
		/// Representa una lista de los pedidos del cliente.
		/// </summary>
		private List<Pedido> pedidos = new();

		/// <summary>
		/// Representa el costo de los pedidos almacenados en el carrito.
		/// </summary>
		public Real Total => Mathf.SumOf(pedidos.Select(P => P.ValorTotal));

		public List<Pedido> Pedidos { get => pedidos; }

		/// <summary>
		/// Representa el costo de la primera cuota que se pagara por los pedidos.
		/// </summary>
		public Real CostoCuota => Mathf.SumOf(pedidos.Select(P => P.ValorCuota));

		/// <summary>
		/// Agrega un pedido al carrito.
		/// </summary>
		/// <param name="pedido">Pedido que se desea aregar al carrito.</param>
		/// <returns>Devuelve un booleano que indica si se pudo agregar el pedido.</returns>
		public bool Agregar(Pedido pedido)
		{
			Pedidos.Add(pedido);
			ActualizarDomicilio();
			return true;
		}

		public bool Pagar()
		{
			var saldo = DomiciliosApp.ClienteActual.Saldo;
			//si el saldo es menor que el valor de la primera cuota entonces no es posible pagar.
			if (saldo < CostoCuota)
				return false;
			//si esta funcion devuelve false es porque no se pudo realizar el pago internamente por lo que no procesamos nada
			if (DomiciliosApp.ClienteActual.SaldoDelta(-CostoCuota))
			{
				//agrupamos los pedidos por vendedores.
				var groups = Pedidos.GroupBy(P => P.producto.ObtenerVendedor());

				foreach (var P in pedidos)
				{
					// Actualizamos el estado de cada pedido
					P.Estado = EstadoPedido.Pagado;
					// Creamos el metodo de pago de este pedido
					P.Pago = new MetodoPago
					{
						CuotasTotales = P.Cuotas,
						PrecioTotal = P.ValorTotal
					};
					// indicamos al metodo de pago que ya se procesado el primer pago
					P.Pago.ProcesarCuota();
				}

				//una vez que tenemos todos los pedidos actualizados y pagados debemos enviarlos a los vendedores
				foreach (var item in groups) item.Key.AgregarPedidos(item);

				//si pudimos pagar todo devolvemos true
				return true;
			}

			return false;
		}

		/// <summary>
		/// Elimina un pedido del carrito.
		/// </summary>
		/// <param name="pedido">Pedido que se desea elimiar del carrito.</param>
		/// <returns>Devuelve un booleano que indica si se pudo eliminar el pedido.</returns>
		public bool Eliminar(Pedido pedido)
		{
			Pedidos.Remove(pedido);
			ActualizarDomicilio();
			return true;
		}

		/// <summary>
		/// Busca el domicilio maximo entre los pedidos de una sola cuota.
		/// </summary>
		private void ActualizarDomicilio()
		{
			var unacuota = pedidos.FindAll(P => P.Cuotas.Equals(1)) ?? new();

			Pedido domicilio = null;
			Real maxdomicilio = 0;
			unacuota.ForEach(P =>
			{
				//se actualizan todos por defecto como si contoran su domicilio.
				P.Actualizar(false);
				//se valida si su domicilio es mayor.
				if (P.producto.ValorDomicilio > maxdomicilio)
				{
					domicilio = P;
					maxdomicilio = P.producto.ValorDomicilio;
				}
			});

			//si hay un domicilio mayor, se actualiza.
			domicilio?.Actualizar(true);
		}
	}
}