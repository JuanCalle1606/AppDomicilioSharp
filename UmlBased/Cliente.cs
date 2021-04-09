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
	public sealed class Cliente : Cuenta
	{
		/// <summary>
		/// El tipo de este cliente
		/// </summary>
		[DP] public TipoCliente Tipo { get; private set; }

		//Carrito

		/// <summary>
		/// Guarda todos los pedidos que ha realizado este cliente.
		/// </summary>
		[DP("Historial")] public List<Factura> HistorialPedidos { get; } = new();

		/// <summary>
		/// 
		/// </summary>
		public void PagoCuotas()
		{
			//Logica: Cada inicio de mes el programa principal llama a esta función en cada cliente para que itere su historial de pedidos y descuente una cuota de cada producto.
		}

		/// <summary>
		/// Busca y devuelve todas las facturas que aun no ha pagado este cliente.
		/// </summary>
		public List<Factura> FacturasPendientes()
		{
			return null;
		}

		/// <summary>
		/// Abona un saldo en una factura especifica. Esta función se usara por los clientes cuando quiera paagr algo entre sus facturas.
		/// </summary>
		/// <param name="factura">Factura en la cual se quiere abonar.</param>
		/// <param name="saldo">Saldo a abonar.</param>
		/// <returns>Devuelve un valor indicando si ha sido posible o no abonar en esa factura.</returns>
		public bool Abonar(Factura factura, Real saldo)
		{
			return true;
		}
	}

	/// <summary>
	/// Enumeración de tipos de clientes disponibles.
	/// </summary>
	public enum TipoCliente
	{
		/// <summary>
		/// Tipo de cliente por defecto.
		/// </summary>
		Normal,
		/// <summary>
		/// Tipo de cliente premium.
		/// </summary>
		VIP,
		/// <summary>
		/// Tipo de cliente preferencial con descuento por compras.
		/// </summary>
		Prioritario
	}
}