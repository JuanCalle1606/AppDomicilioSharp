using System;
using Gtk;
using AppDomicilioSharp.Visual.Dialogs;

namespace AppDomicilioSharp
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			//Aqui debemos cargar las preferencias

			bool logged = false;

			Application.Init("Domicilios App", ref args);

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
