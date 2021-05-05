using System;
using ICommon;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{

	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Ventana principal de los usuarios de tipo vendedor.
	/// </summary>
	partial class LinuxVendWindow
	{
		/// <summary>
		/// Dialogo de acerca de.
		/// </summary>
		readonly AboutUsDialog AboutUs = new();

		/// <summary>
		/// Muesta el dialogo de acerca de.
		/// </summary>
		void On_UserMenuAbout_activate(object o, EventArgs args) =>
			AboutUs.Show();

		void On_UserMenuClose_activate(object o, EventArgs args)
		{
			Hide();
			Gtk.Application app = Application;
			app.RemoveWindow(this);
			AboutUs?.Dispose();
			Dispose();
			var window = LinuxFactory.Default.CreateLoginWindow(null, LinuxFactory.loginaction) as LinuxLoginWindow;
			app.AddWindow(window);
			window.Show();
		}
	}
}