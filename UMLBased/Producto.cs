using System;
using System.Collections.Generic;
using KYLib.Extensions;
using KYLib.Interfaces;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace AppDomicilioSharp.UMLBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Clase que representa un producto que se vende.
	/// </summary>
	public sealed class Producto : INameable
	{
		/// <summary>
		/// Identificador unico de este producto.
		/// </summary>
		[DP] public long Id { get; init; }

		/// <summary>
		/// Nombre descriptivo de este producto.
		/// </summary>
		[DP] public string Name { get; init; }//Nota: esta propiedad se llama Name en lugar de Nombre porque asi la define la interfaz

		/// <summary>
		/// Descripcion sobre este producto.
		/// </summary>
		[DP] public string Descripcion { get; init; }

		/// <summary>
		/// Valor original de este producto, los establecimientos pueden cambiarlo cuando lo deseen.
		/// </summary>
		[DP] public double Precio { get; set; }

		/// <summary>
		/// Valor del domicilio de este producto, los establecimientos tambien definen este valor y lo pueden cambiar, un valor de 0 indica que es un domicilio gratis.
		/// </summary>
		[DP] public double Domicilio { get; set; }

		/// <summary>
		/// Lista de calificaciones que le han dado a este producto.
		/// </summary>
		[DP] private List<byte> Calificacion { get; } = new();

		public byte CalificacionPromedio => (byte)Mathf.Mean(Calificacion.ToArray(b => (int)b));

		/// <summary>
		/// URL a una foto de internet que se usa como imagen del producto.
		/// </summary>
		[DP] public string Foto { get; init; }

		/// <summary>
		/// Fecha en la que fue creado este producto.
		/// </summary>
		[DP] public DateTime Creacion { get; init; }

		/// <summary>
		/// Indica si este producto puede ser pagado por cuotas o no.
		/// </summary>
		[DP] public bool PermiteCuotas { get; set; }
	}
}