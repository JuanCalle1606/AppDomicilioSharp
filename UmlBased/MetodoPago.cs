using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa un metodo de pago por cuotas que se usa para saber si un objeto ha sido pagado ya.
	/// </summary>
	public sealed class MetodoPago
	{
		/// <summary>
		/// Indica el numero de cuotas en las que se pagara este objeto.
		/// </summary>
		[DP] public byte CuotasTotales { get; init; }

		/// <summary>
		/// Indica el numero de cuotas pagadas actualmente.
		/// </summary>
		[DP] public byte CuotasPagadas { get; private set; } = 0;

		/// <summary>
		/// Indica el precio total del producto que se esta pagando.
		/// </summary>
		/// <remarks>
		/// Esta propiedad solo se usa para la conversión a string.
		/// </remarks>
		[DP] public Real PrecioTotal { get; init; }

		/// <summary>
		/// Define si el producto relacionado a este metodo de pago ya se ha terminado de pagar.
		/// </summary>
		public bool Finalizado => CuotasTotales == CuotasPagadas;

		public double CuotasFaltantes => CuotasTotales - CuotasPagadas;

		/// <summary>
		/// Indica que una cuota del procuto ha sido pagada.
		/// </summary>
		public void ProcesarCuota()
		{
			if (CuotasPagadas < CuotasTotales) CuotasPagadas++;
		}

		/// <inheritdoc/>
		public override string ToString() =>
			$"producto de {PrecioTotal}$ pagado a lo largo de {CuotasTotales} cuotas, actualmente pagado {CuotasPagadas} cuotas.";
	}

}

