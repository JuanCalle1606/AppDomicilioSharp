using System;
using System.Linq;
using Gtk;
using ICommon;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Dialogo que muestra el carrito del comprador.
	/// </summary>
	partial class CarritoDialog : Dialog
	{
		/// <summary>
		/// Carrito vinculado a ese dialogo.
		/// </summary>
		public Carrito Carrito { get; private set; }

		/// <summary>
		/// Lista en la que se muestran los productos de los pedidos.
		/// </summary>
		[UI] ListBox ListaPedidos = null;

		/// <summary>
		/// Dialogo de mensaje para mostrar errores al usuario.
		/// </summary>
		[UI] MessageDialog ErrorMsg = null;

		/// <summary>
		/// Indica si se ha hecho la primera visualización del carrito.
		/// </summary>
		bool updated = false;

		/// <summary>
		/// Crea un nuevo dialogo vinculado a un carrito.
		/// </summary>
		public CarritoDialog(Carrito carrito) : this(new Builder("CarritoDialog.glade"), carrito) { }

		private CarritoDialog(Builder builder, Carrito carrito) : base(builder.GetRawOwnedObject("CarritoDialog"))
		{
			builder.Autoconnect(this);
			Carrito = carrito;
			DefaultResponse = ResponseType.Close;
			Response += Dialog_Deleted;
			DeleteEvent += Dialog_Deleted;
			ErrorMsg.DeleteEvent += (o, a) =>
			{
				a.RetVal = true;
				ErrorMsg.Hide();
			};

			builder.Dispose();
		}

		/// <summary>
		/// Realiza el pago del carrito.
		/// </summary>
		void on_PagarBtn_clicked(object o, EventArgs args)
		{

		}

		/// <summary>
		/// Previene que se destruya el carrito y borra la seleccion.
		/// </summary>
		private void Dialog_Deleted(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			Hide();
			ListaPedidos.UnselectAll();
		}

		/// <summary>
		/// Actualiza lo grafico en una primera vista y crea los handlers de eventos de carrito.
		/// </summary>
		public void Actualizar()
		{
			if (updated) return;
			updated = true;
			Carrito.Changed += OnCarritoChanged;
			ActualizarLista();
		}

		/// <summary>
		/// Cuando el estado del carrito cambia debemos actualizar lo visual y agregar o eleminar widgets de la vista.
		/// </summary>
		/// <param name="added">Indica si se ha agregado un elemento a la lista, false si ha sido eliminado.</param>
		/// <param name="content">Pedido agregado o elimiando, o null si el carrito solo se actualizo.</param>
		private void OnCarritoChanged(bool added, Pedido content)
		{
			CalcularSuma();
			if (added)
			{
				if (content == null)
				{
					var widget = ListaPedidos.SelectedRow?.Child as ProductoWidget;
					var pedido = Carrito.Contiene(widget?.Producto);
					if (pedido != null)
						ActualizarDetalles(pedido);
					return;
				}
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

		/// <summary>
		/// Sincroniza la lista grafica con la lista logica de pedidos.
		/// </summary>
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
			CalcularSuma();
		}
	}
}
