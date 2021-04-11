
using System;
using KYLib.Data.Converters;
using KYLib.Interfaces;
using KYLib.MathFn;
using Newtonsoft.Json;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{

	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una cuenta en la aplicación.
	/// </summary>
	public abstract class Usuario : INameable
	{
		/// <summary>
		/// Variable de bloqueo para manejo de dinero
		/// </summary>
		private readonly object _locker = new();

		/// <summary>
		/// Identificador unico de esta cuenta.
		/// </summary>
		[DP] public Int Id { get; init; }

		/// <summary>
		/// Nomber que se le ha asignado a la cuenta.
		/// </summary>
		[DP("Nombre")] public string Name { get; protected set; }

		/// <summary>
		/// Dirección del rpopietario de la cuenta.
		/// </summary>
		[DP] public string Direccion { get; init; }

		/// <summary>
		/// Fecha en la que se creo esta cuenta.
		/// </summary>
		[DP] public string Creacion { get; private set; }

		/// <summary>
		/// Almacena la clave de este usuario.
		/// </summary>
		[JsonConverter(typeof(Base64Converter))]
		[DP] public string Clave { get; init; }

		/// <summary>
		/// Url a una foto para usar en la cuenta.
		/// </summary>
		[DP("FotoUrl")] public string Foto { get; protected set; }

		/// <summary>
		/// Telefono del propietario de la cuenta.
		/// </summary>
		[DP] public string Telefono { get; protected set; }

		/// <summary>
		/// Correo unico registrado en la cuenta, este se usa para iniciar sesión
		/// </summary>
		[DP] public string Correo { get; init; }

		/// <summary>
		/// Saldo actual de esta cuenta.
		/// </summary>
		[DP] public Real Saldo { get; private set; }

		/// <summary>
		/// Devuelve un valor que indica lo que esta endeudado este usuario.
		/// </summary>
		public Real Deuda => Saldo < 0 ? Math.Abs(Saldo) : 0;

		/// <summary>
		/// Descuenta un valor al saldo del usuario, este metodo se llama en los pagos mensuales.
		/// </summary>
		/// <param name="valor"></param>
		public void Descontar(Real valor)
		{
			//hacemos un bloqueo multihilo para evitar que se hagan multiples manipulaciones de dinero al tiempo.
			lock (_locker)
			{
				//restamos el valor sin validar, esto significa que pued dar un valor negativo lo que quiere decir que el usuario esta endeudado.
				Saldo -= valor;
			}
		}

		/// <summary>
		/// Hace un cambio en el saldo.
		/// </summary>
		/// <param name="saldo">Saldo a cambiar, si es un valor negativo entonces se resta y si es un valor positivo se suma.</param>
		/// <returns>Devuelve un booleano que indica si se pudo realizar el cambio.</returns>
		public bool SaldoDelta(Real saldo)
		{
			//hacemos un bloqueo multihilo para evitar que se hagan multiples manipulaciones de dinero al tiempo.
			lock (_locker)
			{
				if (saldo == 0)
					// Si no ocurre ningun cambio entonces se devuelve true porque no ocurre ningun error.
					return true;
				else if (saldo > 0)
				{
					Saldo += saldo;
					return true;
				}
				else if (saldo < 0)
				{
					// Se valida que hacer el cambio no de un resultado negativo.
					if (Saldo - saldo >= 0)
					{
						Saldo -= saldo;
						return true;
					}
				}
			}
			return false;
		}
	}
}