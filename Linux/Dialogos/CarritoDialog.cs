using System;
using System.Linq;
using Gtk;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class CarritoDialog : Dialog
	{
		public Carrito Carrito { get; private set; }

		[UI] ListBox ListaPedidos = null;

		bool updated = false;

		public CarritoDialog(Carrito carrito) : this(new Builder("CarritoDialog.glade"), carrito) { }

		private CarritoDialog(Builder builder, Carrito carrito) : base(builder.GetRawOwnedObject("CarritoDialog"))
		{
			builder.Autoconnect(this);
			Carrito = carrito;
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
			if (updated) return;
			updated = true;
			Carrito.Changed += OnCarritoChanged;
			ActualizarLista();
		}

		private void OnCarritoChanged(bool added, Pedido content)
		{
			if (added)
			{
				ListaPedidos.Add(new ProductoWidget(content.Producto));
			}
			else
			{
				var current = from list in ListaPedidos.Children
							  where (((ListBoxRow)list).Child as ProductoWidget).Producto == content.Producto
							  select list;
				foreach (ListBoxRow item in current)
				{
					using (item)
					{
						ListaPedidos.Remove(item);
						item.Child.Dispose();
					}
				}
			}
		}

		private void ActualizarLista()
		{
			var origin = from car in Carrito.Pedidos select car.Producto;
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
