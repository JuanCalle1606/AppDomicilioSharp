using System;
using Gtk;
using KYLib.Extensions;
using KYLib.MathFn;
using KYLib.Utils;
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
		[UI] SpinButton Calificar = null;
		[UI] Button CalificarBtn = null;
		[UI] Label CalificacionDialogo = null;
		[UI] Image Foto = null;
		[UI] Label DescripcionDialogo = null;
		[UI] SpinButton CuotasDialogo = null;
		[UI] Label NombreVendedor = null;
		[UI] Label CalificacionVendedor = null;
		[UI] Box AdmiteCuotas = null;

		bool calificable = true;

		bool ShowDialog = false;

		void on_VerBtn_clicked(object o, EventArgs args)
		{
			if (!ShowDialog) ConfigurarDialogo();
			ShowDialog = true;
			Dialogo.Show();
		}

		void on_CalificarBtn_clicked(object o, EventArgs args)
		{
			if (!calificable) return;
			calificable = false;
			Small puntaje = (Small)Calificar.ValueAsInt;
			Producto.Calificar(puntaje);
			CalificarBtn.Sensitive = false;
			Calificar.Sensitive = false;
			var cal = "Calificación: {0} ".Format(Producto.Calificacion);
			CalificacionDialogo.Text = cal;
			Calificacion.Text = cal;
		}

		void ConfigurarDialogo()
		{
			Dialogo.DeleteEvent += OnDialogDeleted;
			Dialogo.Response += OnDialogResponse;
			Titulo.Text = Producto.Name;
			DescripcionDialogo.Text = DescripcionDialogo.Text.Format(Producto.Descripcion);
			Precio.Text = Precio.Text.Format(Producto.Precio);
			CalificacionDialogo.Text = CalificacionDialogo.Text.Format(Producto.Calificacion);
			Domicilio.Text = Domicilio.Text.Format(Producto.ValorDomicilio);
			AdmiteCuotas.Visible = Producto.PermiteCuotas;
			var vendedor = Producto.ObtenerVendedor();
			NombreVendedor.Text = vendedor?.Name;
			CalificacionVendedor.Text = CalificacionVendedor.Text.Format(vendedor?.Calificacion);
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