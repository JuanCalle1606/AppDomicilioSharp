using System;
using System.Linq;
using Gtk;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class CarritoDialog : Dialog
	{

		[UI] ListBox ListaPedidos = null;

		public CarritoDialog() : this(new Builder("CarritoDialog.glade")) { }

		private CarritoDialog(Builder builder) : base(builder.GetRawOwnedObject("CarritoDialog"))
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Close;
			Response += Dialog_Response;
			DeleteEvent += Dialog_Deleted;

			builder.Dispose();
		}

		private void Dialog_Deleted(object o, DeleteEventArgs args)
		{
			args.RetVal = true;
			Hide();
		}

		void Dialog_Response(object o, ResponseArgs args) => Hide();

		public void Actualizar()
		{
			var user = (Comprador)DomiciliosApp.ClienteActual;
			ActualizarLista(user);
		}

		private void ActualizarLista(Comprador user)
		{
			var origin = from car in user.Carrito.Pedidos select car.Producto;
			var current = from list in ListaPedidos.Children
						  select (((ListBoxRow)list).Child as ProductoWidget).Producto;

			var equals = origin.SequenceEqual(current);
			if (!equals)
			{
				foreach (var item in origin)
				{
					ListaPedidos.Add(new ProductoWidget(item));
				}
			}
		}
	}
}
