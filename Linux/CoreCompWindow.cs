using System;
using GLib;
using Gtk;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxCompWindow : Window, IWindow
	{
		public LinuxCompWindow() : this(new Builder("LinuxCompWindow.glade")) { }

		private LinuxCompWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxCompWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;
		}

		/// <summary>
		/// Se ejecuta cuando el usuario la ventana.
		/// </summary>
		private void Window_DeleteEvent(object o, EventArgs args) =>
			Gtk.Application.Quit();
	}
}
