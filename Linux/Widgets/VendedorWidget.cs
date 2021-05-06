using System;
using Gtk;
using ICommon;
using KYLib.Extensions;
using KYLib.MathFn;
using Linux.Extensions;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Carlos Arbelaez")]
	/// <summary>
	/// Widget que muestra información sobre un vendedor.
	/// </summary>
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
		/// Label que muestra la calificacion del vendedor.
		/// </summary>
		[UI] Label Calificacion = null;

		/// <summary>
		/// Lista de productos que ofrece este vendedor.
		/// </summary>
		[UI] ListBox ListaProductos = null;

		/// <summary>
		/// Crea un nuevo widget enlazado a un vendedor.
		/// </summary>
		public VendedorWidget(Vendedor vendedor) : this(new Builder("VendedorWidget.glade"), vendedor) { }

		private VendedorWidget(Builder builder, Vendedor vendedor) : base(builder.GetRawOwnedObject("VendedorWidget"))
		{
			builder.Autoconnect(this);
			Vendedor = vendedor;
			//actualizamos todos las propiedades
			Imagen.Load(Vendedor.Foto, 250, 250);
			Nombre.Text = Nombre.Text.Format(vendedor.Name);
			Correo.Text = Correo.Text.Format(vendedor.Correo);
			Telefono.Text = Telefono.Text.Format(vendedor.Telefono);
			Direccion.Text = Direccion.Text.Format(vendedor.Direccion);
			Calificacion.Text = Calificacion.Text.Format(vendedor.Calificacion);
			ConfigurarMenu();
			builder.Dispose();
		}

		/// <summary>
		/// Agrega los productos a <see cref="ListaProductos"/>.
		/// </summary>
		private void ConfigurarMenu()
		{
			foreach (var item in Vendedor.Menu)
			{
				ListaProductos.Add(new ProductoWidget(item));
			}
		}
	}
}
