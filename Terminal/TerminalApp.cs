using System;
using ICommon;
using ICommon.Bases;
using KYLib.ConsoleUtils;
using KYLib.MathFn;
using UmlBased;

namespace Terminal
{
	public class TerminalApp : ConsoleApp, IApp
	{
		public TerminalApp(string appName) : base(appName)
		{ }

		public bool AddWindow(IWindow window)
		{

			return true;
		}

		public Int StartApp()
		{
			if (DomiciliosApp.ClienteActual != null)
				Start();
			return 0;
		}
	}
}