using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using KYLib.MathFn;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Aplicación gtk.
	/// </summary>
	public class LinuxApp : Application, IApp
	{
		/// <summary>
		/// Intancia una nueva aplicación gtk.
		/// </summary>
		/// <param name="application_id">ID unica de la aplicación.</param>
		/// <param name="flags">Flags de la aplicación.</param>
		public LinuxApp(string application_id, GLib.ApplicationFlags flags) : base(application_id, flags)
		{
			// Registramos la app
			Register(GLib.Cancellable.Current);
			Utils.App = this;
			Activated += AppActivated;
		}

		/// <summary>
		/// Función que se ejecuta cuando se le da click a la notificación.
		/// </summary>
		private void AppActivated(object sender, EventArgs e)
		{
			//este metodo lo implementamos para evitar errores pero no hace nada
		}

		/// <inheritdoc/>
		public bool AddWindow(IWindow window)
		{
			// la ventana solo se agrea si es distinta de null.
			Window win = window as Window;
			if (win == null) return false;
			AddWindow(win);
			return true;
		}

		/// <inheritdoc/>
		public Int StartApp()
		{
			Application.Run();
			return 0;
		}
	}
}