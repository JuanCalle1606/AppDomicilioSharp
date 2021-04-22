using System;
using Gtk;
using KYLib.Extensions;
using KYLib.MathFn;
using Linux.Extensions;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux.Widgets
{
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
			Imagen.Load("https://media4.giphy.com/media/xUPGcAep2BZhomS0HC/giphy.gif", 50, 50);
			//actualizamos todos los campos
			Precios.Text = Precios.Text.Format(producto.Precio, producto.ValorDomicilio);
			Nombre.Text = producto.Name;
			Descripcion.Text = producto.Descripcion;
			Calificacion.Text = Calificacion.Text.Format(producto.Calificacion);
			Cuotas.Text = producto.PermiteCuotas ? "Por Cuotas*" : string.Empty;

			builder.Dispose();
		}

		public void on_CarritoBtn_clicked(object o, EventArgs args)
		{
			Comprador user = (Comprador)DomiciliosApp.ClienteActual;

			//vemos si algun pedido actual del carro ya tiene el producto.
			var ped = user.Carrito.Pedidos.Find(P => UmlBased.Producto.
			ReferenceEquals(Producto, P.Producto));
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
				};
			}
			ped.Actualizar(++ped.Cantidad, (Small)CuotasDialogo.Value);

			if (user.Carrito.Agregar(ped))
			{
				Utils.SendNotification("Producto agregado al carrito");
			}
		}
	}
}
