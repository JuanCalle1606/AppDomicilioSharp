#pragma warning disable CS0612
using Gdk;
using Gtk;
using ICommon;
using KYLib.System;
using KYLib.Utils;
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
			LoadTheme();
			return new LinuxApp("org.tori.domiciliosharp", GLib.ApplicationFlags.None);
		}

		private static void LoadTheme()
		{
			Assets.UpdateRelPath("Recursos");
			var prov = new CssProvider();
			prov.LoadFromPath(Assets.GetPath("wind.css"));
			var screen = Gdk.Display.Default.DefaultScreen;

			StyleContext.AddProviderForScreen(screen, prov, StyleProviderPriority.Application);

			IconTheme.AddBuiltinIcon("logo", (int)IconSize.Menu, new Pixbuf(Assets.GetPath("logo.svg")));

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