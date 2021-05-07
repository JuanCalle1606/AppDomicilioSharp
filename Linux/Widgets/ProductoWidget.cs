using System;
using Gtk;
using KYLib.Extensions;
using KYLib.MathFn;
using Linux.Extensions;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux.Widgets
{
	/// <summary>
	/// Widget que muestra información sobre un producto.
	/// </summary>
	public partial class ProductoWidget : Box
	{
		/// <summary>
		/// Producto que esta siendo mostrado aqui.
		/// </summary>
		public Producto Producto { get; private set; }

		/// <summary>
		/// Imagen del producto en miniatura.
		/// </summary>
		[UI] Image Imagen = null;

		/// <summary>
		/// Label que muestra los precios.
		/// </summary>
		[UI] Label Precios = null;

		/// <summary>
		/// Muestra el nombre del producto.
		/// </summary>
		[UI] Label Nombre = null;

		/// <summary>
		/// Muestra la descripción del producto.
		/// </summary>
		[UI] Label Descripcion = null;

		/// <summary>
		/// Muestra la calificacion promedio del producto.
		/// </summary>
		[UI] Label Calificacion = null;

		/// <summary>
		/// Muestra un texto si ese producto soporta cuotas.
		/// </summary>
		[UI] Label Cuotas = null;

		/// <summary>
		/// Crea un nuevo Widget que muestra información de un producto.
		/// </summary>
		/// <param name="producto">Producto a mostrar.</param>
		public ProductoWidget(Producto producto) : this(new Builder("ProductoWidget.glade"), producto) { }

		private ProductoWidget(Builder builder, Producto producto) : base(builder.GetRawOwnedObject("ProductoWidget"))
		{
			builder.Autoconnect(this);
			Producto = producto;
			//cargamos la imagen.
			Imagen.Load(Producto.Foto, 50, 50);
			//actualizamos todos los campos
			Precios.Text = Precios.Text.Format(producto.Precio, producto.ValorDomicilio);
			Nombre.Text = producto.Name;
			Descripcion.Text = producto.Descripcion;
			Calificacion.Text = Calificacion.Text.Format(producto.Calificacion);
			Cuotas.Text = producto.PermiteCuotas ? "Por Cuotas*" : string.Empty;

			builder.Dispose();
		}

		public void Actualizar()
		{
			//actualizamos todos los campos
			Precios.Text = Precios.Text.Format(Producto.Precio, Producto.ValorDomicilio);
			Nombre.Text = Producto.Name;
			Descripcion.Text = Producto.Descripcion;
			Calificacion.Text = Calificacion.Text.Format(Producto.Calificacion);
			Cuotas.Text = Producto.PermiteCuotas ? "Por Cuotas*" : string.Empty;
			Titulo.Text = Producto.Name;
			DescripcionDialogo.Text = DescripcionDialogo.Text.Format(Producto.Descripcion);
			Precio.Text = Precio.Text.Format(Producto.Precio);
			CalificacionDialogo.Text = CalificacionDialogo.Text.Format(Producto.Calificacion);
			Domicilio.Text = Domicilio.Text.Format(Producto.ValorDomicilio);
			AdmiteCuotas.Visible = Producto.PermiteCuotas;
			var vendedor = Producto.ObtenerVendedor();
			NombreVendedor.Text = vendedor?.Name;
			CalificacionVendedor.Text = CalificacionVendedor.Text.Format(vendedor?.Calificacion);

		}

		/// <summary>
		/// Agrega el producto de este widget como un pedido al carrito del usuario o aumenta en 1 su cantidad si ya existe.
		/// </summary>
		public void On_CarritoBtn_clicked(object o, EventArgs args)
		{
			//si el usuario es null es porque es un vendedor
			if (DomiciliosApp.ClienteActual is not Comprador user) return;

			//vemos si algun pedido actual del carro ya tiene el producto.
			var ped = user.Carrito.Pedidos.Find(P => ReferenceEquals(Producto, P.Producto));

			if (ped == null)
			{
				//creamos el pedido que vamos a agregar al carrito.
				ped = new Pedido()
				{
					Id = DomiciliosApp.Instance.NextPedId++,
					Fecha = DateTime.Now,
					Producto = Producto,
					ValorBase = Producto.Precio,
					PorcentajeDescuento = user.Descuento,
					PorcentajeIVA = Pedido.CurrentIVA,
					OwnerID = user.Id
				};
			}
			//aumentamos la cantidad del pedido en 1.
			ped.Actualizar(++ped.Cantidad, (Small)CuotasDialogo.Value);

			//agregamos el pedido al carrito.
			if (user.Carrito.Agregar(ped))
			{
				Utils.SendNotification("Producto agregado al carrito");
			}
		}

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			Dialogo.Dispose();
			Imagen.Dispose();
			base.Dispose(disposing);
		}
	}
}
