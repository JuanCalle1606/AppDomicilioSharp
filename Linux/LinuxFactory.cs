using System;
using Gtk;
using ICommon;
using UmlBased;

namespace Linux
{
	public class LinuxFactory : IFactory
	{
		/// <summary>
		/// Devuelve el factory para crear app de gtk.
		/// </summary>
		public static IFactory Default => new LinuxFactory();

		public IApp CreateApp()
		{
			Application.Init();
			return new LinuxApp("org.tori.domiciliosharp", GLib.ApplicationFlags.None);
		}

		public ILoginWindow CreateLoginWindow(Usuario DefaultUser, System.Action onUserLogin)
		{
			if (DefaultUser == null)
			{
				var dev = new LinuxLoginWindow();
				dev.OnLogin = () =>
				{
					dev.Dispose();
					onUserLogin?.Invoke();
				};
				return dev;
			}
			return null;
		}
	}
}