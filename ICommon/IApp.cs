using System;
using ICommon.Bases;
using KYLib.MathFn;

namespace ICommon
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Representa un administrados de aplicación que guarda ventanas dentro de si.
	/// </summary>
	public interface IApp
	{
		/// <summary>
		/// Agrega una ventana a la aplicación.
		/// </summary>
		/// <param name="window">Ventana a agregar.</param>
		/// <returns>Devuelve si se pudo o no agregar la ventana.</returns>
		bool AddWindow(IWindow window);

		/// <summary>
		/// Inicia el bucle de la aplicación haciendo que se muestren sus ventanas.
		/// </summary>
		/// <returns>Codigo de salida de la aplciación.</returns>
		Int StartApp();
	}
}
