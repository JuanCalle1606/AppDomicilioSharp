using System;
using Gtk;
using KYLib.Extensions;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class CarritoDialog
	{
		[UI] Label SeleccionarAlert = null;

		[UI] Label TimeAgo = null;

		[UI] Label Precios = null;

		[UI] Box Detalles = null;

		[UI] SpinButton NoCantidad = null;

		[UI] SpinButton NoCuotas = null;

		void on_ListaPedidos_row_selected(object o, RowSelectedArgs args)
		{
			var widget = (ProductoWidget)args.Row.Child;
			var pedido = Carrito.Pedidos.Find(P => P.Producto.Equals(widget.Producto));

			//actualizamos propiedades
			ActualizarDetalles(pedido);

			SeleccionarAlert.Visible = false;
			Detalles.Visible = true;
		}

		private void ActualizarDetalles(Pedido pedido)
		{
			var diff = DateTime.Now - pedido.Fecha;
			var elapsed = TimeSpan.FromSeconds(Math.Round(diff.TotalSeconds));

			Precios.Text =
@"Precio base:
{0:C2}
Precio cuota:
{1:C2}
Precio total:
{2:C2}".Format(pedido.ValorBase, pedido.ValorCuota, pedido.ValorTotal);
			TimeAgo.Text = "Añadido hace: {0}".Format(elapsed);
			NoCantidad.Value = pedido.Cantidad;
			NoCuotas.Value = pedido.Cuotas;
		}
	}
}