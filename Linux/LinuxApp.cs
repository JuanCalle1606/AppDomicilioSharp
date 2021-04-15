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