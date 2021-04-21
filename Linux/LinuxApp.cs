using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using KYLib.MathFn;

namespace Linux
{
	public class LinuxApp : Application, IApp
	{
		public LinuxApp(string application_id, GLib.ApplicationFlags flags) : base(application_id, flags)
		{
			Register(GLib.Cancellable.Current);
			Utils.App = this;
			Activated += AppActivated;
		}

		private void AppActivated(object sender, EventArgs e)
		{
			//este metodo lo implementamos para evitar errores pero no hace nada
		}

		public bool AddWindow(IWindow window)
		{
			Window win = window as Window;
			if (win == null) return false;
			AddWindow(win);
			return true;
		}

		public Int StartApp()
		{
			Application.Run();
			return 0;
		}
	}
}