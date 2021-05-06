using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using Linux.Widgets;
using UmlBased;
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

		[UI] ListBox ListaPedidos = null;

		public LinuxVendWindow() : this(new Builder("LinuxVendWindow.glade")) { }

		private LinuxVendWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxVendWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;
			First();
			builder.Dispose();
		}

		[Author("Juan Pablo Calle")]
		private void First()
		{
			if (DomiciliosApp.ClienteActual is not Vendedor user) return;
			// en la aprte de inicio solo mostramos los ultimos 5 pedidos
			Pedido[] pedidos;
			if (user.Pedidos.Count <= 5) pedidos = user.Pedidos.ToArray();
			else pedidos = user.Pedidos.ToArray()[^5..^1];
			foreach (var item in pedidos)
			{
				ListaPedidos.Add(new ProductoWidget(item.Producto));
			}
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
