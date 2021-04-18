using System;
using System.IO;
using GLib;
using Gtk;
using ICommon.Bases;
using Linux.Widgets;
using UmlBased;
using Pix = Gdk.Pixbuf;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxCompWindow : Window, IWindow
	{
		[UI] Image UserAvatar = null;

		[UI] ListBox ListaProductos = null;

		public LinuxCompWindow() : this(new Builder("LinuxCompWindow.glade")) { }

		private LinuxCompWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxCompWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;

			var foto = DomiciliosApp.ClienteActual.Foto;
			if (!string.IsNullOrWhiteSpace(foto) && File.Exists(foto))
			{
				if (Pix.GetFileInfo(foto, out _, out _) != null)
				{
					using (var temp = new Pix(foto))
					{
						UserAvatar.Pixbuf = temp.ScaleSimple(20, 20, Gdk.InterpType.Tiles);
					}
				}
			}
			for (int i = 0; i < 10; i++)
			{
				ListaProductos.Add(new ProductoWidget());
			}
		}

		/// <summary>
		/// Se ejecuta cuando el usuario la ventana.
		/// </summary>
		private void Window_DeleteEvent(object o, EventArgs args) =>
			Gtk.Application.Quit();

		/// <inheritdoc/>
		void IWindow.Show() => ShowAll();
	}
}
