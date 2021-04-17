using System;
using ICommon.Bases;
using KYLib.MathFn;
using UmlBased;

namespace ICommon
{
	/// <summary>
	/// Representa una instancia que se usa para crear aplicaciones y ventanas.
	/// </summary>
	public interface IFactory
	{
		/// <summary>
		/// Crea una nueva aplicación para trabajar en la plataforma de este <see cref="IFactory"/>.
		/// </summary>
		IApp CreateApp();

		/// <summary>
		/// Crea la ventana en la que el usuario debe logearse.
		/// </summary>
		/// <param name="DefaultUser">Cuenta por defecto a utilizar o null si el usuario debe ingresar credenciales.</param>
		/// <param name="onUserLogin">Action que deberia ser ejecutada cuando se inicie sesión</param>
		ILoginWindow CreateLoginWindow(Usuario DefaultUser, Action onUserLogin);

		/// <summary>
		/// Crea la ventana que generalmente usa el comprador.
		/// </summary>
		IWindow CreateCompWindow();

		/// <summary>
		/// Crea la ventana que generalmente usa el vendedor.
		/// </summary>
		IWindow CreateVendWindow();
	}
}
