using System;
using GLib;
using Gtk;
using ICommon;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxLoginWindow : Dialog, ILoginWindow
	{
		public LinuxLoginWindow() : this(new Builder("LinuxLoginWindow.glade")) { }

		private LinuxLoginWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxLoginWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;
			DefaultResponse = ResponseType.Close;
			Response += Window_DeleteEvent;
			RegMsg.DefaultResponse = ResponseType.Accept;
		}

		private void on_Login_changed(object o, EventArgs args)
		{
			var obj = o as Entry;
			if (obj == null) return;

			var Exp = RegRevel;
			if (o == LoginCorreo)
				Exp = LoginCorreoExp;
			else if (o == LoginClave)
				Exp = LoginClaveExp;

			if (Exp.ChildRevealed)
				Exp.RevealChild = false;

			//esta linea es solo para lo grafico, una mini animacion para saber cuantos caracteres se pueden poner.
			obj.ProgressFraction = obj.Text.Length / (float)obj.MaxLength;
		}

		private void Window_DeleteEvent(object o, SignalArgs args) =>
			Gtk.Application.Quit();

		public IPage RegisterPage { get; set; }

		public System.Action OnLogin { get; set; }
	}
}
