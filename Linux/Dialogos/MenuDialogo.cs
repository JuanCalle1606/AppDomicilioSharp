using System;
using Gtk;
using ICommon;
using KYLib.Extensions;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Carlos Arbelaez")]
	partial class MenuDialogo : Dialog
	{
		[UI] ListBox ListaPedidos = null;
		[UI] Label SeleccionarAlert = null;
		[UI] Box Detalles = null;
		[UI] SpinButton Precio = null;
		[UI] SpinButton Domicilio = null;
		[UI] CheckButton PermiteCuotas = null;
		[UI] Label TimeAgo = null;
		public MenuDialogo() : this(new Builder("MenuDialogo.glade")) { }

		private MenuDialogo(Builder builder) : base(builder.GetRawOwnedObject("MenuDialogo"))
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Cancel;
			Response += Dialog_Response;
			DeleteEvent += Dialog_Response;
			CrearProducto.DeleteEvent += ProdDialogClose;
			CrearProducto.Response += ProdDialogClose;
			ActualizarMenu();
			builder.Dispose();
		}

		void On_AplicarBtn_clicked(object o, EventArgs args)
		{
			var row = ListaPedidos.SelectedRow;
			var widget = row.Child as ProductoWidget;
			var producto = widget.Producto;
			producto.Precio = Precio.Value;
			producto.ValorDomicilio = Domicilio.Value;
			producto.PermiteCuotas = PermiteCuotas.Active;
			widget.Actualizar();
			MostrarDetalles(producto);
		}

		private void Dialog_Response(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			Hide();
			ListaPedidos.UnselectAll();
		}

		void ActualizarMenu()
		{
			var user = DomiciliosApp.ClienteActual as Vendedor;
			foreach (var item in user.Menu)
			{
				ListaPedidos.Add(new ProductoWidget(item));
			}
		}

		void On_ListaPedidos_row_selected(object o, RowSelectedArgs args)
		{
			if (args.Row == null)
			{
				Detalles.Visible = false;
				SeleccionarAlert.Visible = true;
				return;
			}

			var widget = args.Row.Child as ProductoWidget;
			var producto = widget.Producto;

			MostrarDetalles(producto);
			Detalles.Visible = true;
			SeleccionarAlert.Visible = false;

		}

		private void MostrarDetalles(Producto producto)
		{
			Precio.Value = producto.Precio;
			Domicilio.Value = producto.ValorDomicilio;
			PermiteCuotas.Active = producto.PermiteCuotas;
			var diff = DateTime.Now - producto.FechaCreacion;
			var elapsed = TimeSpan.FromSeconds(Math.Round(diff.TotalSeconds));
			TimeAgo.Text = "Añadido hace: {0}".Format(elapsed);
		}
	}
}
