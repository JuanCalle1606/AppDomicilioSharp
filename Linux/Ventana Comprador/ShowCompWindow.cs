using System;
using System.IO;
using Gtk;
using KYLib.MathFn;
using KYLib.Utils;
using Linux.Widgets;
using UmlBased;
using Pix = Gdk.Pixbuf;

namespace Linux
{
	partial class LinuxCompWindow
	{
		private bool configuredshow = false;

		private void ConfigureFistShow()
		{
			if (configuredshow) return;
			configuredshow = true;

			var foto = DomiciliosApp.ClienteActual.Foto;
			if (!string.IsNullOrWhiteSpace(foto) && File.Exists(foto))
			{
				if (Pix.GetFileInfo(foto, out _, out _) != null)
				{
					using (var temp = new Pix(foto))
					{
						UserAvatar.Pixbuf = temp.ScaleSimple(20, 20, Gdk.InterpType.Tiles);
					}
				}
			}

			//esto es solo para pruebas, agregamos productos random
			for (int i = 0; i < 10; i++)
			{
				ListaProductos.Add(new ProductoWidget());
			}
		}
	}
}