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
		/// <summary>
		/// Muestra un mensaje cuando .
		/// </summary>
		[UI] Label SeleccionarAlert = null;

		/// <summary>
		/// Muestra información de hace cuanto se agrego ese pedido al carro.
		/// </summary>
		[UI] Label TimeAgo = null;

		/// <summary>
		/// Muestra el precio del primer pago.
		/// </summary>
		[UI] Label PrecioCuota = null;

		/// <summary>
		/// Muestra el precio total del carrito.
		/// </summary>
		[UI] Label PrecioTotal = null;

		/// <summary>
		/// Muestra toda la información de precios.
		/// </summary>
		[UI] Label Precios = null;

		/// <summary>
		/// Contiene todo el contenido principal del panel de detalles.
		/// </summary>
		[UI] Box Detalles = null;

		/// <summary>
		/// Selector de la cantidad de productos del pedido.
		/// </summary>
		[UI] SpinButton NoCantidad = null;

		/// <summary>
		/// Selector del de cuotas del pedido.
		/// </summary>
		[UI] SpinButton NoCuotas = null;

		/// <summary>
		/// Muestra el panel de detalles y actualiza la información
		/// </summary>
		void On_ListaPedidos_row_selected(object o, RowSelectedArgs args)
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

		/// <summary>
		/// Aplica los cambios hechos en la cantidad o numero de cuotas del pedido.
		/// </summary>
		void On_ApplyBtn_clicked(object o, EventArgs args)
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

		/// <summary>
		/// Remueve un pedido del carrito.
		/// </summary>
		void On_RemoveBtn_clicked(object o, EventArgs args)
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

		/// <summary>
		/// Actualiza el panel de dettales con información de un pedido
		/// </summary>
		/// <param name="pedido"></param>
		private void ActualizarDetalles(Pedido pedido)
		{
			var diff = DateTime.Now - pedido.Fecha;
			var elapsed = TimeSpan.FromSeconds(Math.Round(diff.TotalSeconds));

			Precios.Text =
@"Precio base:
{0:C2}
Precio domicilio:
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

		/// <summary>
		/// Azctualiza los precios finales en la parte inferior.
		/// </summary>
		void CalcularSuma()
		{
			PrecioCuota.Text = "Precio a pagar: {0:C2}".Format(Carrito.CostoCuota);
			PrecioTotal.Text = "Precio total: {0:C2}".Format(Carrito.Total);
		}
	}
}