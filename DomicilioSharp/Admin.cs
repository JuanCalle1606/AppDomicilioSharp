using System;
using System.Collections.Generic;
using KYLib.ConsoleUtils;
using KYLib.MathFn;
using KYLib.System;
using KYLib.Utils;
using UmlBased;
using KYLib.Extensions;

namespace DomicilioSharp
{
	partial class Program
	{
		/// <summary>
		/// Carga la ventana de administrador.
		/// </summary>
		static void LoadAdmin()
		{
			if (!(Factory is Linux.LinuxFactory)) return;
			if (!Mod.TryImport("Admin.dll", out Mod Admin)) return;

			var WindowType = Admin.DLL.GetType("Admin.AdminWindow");

			if (WindowType == null) return;

			var AdminWindow = Activator.CreateInstance(WindowType);

			var WindowShow = WindowType.GetMethod("Show");

			WindowShow.Invoke(AdminWindow, null);

			Cons.Trace("Corriendo como administrador", ForegroundColor.Magenta);
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

			DomiciliosApp.ObtenerVendedor(0).AgregarProducto(prod);
		}

	}
}