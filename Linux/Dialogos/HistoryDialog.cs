using System;
using System.Linq;
using Gtk;
using ICommon;
using KYLib.Extensions;
using KYLib.MathFn;
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

		[UI] Label OtrosPrecios = null;

		[UI] Label Cantidad = null;

		[UI] SpinButton Cuotas = null;

		[UI] Button AbonarBtn = null;

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

		void On_CancelarBtn_clicked(object o, EventArgs args)
		{
			if (DomiciliosApp.ClienteActual is not Comprador user) return;
			var row = ListaPedidos.SelectedRow;
			var widget = row.Child as ProductoWidget;
			var producto = widget.Producto;
			var pedido = user.HistorialPedidos[row.Index];

			producto.ObtenerVendedor().Cancelar(pedido);
			MostrarDetalles(pedido);
		}

		void On_AbonarBtn_clicked(object o, EventArgs args)
		{
			if (DomiciliosApp.ClienteActual is not Comprador user) return;
			var row = ListaPedidos.SelectedRow;
			var pedido = user.HistorialPedidos[row.Index];

			user.Abonar(pedido, (Small)Cuotas.ValueAsInt);
			MostrarDetalles(pedido);
		}

		private void MostrarDetalles(Pedido pedido)
		{
			TimeAgo.Text = "Comprado el {0:D2}/{1:D2}/{2}".
			Format(pedido.Fecha.Day, pedido.Fecha.Month, pedido.Fecha.Year);

			string domstr = pedido.ValorDomicilio == 0 ? "" :
@"Precio domicilio:
{0:C2}
".
			Format(pedido.ValorDomicilio);

			Precios.Text =
@"Precio base:
{0:C2}
{3}Precio cuota:
{1:C2}
Precio total:
{2:C2}".
			Format(pedido.ValorBase, pedido.ValorCuota, pedido.ValorTotal, domstr);

			OtrosPrecios.Text =
@"IVA aplicado:
{0:P0}
Descuento de usuario:
{1:P1}
Aditivo por cuotas:
{2:C2}".
			Format(pedido.PorcentajeIVA - 1, 1 - pedido.PorcentajeDescuento, pedido.ValorAditivo);

			Cantidad.Text = "Cantidad: {0}".Format(pedido.Cantidad);

			Estado.Text = "Estado: {0}".
			Format(pedido.Estado);

			NoCuotas.Text = "Has pagado {0} de {1} cuotas".Format(pedido.Pago.CuotasPagadas, pedido.Pago.CuotasTotales);

			CuotasInfo.Visible = pedido.Cuotas > 1;
			//pedido.Producto.PermiteCuotas && !pedido.Pago.Finalizado;
			if (pedido.Cuotas > 1)
			{
				Cuotas.Adjustment.Upper = pedido.Pago.CuotasFaltantes;
				Cuotas.Visible = !pedido.Pago.Finalizado;
				AbonarBtn.Visible = !pedido.Pago.Finalizado;
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
