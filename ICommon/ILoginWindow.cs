using System;
using ICommon.Bases;

namespace ICommon
{
	/// <summary>
	/// Ventana de inicio de sesión.
	/// </summary>
	public interface ILoginWindow : IWindow
	{
		/// <summary>
		/// Pagina de inicio de sesión.
		/// </summary>
		IPage LoginPage { get; }

		/// <summary>
		/// Pagina para registrarse.
		/// </summary>
		IPage RegisterPage { get; }

		/// <summary>
		/// Accion que sera ejecutada luego de que se inicie sesión.
		/// </summary>
		Action OnLogin { get; set; }
	}
}