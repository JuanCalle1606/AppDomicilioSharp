using System;
using UmlBased;

namespace Linux
{
	partial class LinuxCompWindow
	{
		/// <summary>
		/// Dialogo de acerca de.
		/// </summary>
		AboutUsDialog AboutUs = new();

		/// <summary>
		/// Dialogo del carrito.
		/// </summary>
		CarritoDialog Carrito;

		/// <summary>
		/// Muesta el dialogo de acerca de.
		/// </summary>
		void on_UserMenuAbout_activate(object o, EventArgs args) =>
			AboutUs.Show();

		/// <summary>
		/// Abre el dialogo del carrito para que el usuario lo vea.
		/// </summary>
		void on_ViewCarrito_clicked(object o, EventArgs args)
		{
			var user = (Comprador)DomiciliosApp.ClienteActual;
			Carrito ??= new CarritoDialog(user.Carrito);
			Carrito.Actualizar();
			Carrito.Show();
		}
	}
}