using System;
using AppDomicilioSharp.Visual.Dialogs;
using Gtk;
using KYLib.System;

namespace AppDomicilioSharp
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			//Aqui debemos cargar las preferencias
			Console.Write($"Running on {Info.CurrentSystem}");

			Application.Init();

			var app = new Application("org.Kyt.AppDomicilioSharp", GLib.ApplicationFlags.None);
			app.Register(GLib.Cancellable.Current);

			Dialog winl = new LoginDialog();
			//Window winm = new Windows.MainWindow();

			app.AddWindow(winl);

			winl.Show();
			Application.Run();
		}
	}
}
