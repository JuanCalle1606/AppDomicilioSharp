using System;
using Gtk;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxCompWindow
	{
		AboutUsDialog AboutUs = new();

		CarritoDialog Carrito;

		/// <summary>
		/// Muesta el dialogo de acerca de.
		/// </summary>
		void on_UserMenuAbout_activate(object o, EventArgs args) =>
			AboutUs.Show();

		void on_ViewCarrito_clicked(object o, EventArgs args)
		{
			var user = (Comprador)DomiciliosApp.ClienteActual;
			Carrito ??= new CarritoDialog(user.Carrito);
			Carrito.Actualizar();
			Carrito.Show();
		}
	}
}