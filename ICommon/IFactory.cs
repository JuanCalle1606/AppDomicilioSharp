using System;
using KYLib.MathFn;
using UmlBased;

namespace ICommon
{
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
		ILoginWindow CreateLoginWindow(Cuenta DefaultUser);


	}
}
