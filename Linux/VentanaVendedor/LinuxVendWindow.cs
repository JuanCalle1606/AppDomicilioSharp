using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{

	[Author("Juan Carlos Arbelaez")]
	/// <summary>
	/// Ventana principal de los usuarios de tipo vendedor.
	/// </summary>
	partial class LinuxVendWindow : Window, IWindow
	{
		MenuDialogo Menu = null;


		public LinuxVendWindow() : this(new Builder("LinuxVendWindow.glade")) { }

		private LinuxVendWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxVendWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;

			builder.Dispose();
		}

		[Author("Juan Pablo Calle")]
		private void Window_DeleteEvent(object o, EventArgs args) =>
			Application.Quit();

		void On_VerOrdenesBtn_clicked(object o, EventArgs args)
		{

		}

		void On_EditarMenuBtn_clicked(object o, EventArgs args)
		{
			Menu ??= new();
			Menu.Show();
		}
	}
}
