using System;
using System.Collections.Generic;
using System.Linq;
using KYLib.MathFn;

namespace UmlBased
{
	/// <summary>
	/// Carrito que almacena una lista de pedidos.
	/// </summary>
	public sealed class Carrito
	{
		/// <summary>
		/// Evento que se llama cuando se agrega o elimina un pedido del carrito.
		/// </summary>
		public event Action<bool, Pedido> Changed;

		/// <summary>
		/// Representa el costo de los pedidos almacenados en el carrito.
		/// </summary>
		public Real Total => Mathf.SumOf(Pedidos.Select(P => P.ValorTotal));

		/// <summary>
		/// Representa una lista de los pedidos del cliente.
		/// </summary>
		public List<Pedido> Pedidos { get; } = new();

		/// <summary>
		/// Representa el costo de la primera cuota que se pagara por los pedidos.
		/// </summary>
		public Real CostoCuota => Mathf.SumOf(Pedidos.Select(P => P.ValorCuota));

		/// <summary>
		/// Vacia el carrito.
		/// </summary>
		public void Vaciar() => Pedidos.Clear();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="producto"></param>
		/// <returns></returns>
		public Pedido Contiene(Producto producto) =>
			Pedidos.Find(P => P.Producto.Equals(producto));

		/// <summary>
		/// Agrega un pedido al carrito.
		/// </summary>
		/// <param name="pedido">Pedido que se desea agregar al carrito.</param>
		/// <returns>Devuelve un booleano que indica si se pudo agregar el pedido.</returns>
		public bool Agregar(Pedido pedido)
		{
			//vemos si ya existe un pedido con ese producto
			var ped = Pedidos.Find(P => P.Producto.Equals(pedido.Producto));
			//si no existe entonces se agrega.
			if (ped == null)
			{
				Pedidos.Add(pedido);
				//esto solo ocurre cuando se agrega un nuevo pedido, no cuando se actualiza uno existente.
				ActualizarDomicilio();
				Changed?.Invoke(true, pedido);
				return true;
			}
			Changed?.Invoke(true, null);
			return false;
		}

		/// <summary>
		/// Realiza el pago del carrito.
		/// </summary>
		/// <returns>Un valor que indica si se pudo pagar el carrito.</returns>
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
				var groups = Pedidos.GroupBy(P => P.Producto.ObtenerVendedor());
				var user = (Comprador)DomiciliosApp.ClienteActual;

				foreach (var P in Pedidos)
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
					// Acualizamos la fecha a este momento
					P.Fecha.Add(DateTime.Now - P.Fecha);
					// agregamos el pedido al historial
					user.HistorialPedidos.Add(P);
				}

				//una vez que tenemos todos los pedidos actualizados y pagados debemos enviarlos a los vendedores
				foreach (var item in groups) item.Key.AgregarPedidos(item);

				// limpiamos el carrito actual
				Pedidos.Clear();

				// evento de que ha ocurrido un cambio.
				Changed?.Invoke(false, null);

				//si pudimos pagar todo devolvemos true
				return true;
			}

			return false;
		}

		/// <summary>
		/// Elimina un pedido del carrito.
		/// </summary>
		/// <param name="pedido">Pedido que se desea eliminar del carrito.</param>
		/// <returns>Devuelve un booleano que indica si se pudo eliminar el pedido.</returns>
		public bool Eliminar(Pedido pedido)
		{
			var dev = Pedidos.Remove(pedido);
			if (dev)
			{
				ActualizarDomicilio();
				Changed?.Invoke(false, pedido);
			}
			return dev;
		}

		/// <summary>
		/// Busca el domicilio maximo entre los pedidos de una sola cuota.
		/// </summary>
		private void ActualizarDomicilio()
		{
			var unacuota = Pedidos.FindAll(P => P.Cuotas.Equals(1)) ?? new();

			Pedido domicilio = null;
			Real maxdomicilio = 0;
			unacuota.ForEach(P =>
			{
				//se actualizan todos por defecto como si contoran su domicilio.
				P.Actualizar(false);
				//se valida si su domicilio es mayor.
				if (P.Producto.ValorDomicilio > maxdomicilio)
				{
					domicilio = P;
					maxdomicilio = P.Producto.ValorDomicilio;
				}
			});

			//si hay un domicilio mayor, se actualiza.
			domicilio?.Actualizar(true);
		}
	}
}