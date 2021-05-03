using System;
using System.IO;
using Gtk;
using KYLib.Extensions;
using KYLib.MathFn;
using KYLib.Utils;
using Linux.Extensions;
using Linux.Widgets;
using UmlBased;
using Pix = Gdk.Pixbuf;

namespace Linux
{
	partial class LinuxCompWindow
	{
		/// <summary>
		/// Indica si la ventana ya fue configurada.
		/// </summary>
		private bool configuredshow = false;

		/// <summary>
		/// Configura la ventana.
		/// </summary>
		private void ConfigureFistShow()
		{
			if (configuredshow) return;
			configuredshow = true;

			var foto = DomiciliosApp.ClienteActual.Foto;
			UserAvatar.Load(foto, 20, 20);

			ShowProductos();
		}

		void ShowProductos()
		{
			if (DomiciliosApp.Instance.Productos.Count == 0) return;

			var i = 0;
			foreach (var item in DomiciliosApp.Instance.Productos.Shuffle())
			{
				ListaProductos.Add(new ProductoWidget(item));
				if (i++ >= 10) break;
			}
		}

		void ClearProductos()
		{
			var current = ListaProductos.Children;
			foreach (ListBoxRow item in current)
			{
				using (item)
				{
					ListaProductos.Remove(item);
					item.Child.Dispose();
				}
			}
		}
	}
}
