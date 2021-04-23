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
			Mod Admin;
			if (!Mod.TryImport("Admin.dll", out Admin)) return;

			var types = new List<Type>(Admin.DLL.GetTypes());

			var WindowType = Admin.DLL.GetType("Admin.AdminWindow");

			if (WindowType == null) return;

			var AdminWindow = Activator.CreateInstance(WindowType);

			var WindowShow = WindowType.GetMethod("Show");

			WindowShow.Invoke(AdminWindow, null);

			Cons.Trace("Corriendo como administrador", ForegroundColor.Magenta);
		}

	}
}