using System;
using Gtk;
using ICommon;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	partial class MenuDialogo
	{
		[UI] Entry ProdNombre = null;

		[UI] TextView ProdDetalles = null;

		[UI] SpinButton ProdPrecio = null;

		[UI] SpinButton ProdDomicilio = null;

		[UI] Entry ProdUrl = null;

		[UI] CheckButton ProdCuotas = null;

		[UI] Dialog CrearProducto = null;

		void On_ProdAplicarBtn_clicked(object o, EventArgs args)
		{
			if (string.IsNullOrEmpty(ProdNombre.Text)
				|| string.IsNullOrEmpty(ProdDetalles.Buffer.Text))
				return;
			var user = DomiciliosApp.ClienteActual as Vendedor;
			var prod = new Producto()
			{
				Id = DomiciliosApp.Instance.Productos.Count,
				Name = ProdNombre.Text,
				Descripcion = ProdDetalles.Buffer.Text,
				Precio = ProdPrecio.Value,
				ValorDomicilio = ProdDomicilio.Value,
				FechaCreacion = DateTime.Now,
				Foto = ProdUrl.Text,
				PermiteCuotas = ProdCuotas.Active,
				OwnerId = user.Id
			};
			user.AgregarProducto(prod);
			ListaPedidos.Add(new ProductoWidget(prod));
		}

		void On_RemoverBtn_clicked_cb(object o, EventArgs args)
		{
			var user = DomiciliosApp.ClienteActual as Vendedor;
			var row = ListaPedidos.SelectedRow;
			var widget = row.Child as ProductoWidget;
			var producto = widget.Producto;

			user.RemoverProducto(producto);
			using (row)
			{
				ListaPedidos.Remove(row);
				row.Child.Dispose();
			}
		}

		void On_AgregarBtn_clicked(object o, EventArgs args)
		{
			ProdNombre.Text = string.Empty;
			ProdDetalles.Buffer.Text = string.Empty;
			ProdPrecio.Value = 1000;
			ProdDomicilio.Value = 0;
			ProdUrl.Text = string.Empty;
			ProdCuotas.Active = true;
			ListaPedidos.UnselectAll();
			CrearProducto.Show();
		}

		private void ProdDialogClose(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			CrearProducto.Hide();
		}
	}
}