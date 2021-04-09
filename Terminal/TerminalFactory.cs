
using System;
using ICommon;

namespace Terminal
{
	public class TerminalFactory : IFactory
	{
		/// <summary>
		/// Devuelve el factory para crear app de consola.
		/// </summary>
		public static IFactory Default() => new TerminalFactory();

		public IApp CreateApp()
		{
			throw new NotImplementedException();
		}

		public ILoginWindow CreateLoginWindow()
		{
			throw new NotImplementedException();
		}
	}
}