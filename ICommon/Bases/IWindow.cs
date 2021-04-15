using System;
using KYLib.Interfaces;
using KYLib.MathFn;

namespace ICommon.Bases
{
	/// <summary>
	/// Una ventana del sistema.
	/// </summary>
	public interface IWindow : INameable
	{
		/// <summary>
		/// Muestra la ventana.
		/// </summary>
		void Show();
	}
}
