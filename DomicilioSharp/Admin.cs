using System;
using System.Collections.Generic;
using KYLib.ConsoleUtils;
using KYLib.System;

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
		}

	}
}