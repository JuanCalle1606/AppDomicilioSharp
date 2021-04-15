using System;
using GLib;
using Gtk;
using ICommon;
using ICommon.Bases;
using KYLib.Interfaces;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class LinuxLoginWindow : Dialog, ILoginWindow
	{
		public LinuxLoginWindow() : this(new Builder("LinuxLoginWindow.glade")) { }

		private LinuxLoginWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxLoginWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;
			DefaultResponse = ResponseType.Close;
			Response += Window_DeleteEvent;
		}

		private void Window_DeleteEvent(object o, SignalArgs args) =>
			Gtk.Application.Quit();

		public IPage LoginPage { get; set; }

		public IPage RegisterPage { get; set; }

		public System.Action OnLogin { get; set; }
	}
}
