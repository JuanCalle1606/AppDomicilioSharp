using System;
using System.Collections.Generic;
using System.Linq;
using AppDomicilioSharp.ConsoleBased;
using AppDomicilioSharp.Visual.Dialogs;
using Gdk;
using Gtk;
using KYLib.MathFn;
using static KYLib.ConsoleUtils.Cons;

namespace AppDomicilioSharp
{
	class Program
	{
		[STAThread]
		public static int Main(string[] args)
		{
			//Aqui debemos cargar las preferencias

			//Validamos validamos algunos parametros de consola
			if (ValidateConsole(new(args), out var code))
				return code;

			try
			{
				Application.Init();
			}
			catch (Exception)
			{
				Console.Error.Write("Ha ocurrido un error al iniciar el motor grafico, si no tiene gtk+ por favor instalelo o inicie la aplicación en modo consola con -c");
				return 1;
			}

			var app = new Application("org.Kyt.AppDomicilioSharp", GLib.ApplicationFlags.None);
			app.Register(GLib.Cancellable.Current);

			Dialog winl = new LoginDialog();
			//Window winm = new Windows.MainWindow();

			app.AddWindow(winl);

			winl.Show();
			Application.Run();
			return 0;
		}

		private static bool ValidateConsole(List<string> args, out Int output)
		{
			args = new(args.Select(s => s.ToLower()));
			output = default;
			if (!string.IsNullOrEmpty(args.Find(s => (s == "-h" || s == "--help"))))
			{
				Trace("Mostrando ayuda");
				return true;
			}
			else if (!string.IsNullOrEmpty(args.Find(s => (s == "-c" || s == "--console"))))
			{
				Trace("Iniciando modo consola");
				Start<DomiciliosAppConsole>();
				return true;
			}
			return false;
		}
	}
}
