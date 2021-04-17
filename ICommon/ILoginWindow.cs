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
		/// Accion que sera ejecutada luego de que se inicie sesión.
		/// </summary>
		Action OnLogin { get; set; }
	}
}