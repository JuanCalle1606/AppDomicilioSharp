using System;
using Gtk;
using KYLib.Extensions;
using Linux.Extensions;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux.Widgets
{
	public partial class ProductoWidget
	{
		[UI] Dialog Dialogo = null;
		[UI] Label Titulo = null;
		[UI] Label Precio = null;
		[UI] Label Domicilio = null;
		//[UI] SpinButton Calificar = null;
		//[UI] Button CalificarBtn = null;
		[UI] Image Foto = null;
		[UI] Label DescripcionDialogo = null;
		[UI] SpinButton CuotasDialogo = null;
		//[UI] Label NombreVendedor = null;
		//[UI] Label CalificacionVendedor = null;
		//[UI] Button CarritoBtnDialogo = null;
		//[UI] Button CerrarBtn = null;
		[UI] Box AdmiteCuotas = null;

		bool ShowDialog = false;

		void on_VerBtn_clicked(object o, EventArgs args)
		{
			if (!ShowDialog) ConfigurarDialogo();
			ShowDialog = true;
			Dialogo.Show();
		}

		void ConfigurarDialogo()
		{
			Dialogo.DeleteEvent += OnDialogDeleted;
			Dialogo.Response += OnDialogResponse;
			Titulo.Text = Producto.Name;
			DescripcionDialogo.Text = DescripcionDialogo.Text.Format(Producto.Descripcion);
			Precio.Text = Precio.Text.Format(Producto.Precio);
			Domicilio.Text = Domicilio.Text.Format(Producto.ValorDomicilio);
			AdmiteCuotas.Visible = Producto.PermiteCuotas;
			//var vendedor = Producto.ObtenerVendedor();
			//NombreVendedor.Text = vendedor.Name;
			Foto.Load(Producto.Foto, 250, 250);

		}

		private void OnDialogResponse(object o, ResponseArgs args)
		{
			//ocultamos el dialogo
			Dialogo.Hide();
		}

		private void OnDialogDeleted(object o, DeleteEventArgs args)
		{
			//prevenimos que la ventana se cierre
			args.RetVal = true;
			//ocultamos el dialogo
			Dialogo.Hide();
		}
	}
}