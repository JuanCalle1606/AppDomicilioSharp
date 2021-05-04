using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Ventana que principal de un usuario de tipo comprador.
	/// </summary>
	partial class LinuxCompWindow : Window, IWindow
	{
		[UI] Image UserAvatar = null;

		[UI] ListBox ListaProductos = null;

		[UI] SearchEntry SearchInput = null;

		public LinuxCompWindow() : this(new Builder("LinuxCompWindow.glade")) { }

		private LinuxCompWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxCompWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;
			builder.Dispose();
		}

		/// <summary>
		/// Se ejecuta cuando el usuario la ventana.
		/// </summary>
		private void Window_DeleteEvent(object o, EventArgs args) =>
			Gtk.Application.Quit();

		/// <inheritdoc/>
		void IWindow.Show()
		{
			ConfigureFistShow();
			ShowAll();
		}
	}
}
