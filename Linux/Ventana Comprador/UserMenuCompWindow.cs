using System;
using UmlBased;

namespace Linux
{
	partial class LinuxCompWindow
	{
		/// <summary>
		/// Dialogo de acerca de.
		/// </summary>
		readonly AboutUsDialog AboutUs = new();

		/// <summary>
		/// Dialogo del carrito.
		/// </summary>
		CarritoDialog Carrito;

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
			Carrito?.Dispose();
			AboutUs?.Dispose();
			Dispose();
			var window = LinuxFactory.Default.CreateLoginWindow(null, LinuxFactory.loginaction) as LinuxLoginWindow;
			app.AddWindow(window);
			window.Show();
		}

		/// <summary>
		/// Abre el dialogo del carrito para que el usuario lo vea.
		/// </summary>
		void On_ViewCarrito_clicked(object o, EventArgs args)
		{
			var user = (Comprador)DomiciliosApp.ClienteActual;
			Carrito ??= new CarritoDialog(user.Carrito);
			Carrito.Actualizar();
			Carrito.Show();
		}

		void On_SearchBtn_clicked(object o, EventArgs args)
		{
			ClearProductos();
			var input = SearchInput.Text.ToLower();

			if (string.IsNullOrWhiteSpace(input))
			{
				ShowProductos();
				return;
			}
			ShowProductos(DomiciliosApp.Instance.Productos, input);
		}
	}
}