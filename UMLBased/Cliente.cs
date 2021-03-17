using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace AppDomicilioSharp
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una cuenta de un cliente 
	/// </summary>
	public sealed class Cliente : Cuenta
	{
		/// <summary>
		/// 
		/// </summary>
		[DP] public TipoCliente Tipo { get; private set; }

		//Carrito

		//historial

		/// <summary>
		/// 
		/// </summary>
		public void PagoCuotas()
		{
			//Logica: Cada inicio de mes el programa principal llama a esta función en cada cliente para que itere su historial de pedidos y descuente una cuota de cada producto.
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