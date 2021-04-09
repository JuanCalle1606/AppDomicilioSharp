using System;
using System.Collections.Generic;
using ICommon;
using KYLib.ConsoleUtils;
using KYLib.Extensions;
using KYLib.MathFn;
using KYLib.System;

namespace DomicilioSharp
{
	class Program
	{
		public static List<string> ArgsList;

		public static IApp App;

		static int Main(string[] args)
		{
			ArgsList = new(args);
			return RunApp(true);
		}

		public static Int RunApp(bool create)
		{
			// argumento que indica que nuestra app sera por consola
			if (ArgsList.Contains("-c"))
			{
				return Terminal.Program.Main();
			}
			// debemos crear la app? esta propiedad siempre es true cuando el punto de inicio es este programa, si el punto de inicio es Windows.exe esto sera false.
			if (create)
			{
				var ec = CreateApp();
				//si el codigo es diferente de 0 lo devolvemos.
				if (ec != 0)
					return ec;
			}

			return App.Start();
		}

		private static Int CreateApp()
		{
			if (Info.CurrentSystem.IsLinux() || ArgsList.Contains("--gtk"))
			{
				//creamos la aplicacion con Gtk;
				return 0;
			}
			//esto deberia entrar unicamente cuando se buildea en windows y el usuario abre la aplicación con DomicilioSharp.exe en lugar de con Windows.exe, esto es valido pero no recomendable ya que lo que haremos es invocar el proceso por consola lo que ocacionara que se consuma la memoria de 2 procesos.
			else if (Info.CurrentSystem.IsWindows())
			{
				//invocar Windows.exe
				return 0;
			}

			Cons.Error = "El sistema operativo actual no esta soportado, si este sistema tiene instalado gtk ejecute este programa con el parametro \"--gtk\" para usar ese motor grafico o utilize \"-c\" para usar la aplicación por consola.";
			return 1;
		}
	}
}
