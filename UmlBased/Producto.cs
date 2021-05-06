using System;
using System.Collections.Generic;
using KYLib.Interfaces;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
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
		[DP] public Int Id { get; init; }

		/// <summary>
		/// Nombre descriptivo de este producto.
		/// </summary>
		[DP("Nombre")] public string Name { get; init; }//Nota: esta propiedad se llama Name en lugar de Nombre porque asi la define la interfaz

		/// <summary>
		/// Descripcion sobre este producto.
		/// </summary>
		[DP] public string Descripcion { get; init; }

		/// <summary>
		/// Valor original de este producto, los establecimientos pueden cambiarlo cuando lo deseen.
		/// </summary>
		[DP] public Real Precio { get; set; }

		/// <summary>
		/// Valor del domicilio de este producto, los establecimientos tambien definen este valor y lo pueden cambiar, un valor de 0 indica que es un domicilio gratis.
		/// </summary>
		[DP] public Real ValorDomicilio { get; set; }

		/// <summary>
		/// Lista de calificaciones que le han dado a este producto.
		/// </summary>
		[DP] private List<Small> calificacion = new();

		/// <summary>
		/// Promedio de las calificaciones.
		/// </summary>
		/*TODO: Cambiar el tipo de Real a Float*/
		public Real Calificacion => calificacion.Count == 0 ? 0 : Math.Round(Mathf.MeanOf<Small, Real>(calificacion), 1);

		/// <summary>
		/// URL a una foto de internet que se usa como imagen del producto.
		/// </summary>
		[DP] public string Foto { get; init; }

		/// <summary>
		/// Fecha en la que fue creado este producto.
		/// </summary>
		[DP] public DateTime FechaCreacion { get; init; }

		/// <summary>
		/// Indica si este producto puede ser pagado por cuotas o no.
		/// </summary>
		[DP] public bool PermiteCuotas { get; set; }

		/// <summary>
		/// Agrega una puntuacion a este producto.
		/// </summary>
		/// <param name="puntuacion">Calificacion a agregar.</param>
		public void Calificar(Small puntuacion) => calificacion.Add(puntuacion);

		/// <summary>
		/// Guarda el id del vendedor que tiene ofrece este producto.
		/// </summary>
		[DP] public Int OwnerId { get; init; }

		/// <summary>
		/// Devuelve el vendedor que esta vendiendo este producto.
		/// </summary>
		public Vendedor ObtenerVendedor() =>
			DomiciliosApp.ObtenerVendedor(OwnerId);

	}
}