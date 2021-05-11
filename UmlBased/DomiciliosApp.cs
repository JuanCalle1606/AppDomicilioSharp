using System;
using System.Collections.Generic;
using KYLib.Extensions;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Clase que guarda todos los objetos globales.
	/// </summary>
	public class DomiciliosApp
	{
		/// <summary>
		/// Intancia global de una aplicación de domicilio.
		/// </summary>
		public static DomiciliosApp Instance { get; private set; }

		/// <summary>
		/// Constructor singleton
		/// </summary>
		public DomiciliosApp()
		{
			if (Instance == null)
				Instance = this;
			else
				throw new InvalidOperationException("No se puede crear otra instancia de la clase DomiciliosApp si ya existe una.");
		}

		/// <summary>
		/// Guarda la siguiente id que sera usada para crear un usuario.
		/// </summary>
		[DP] public Int NextUserId = 0;

		[DP] public DateTime UltimoPago = DateTime.Now;

		/// <summary>
		/// Guarda la siguiente id que sera usada para crear un pedido.
		/// </summary>
		[DP] public Int NextPedId = 0;

		/// <summary>
		/// Lista de Productos que guarda a todos los productos que han sido creados.
		/// </summary>
		[DP] public List<Producto> Productos = new();

		/// <summary>
		/// Lista de Compradores que guarda a todos los usuarios de tipo comprador.
		/// </summary>
		[DP] public List<Comprador> Compradores = new();

		/// <summary>
		/// Lista de Vendedores que guarda a todos los usuarios de tipo Vendedor.
		/// </summary>
		[DP] public List<Vendedor> Vendedores = new();

		/// <summary>
		/// Aqui se guarda el usuario que esta logeado actualmente.
		/// </summary>
		public static Usuario ClienteActual { get; set; }

		/*
		/// <summary>
		/// Busca entre todas las cuentas una que tenga ese id.
		/// </summary>
		/// <param name="id">Id de la cuenta a buscar.</param>
		public static Usuario ObtenerCuenta(Int id) =>
			(Usuario)ObtenerComprador(id) ?? ObtenerVendedor(id);
		*/

		/// <summary>
		/// Obtiene una cuenta de comprador que tenga una id dada.
		/// </summary>
		/// <param name="id">Id a buscar.</param>
		public static Comprador ObtenerComprador(Int id) =>
			Instance.Compradores.Find(C => C.Id.Equals(id));

		/// <summary>
		/// Obtiene una cuenta de vendedor que tenga una id dada.
		/// </summary>
		/// <param name="id">Id a buscar.</param>
		public static Vendedor ObtenerVendedor(Int id) =>
			Instance.Vendedores.Find(C => C.Id.Equals(id));

		public static void ProcesarDia() =>
			Instance.Compradores.ForEach(p => p.PagarCuotas());
	}
}