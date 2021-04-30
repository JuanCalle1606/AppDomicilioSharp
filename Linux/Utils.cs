using GLib;
using ICommon;
using KYLib.Extensions;
using KYLib.System;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Clase de utilidades.
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// App wrapper.
		/// </summary>
		public static Gtk.Application App { private get; set; }

		/// <summary>
		/// Notificación que se usa.
		/// </summary>
		private static readonly Notification Noti = new("Domicilio Sharp");

		/// <summary>
		/// Envia una notificación al usuario.
		/// </summary>
		/// <param name="text">Mensaje a mostrar al usuario.</param>
		public static void SendNotification(string text)
		{
			if (Info.CurrentSystem.IsLinux())
			{
				Noti.Body = text;
				App.SendNotification("Domicilio-Notification", Noti);
			}
		}
	}
}