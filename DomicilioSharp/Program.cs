using System;
using System.Collections.Generic;
using ICommon;
using KYLib.ConsoleUtils;
using KYLib.Extensions;
using KYLib.MathFn;
using KYLib.System;
using Terminal;

namespace DomicilioSharp
{
	class Program
	{
		public static List<string> ArgsList;

		public static IApp App;

		public static IFactory Factory;

		static int Main(string[] args)
		{
			ArgsList = new(args);
			return RunApp(true);
		}

		public static Int RunApp(bool create)
		{
			// cargamos los datos guardados antes de cualquier cosa
			RestorePreferences();

			// debemos crear la app? esta propiedad siempre es true cuando el punto de inicio es este programa, si el punto de inicio es Windows.exe esto sera false.
			if (create)
			{
				var ec = CreateFactory();
				//si el codigo es diferente de 0 lo devolvemos.
				if (ec != 0)
					return ec;
			}

			App = Factory.CreateApp();

			App.AddWindow(Factory.CreateLoginWindow(null));

			return (App?.StartApp()).GetValueOrDefault(1);
		}

		private static Int CreateFactory()
		{
			//lo primero que validamos es si sera una aplicación de consola.
			if (ArgsList.Contains("-c"))
			{
				Factory = TerminalFactory.Default;
				return 0;
			}
			else if (Info.CurrentSystem.IsLinux() || ArgsList.Contains("--gtk"))
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

		/// <summary>
		/// Carga los datos desde los archivos.
		/// </summary>
		private static void RestorePreferences()
		{

		}
	}
}
