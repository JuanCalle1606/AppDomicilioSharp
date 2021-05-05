using System;
using System.Linq;
using Gtk;
using ICommon;
using KYLib.Extensions;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	class HistoryDialog : Dialog
	{
		[UI] Label TimeAgo = null;

		[UI] Box Detalles = null;

		[UI] Label SeleccionarAlert = null;

		[UI] ListBox ListaPedidos = null;

		[UI] Box CuotasInfo = null;

		[UI] Label NoCuotas = null;

		[UI] Label Precios = null;

		[UI] Label Estado = null;

		public HistoryDialog() : this(new Builder("HistoryDialog.glade")) { }

		private HistoryDialog(Builder builder) : base(builder.GetRawOwnedObject("HistoryDialog"))
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Cancel;
			Response += Dialog_Response;
			DeleteEvent += Dialog_Response;
			builder.Dispose();
		}

		void On_ListaPedidos_row_selected(object o, RowSelectedArgs args)
		{
			if (args.Row == null)
			{
				Detalles.Visible = false;
				SeleccionarAlert.Visible = true;
				return;
			}
			var user = DomiciliosApp.ClienteActual as Comprador;
			MostrarDetalles(user.HistorialPedidos[args.Row.Index]);

			Detalles.Visible = true;
			SeleccionarAlert.Visible = false;
		}

		private void MostrarDetalles(Pedido pedido)
		{
			TimeAgo.Text = "Comprado el {0:D2}/{1:D2}/{2}".
			Format(pedido.Fecha.Day, pedido.Fecha.Month, pedido.Fecha.Year);

			Precios.Text =
@"Precio base:
{0:C2}
Precio domicilio:
{3:C2}
IVA aplicado:
{4:P0}
Descuento de usuario:
{5:P1}
Aditivo por cuotas:
{6:C2}
Precio cuota:
{1:C2}
Precio total:
{2:C2}".
			Format(pedido.ValorBase, pedido.ValorCuota, pedido.ValorTotal, pedido.ValorDomicilio, pedido.PorcentajeIVA - 1, 1 - pedido.PorcentajeDescuento, pedido.ValorAditivo);

			Estado.Text = "Estado: {0}".
			Format(pedido.Estado);

			NoCuotas.Text = "Has pagado {0} de {1} cuotas".Format(pedido.Pago.CuotasPagadas, pedido.Pago.CuotasTotales);

			CuotasInfo.Visible = pedido.Producto.PermiteCuotas;
			if (pedido.Producto.PermiteCuotas)
			{

			}
		}

		public void Actualizar()
		{
			var user = DomiciliosApp.ClienteActual as Comprador;
			var diff = user.HistorialPedidos.Count - ListaPedidos.Children.Length;
			if (diff == 0) return;
			var count = ListaPedidos.Children.Length;

			foreach (var item in user.HistorialPedidos.Skip(count))
				ListaPedidos.Add(new ProductoWidget(item.Producto));
			ListaPedidos.UnselectAll();
		}

		private void Dialog_Response(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			Hide();
			ListaPedidos.UnselectAll();
		}
	}
}
