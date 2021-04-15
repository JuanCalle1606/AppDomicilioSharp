
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

		public ILoginWindow CreateLoginWindow(Usuario DefaultUser, Action onUserLogin)
		{
			if (DefaultUser == null)
			{
				var dev = new TerminalLoginWindow();
				dev.OnLogin = onUserLogin;
				return dev;
			}
			return null;
		}
	}
}