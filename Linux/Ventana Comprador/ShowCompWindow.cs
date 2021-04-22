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
		private bool configuredshow = false;

		private void ConfigureFistShow()
		{
			if (configuredshow) return;
			configuredshow = true;

			var foto = DomiciliosApp.ClienteActual.Foto;
			UserAvatar.Load(foto, 20, 20);

			//esto es solo para pruebas, agregamos productos random
			for (int i = 0; i < 10; i++)
			{
				var prod = new Producto()
				{
					Name = Rand.GetInt(1023, 1216).ToString(),
					Descripcion = Rand.GetInt(0, 9, 10, 20).ToString(' '),
					Precio = Rand.GetInt(1000, 100000),
					ValorDomicilio = Rand.GetInt(0, 15000),
					FechaCreacion = DateTime.Now,
					Foto = "https://media.discordapp.net/attachments/783188322087993346/834245259964973096/2021-04-20_22_52_39-Window.png"
				};
				var c = Rand.GetInt(0, 11, 1, 11);
				foreach (var item in c)
				{
					prod.Calificar((Small)item);
				}

				ListaProductos.Add(new ProductoWidget(prod));
			}
		}
	}
}