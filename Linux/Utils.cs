using GLib;
using Gtk;

namespace Linux
{
	public static class Utils
	{
		public static Gtk.Application App { private get; set; }

		private static Notification Noti = new Notification("Domicilio Sharp");

		public static void SendNotification(string text)
		{
			Noti.Body = text;
			App.SendNotification("Domicilio-Notification", Noti);
		}
	}
}