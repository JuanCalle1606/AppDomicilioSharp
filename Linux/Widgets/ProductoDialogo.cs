using System;
using Gtk;
using ICommon;
using KYLib.Extensions;
using KYLib.MathFn;
using Linux.Extensions;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux.Widgets
{
	[Author("Juan Pablo Calle")]
	public partial class ProductoWidget
	{
		/// <summary>
		/// Dialogo que muestra información mas detallada.
		/// </summary>
		[UI] Dialog Dialogo = null;

		/// <summary>
		/// Muestra el nombre del producto.
		/// </summary>
		[UI] Label Titulo = null;

		/// <summary>
		/// Muestra el precio del producto.
		/// </summary>
		[UI] Label Precio = null;

		/// <summary>
		/// Muestra el precio del domicilio.
		/// </summary>
		[UI] Label Domicilio = null;

		/// <summary>
		/// Selector de la calificación que se le dara al usuario.
		/// </summary>
		[UI] SpinButton Calificar = null;

		/// <summary>
		/// Boton que se pulsa para calificar el producto.
		/// </summary>
		[UI] Button CalificarBtn = null;

		/// <summary>
		/// Muestra la calificación del producto en el dialogo.
		/// </summary>
		[UI] Label CalificacionDialogo = null;

		/// <summary>
		/// Muestra la foto del producto a 250x250.
		/// </summary>
		[UI] Image Foto = null;

		/// <summary>
		/// Muestra la descripción del producto en el dialogo.
		/// </summary>
		[UI] Label DescripcionDialogo = null;

		/// <summary>
		/// Selector del numero de cuotas a pagar.
		/// </summary>
		[UI] SpinButton CuotasDialogo = null;

		/// <summary>
		/// Muestra el nombre del vendedor.
		/// </summary>
		[UI] Label NombreVendedor = null;

		/// <summary>
		/// Muestra la calificación del vendedor.
		/// </summary>
		[UI] Label CalificacionVendedor = null;

		/// <summary>
		/// Box que contiene un cuadro que se muestra cuando se admiten cuotas.
		/// </summary>
		[UI] Box AdmiteCuotas = null;

		/// <summary>
		/// Indica si se puede calificar el producto. Se borra al reiniciar la app.
		/// </summary>
		bool calificable = true;

		/// <summary>
		/// Indica si el dialogo ha dsido mostrado una vez ya lo que queire decir que se cargaron recursos.
		/// </summary>
		bool ShowDialog = false;

		/// <summary>
		/// Muestra el dialogo con información del producto.
		/// </summary>
		void On_VerBtn_clicked(object o, EventArgs args)
		{
			if (!ShowDialog) ConfigurarDialogo();
			ShowDialog = true;
			Dialogo.Show();
		}

		/// <summary>
		/// Añade una calificiación al producto.
		/// </summary>
		void On_CalificarBtn_clicked(object o, EventArgs args)
		{
			if (!calificable) return;
			calificable = false;
			Small puntaje = (Small)Calificar.ValueAsInt;
			Producto.Calificar(puntaje);
			CalificarBtn.Sensitive = false;
			Calificar.Sensitive = false;
			//actualizamos la interfaz con la ultima información
			var cal = "Calificación: {0} ".Format(Producto.Calificacion);
			CalificacionDialogo.Text = cal;
			Calificacion.Text = cal;
		}

		/// <summary>
		/// Configura los campos del dialogo y carga la foto.
		/// </summary>
		void ConfigurarDialogo()
		{
			Dialogo.DeleteEvent += OnDialogDeleted;
			Dialogo.Response += OnDialogResponse;
			Actualizar();
			Foto.Load(Producto.Foto, 250, 250);
		}

		/// <summary>
		/// Oculta el dialogo cuando se le da a agregar al carro o a cerrar.
		/// </summary>
		private void OnDialogResponse(object o, ResponseArgs args)
		{
			//ocultamos el dialogo
			Dialogo.Hide();
		}

		/// <summary>
		/// Previene que se borre la ventana cuando se cierra con la "X" o con alt+f4
		/// </summary>
		private void OnDialogDeleted(object o, DeleteEventArgs args)
		{
			//prevenimos que la ventana se cierre
			args.RetVal = true;
			//ocultamos el dialogo
			Dialogo.Hide();
		}
	}
}