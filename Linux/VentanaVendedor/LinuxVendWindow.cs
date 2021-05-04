using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{

	[Author("Juan Carlos Arbelaez")]
	/// <summary>
	/// Venatna principal de los usuarios de tipo vendedor.
	/// </summary>
	class LinuxVendWindow : Window, IWindow
	{
		MenuDialogo Menu = null;


		public LinuxVendWindow() : this(new Builder("LinuxVendWindow.glade")) { }

		private LinuxVendWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxVendWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += OnWindowDestroy;
			builder.Dispose();
		}

		[Author("Juan Pablo Calle")]
		private void OnWindowDestroy(object o, EventArgs args) =>
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
