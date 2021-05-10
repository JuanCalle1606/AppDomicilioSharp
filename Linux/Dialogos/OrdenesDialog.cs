using System;
using Gtk;
using KYLib.Extensions;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class OrdenesDialog : Dialog
	{
		[UI] ListBox ListaPedidos = null;

		[UI] Label SeleccionarAlert = null;

		[UI] Label Datos = null;

		[UI] Label TimeAgo = null;

		[UI] Box Detalles = null;

		public OrdenesDialog() : this(new Builder("OrdenesDialog.glade")) { }

		private OrdenesDialog(Builder builder) : base(builder.GetObject("OrdenesDialog").Handle)
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Cancel;
			DeleteEvent += Dialog_Response;
			Response += Dialog_Response;
			MostrarPedidos();
		}

		void On_ListaPedidos_row_selected(object o, RowSelectedArgs args)
		{
			if (args.Row == null)
			{
				Detalles.Visible = false;
				SeleccionarAlert.Visible = true;
				return;
			}
			var user = DomiciliosApp.ClienteActual as Vendedor;
			var pedido = user.Pedidos[args.Row.Index];
			MostrarDetalles(pedido);

			Detalles.Visible = true;
			SeleccionarAlert.Visible = false;
		}

		void MostrarDetalles(Pedido pedido)
		{
			var diff = DateTime.Now - pedido.Fecha;
			var elapsed = TimeSpan.FromSeconds(Math.Round(diff.TotalSeconds));
			TimeAgo.Text = "Añadido hace: {0}".Format(elapsed);
			var comp = pedido.ObtenerComprador();
			Datos.Text =
@"Dirección: {0}
Telefono: {1}".
			Format(comp.Direccion, comp.Telefono);
		}

		private void MostrarPedidos()
		{
			var user = DomiciliosApp.ClienteActual as Vendedor;
			foreach (var item in user.Pedidos)
			{
				ListaPedidos.Add(new ProductoWidget(item.Producto));
			}
		}

		private void Dialog_Response(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			Hide();
			ListaPedidos.UnselectAll();
		}
	}
}
