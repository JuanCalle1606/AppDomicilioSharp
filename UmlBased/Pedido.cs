using System;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa un pedido que se le ha hecho a un <see cref="Vendedor"/>
	/// </summary>
	public class Pedido
	{
		/// <summary>
		/// Id unico de este pedido.
		/// </summary>
		[DP] public Int Id { get; init; }

		/// <summary>
		/// Numero de cuotas en las que se pagara este pedido.
		/// </summary>
		[DP] public Small Cuotas { get; private set; } = 1;

		/// <summary>
		/// Cantidad de <see cref="producto"/> que contiene este pedido.
		/// </summary>
		[DP] public Small Cantidad = 1;

		/// <summary>
		/// Guarda la fecha en que se realizo este pedido. Esta fecha es la fecha del primer pago
		/// </summary>
		[DP] public DateTime Fecha { get; init; }

		/// <summary>
		/// Producto que esta en este pedido.
		/// </summary>
		[DP] public Producto producto { get; init; }

		/// <summary>
		/// Guarda el precio base del producto el dia que se compro.
		/// </summary>
		[DP] public Real ValorBase { get; init; }

		/// <summary>
		/// Guarda el descuento que se hizo al momento de comprar el producto.
		/// </summary>
		[DP] public Real ValorDescuento { get; init; }

		/// <summary>
		/// Guarda cuanto se pago de domicilio por este producto.
		/// </summary>
		[DP] public Real ValorDomicilio { get; private set; }

		/// <summary>
		/// Guarda un valor aditivo que se añade al total si la compra es por cuotas.
		/// </summary>
		[DP] public Real ValorAditivo { get; private set; }

		/// <summary>
		/// Guarda la cantidad de iva que sera aplicada + 1, es decir, si el iva era de 18% aqui se guarda 1.18 para mas facilidad.
		/// </summary>
		[DP] public Real PorcentajeIVA { get; init; }

		/// <summary>
		/// Guarda el valor total que se pagara por este producto.
		/// </summary>
		[DP] public Real ValorTotal { get; private set; }

		/// <summary>
		/// Guarda el valor de la primera cuota de este producto.
		/// </summary>	
		[DP] public Real ValorCuota { get; private set; }

		/// <summary>
		/// Estado actual del producto.
		/// </summary>
		[DP] public EstadoPedido Estado = EstadoPedido.Carrito;

		/// <summary>
		/// Guarda la información de pago de este pedido, nulo cuando aun no se ha pagado.
		/// </summary>
		[DP] public MetodoPago Pago;

		/// <summary>
		/// Cancela un pedido, para cancelar un pedido debe ser de unica cuota o puede ser de multiples cuotas pero que solo se haya pagado una, de lo contrario no podra ser reembolsado.
		/// </summary>
		/// <returns>Retorna un valor que indica si se pudo o no cancelar el pedido.</returns>
		public bool Cancelar()
		{
			//solo se puede devolver el item si no ha sido despachado.
			if (Estado == EstadoPedido.Pagado)
			{
				//removemos el pedido de la lista del vendedor
				var rev = producto.ObtenerVendedor().Pedidos.Remove(this);
				//si no se puedo remover retornamos.
				if (!rev) return false;
				//actualizamos el estado del pedido.
				Estado = EstadoPedido.Cancelado;
				// le devolvemos el dinero de una cuota (lo que se pago en la primera cuota) al cliente.
				DomiciliosApp.ClienteActual.SaldoDelta(ValorCuota);
			}
			return false;
		}

		/// <summary>
		/// Calcula los precios totales y de cuotas.
		/// </summary>
		private void CalcularTotal()
		{

		}

		/// <summary>
		/// Indica que se cambiado uno de los valores del pedido por lo que se deben recalcular los costos.
		/// </summary>
		/// <param name="cantidad">Nueva cantidad de productos.</param>
		/// <param name="cuotas">Nueva cantidad de cuotas.</param>
		public void Actualizar(Small cantidad, Small cuotas)
		{

		}

		/// <summary>
		/// Indica que se ha agregado un nuevo pedido al carrito por lo que puede que haya cambiado el domicilio maximo.
		/// </summary>
		/// <param name="domicilio">Indica si el producto de este pedido es el que tiene domicilio mas caro.</param>
		public void Actualizar(bool domicilio)
		{

		}
	}
}