using System;
using Gtk;
using KYLib.Extensions;
using KYLib.MathFn;
using Linux.Extensions;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class VendedorWidget : Window
	{
		/// <summary>
		/// Vendedor que esta siendo mostrado aqui.
		/// </summary>
		public Vendedor Vendedor { get; private set; }

		/// <summary>
		/// Imagen del vendedor.
		/// </summary>
		[UI] Image Imagen = null;

		/// <summary>
		/// Label que muestra el nombre del vendedor.
		/// </summary>
		[UI] Label Nombre = null;

		/// <summary>
		/// Label que muestra el telefono del vendedor.
		/// </summary>
		[UI] Label Telefono = null;

		/// <summary>
		/// Label que muestra el correo del vendedor.
		/// </summary>
		[UI] Label Correo = null;

		/// <summary>
		/// Label que muestra la direccion del vendedor.
		/// </summary>
		[UI] Label Direccion = null;

		/// <summary>
		/// Label que muestra el horario de atencion del vendedor.
		/// </summary>
		[UI] Label HroAtencion = null;

		/// <summary>
		/// Label que muestra la calificacion del vendedor.
		/// </summary>
		[UI] Label Calificacion = null;

		[UI] ListBox ListaProductos = null;
		public VendedorWidget(Vendedor vendedor) : this(new Builder("VendedorWidget.glade"), vendedor) { }


		private VendedorWidget(Builder builder, Vendedor vendedor) : base(builder.GetRawOwnedObject("VendedorWidget"))
		{
			builder.Autoconnect(this);
			Vendedor = vendedor;
			Imagen.Load(Vendedor.Foto, 250, 250);
			Nombre.Text = Nombre.Text.Format(vendedor.Name);
			Telefono.Text = Telefono.Text.Format(vendedor.Telefono);
			Direccion.Text = Direccion.Text.Format(vendedor.Direccion);
			HroAtencion.Text = HroAtencion.Text.Format(vendedor.AtencionCliente);
			Calificacion.Text = Calificacion.Text.Format(vendedor.Calificacion);
			ConfigurarMenu();
			builder.Dispose();
		}

		private void ConfigurarMenu()
		{
			foreach (var item in Vendedor.Menu)
			{
				ListaProductos.Add(new ProductoWidget(item));
			}
		}
	}
}
