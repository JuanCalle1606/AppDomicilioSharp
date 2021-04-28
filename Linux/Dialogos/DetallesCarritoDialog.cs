using System;
using Gtk;
using KYLib.Extensions;
using KYLib.MathFn;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class CarritoDialog
	{
		[UI] Label SeleccionarAlert = null;

		[UI] Label TimeAgo = null;

		[UI] Label PrecioCuota = null;

		[UI] Label PrecioTotal = null;

		[UI] Label Precios = null;

		[UI] Box Detalles = null;

		[UI] SpinButton NoCantidad = null;

		[UI] SpinButton NoCuotas = null;

		void on_ListaPedidos_row_selected(object o, RowSelectedArgs args)
		{
			if (args.Row == null)
			{
				SeleccionarAlert.Visible = true;
				Detalles.Visible = false;
				return;
			}

			var widget = args.Row.Child as ProductoWidget;
			var pedido = Carrito.Contiene(widget.Producto);

			//actualizamos propiedades
			ActualizarDetalles(pedido);

			SeleccionarAlert.Visible = false;
			Detalles.Visible = true;
		}

		void on_ApplyBtn_clicked(object o, EventArgs args)
		{
			Small cantidad = (Small)NoCantidad.ValueAsInt;
			Small cuotas = (Small)NoCuotas.ValueAsInt;
			var widget = ListaPedidos.SelectedRow.Child as ProductoWidget;
			var pedido = Carrito.Contiene(widget.Producto);
			if (pedido.Cantidad != cantidad || pedido.Cuotas != cuotas)
			{
				pedido.Actualizar(cantidad, cuotas);
				Carrito.Agregar(pedido);
				ActualizarDetalles(pedido);
			}
		}

		void on_RemoveBtn_clicked(object o, EventArgs args)
		{
			var widget = ListaPedidos.SelectedRow.Child as ProductoWidget;
			var pedido = Carrito.Contiene(widget.Producto);

			if (!Carrito.Eliminar(pedido))
			{
				ErrorMsg.Text = "No ha sido posible eliminar el pedido";
				ErrorMsg.SecondaryText = "Seguramente ya ha sido eliminado. Si el problema persiste reinicia la app";
				ErrorMsg.Run();
				ErrorMsg.Hide();
			}
		}

		private void ActualizarDetalles(Pedido pedido)
		{
			var diff = DateTime.Now - pedido.Fecha;
			var elapsed = TimeSpan.FromSeconds(Math.Round(diff.TotalSeconds));

			Precios.Text =
@"Precio base:
{0:C2}
Percio domicilio:
{3:C2}
Precio cuota:
{1:C2}
Precio total:
{2:C2}".
			Format(pedido.ValorBase, pedido.ValorCuota, pedido.ValorTotal, pedido.ValorDomicilio);
			TimeAgo.Text = "Añadido hace: {0}".Format(elapsed);
			NoCantidad.Value = pedido.Cantidad;
			NoCuotas.Value = pedido.Cuotas;
		}

		void CalcularSuma()
		{
			PrecioCuota.Text = "Precio a pagar: {0:C2}".Format(Carrito.CostoCuota);
			PrecioTotal.Text = "Precio total: {0:C2}".Format(Carrito.Total);
		}
	}
}