
using System;
using ICommon;
using UmlBased;

namespace Terminal
{
	public class TerminalFactory : IFactory
	{
		/// <summary>
		/// Devuelve el factory para crear app de consola.
		/// </summary>
		public static IFactory Default => new TerminalFactory();

		public IApp CreateApp() => new TerminalApp("Domicilios Sharp");

		public ILoginWindow CreateLoginWindow(Usuario DefaultUser)
		{
			Console.WriteLine("Creada la ventana de logeo");
			return null;
		}
	}
}