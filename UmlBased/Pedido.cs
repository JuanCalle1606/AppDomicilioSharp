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
		/// Almacena el IVA actual.
		/// </summary>
		public static readonly Real CurrentIVA = 1.19;

		/// <summary>
		/// Id unico de este pedido.
		/// </summary>
		[DP] public Int Id { get; init; }

		/// <summary>
		/// Numero de cuotas en las que se pagara este pedido.
		/// </summary>
		[DP] public Small Cuotas { get; private set; } = 1;

		/// <summary>
		/// Cantidad de <see cref="Producto"/> que contiene este pedido.
		/// </summary>
		[DP] public Small Cantidad;

		/// <summary>
		/// Guarda la fecha en que se realizo este pedido.
		/// </summary>
		/// <remarks>
		/// Cuando el pedido esta en carrito indica la fecha en la que se agrego pero cuando cuando se paga el pedido esta fecha guarda el momento en que se paga.
		/// </remarks>
		[DP] public DateTime Fecha { get; init; }

		/// <summary>
		/// Producto que esta en este pedido.
		/// </summary>
		[DP] public Producto Producto { get; init; }

		/// <summary>
		/// Guarda el precio base del producto el dia que se compro.
		/// </summary>
		[DP] public Real ValorBase { get; init; }

		/// <summary>
		/// Guarda el descuento que se hizo al momento de comprar el producto.
		/// </summary>
		[DP] public Real PorcentajeDescuento { get; init; }

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

		[DP] public Int OwnerID { get; init; }

		bool preserve = true;

		public Comprador ObtenerComprador() =>
			DomiciliosApp.ObtenerComprador(OwnerID);


		/// <summary>
		/// Cancela un pedido, para cancelar un pedido debe ser de unica cuota o puede ser de multiples cuotas pero que solo se haya pagado una, de lo contrario no podra ser reembolsado.
		/// </summary>
		/// <returns>Retorna un valor que indica si se pudo o no cancelar el pedido.</returns>
		public bool Cancelar()
		{
			//solo se puede devolver el item si no ha sido despachado.
			if (Estado == EstadoPedido.Pagado)
			{
				//actualizamos el estado del pedido.
				Estado = EstadoPedido.Cancelado;
				// le devolvemos el dinero de una cuota (lo que se pago en la primera cuota) al cliente.
				DomiciliosApp.ClienteActual.CambiarSaldo(ValorCuota);

				return true;
			}
			return false;
		}

		/// <summary>
		/// Calcula los precios totales y de cuotas.
		/// </summary>
		private void CalcularTotal()
		{
			if (preserve || Cuotas > 1)
				ValorDomicilio = Producto.ValorDomicilio;
			else
				ValorDomicilio = 0;

			var total = ValorBase * Cantidad;

			//Calculamos todo
			ValorAditivo = total * (Cuotas - 1) * 0.01;
			total *= PorcentajeDescuento;
			total += ValorDomicilio;
			total += ValorAditivo;
			total *= PorcentajeIVA;

			ValorTotal = total;
			ValorCuota = ValorTotal / Cuotas;
		}

		/// <summary>
		/// Indica que se cambiado uno de los valores del pedido por lo que se deben recalcular los costos.
		/// </summary>
		/// <param name="cantidad">Nueva cantidad de productos.</param>
		/// <param name="cuotas">Nueva cantidad de cuotas.</param>
		public void Actualizar(Small cantidad, Small cuotas)
		{
			Cantidad = cantidad;
			Cuotas = cuotas;
			CalcularTotal();
		}

		/// <summary>
		/// Indica que se ha agregado un nuevo pedido al carrito por lo que puede que haya cambiado el domicilio maximo.
		/// </summary>
		/// <param name="domicilio">Indica si el producto de este pedido es el que tiene domicilio mas caro.</param>
		public void Actualizar(bool domicilio)
		{
			preserve = domicilio;
			CalcularTotal();
		}
	}
}