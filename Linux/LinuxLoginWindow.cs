using System;
using GLib;
using Gtk;
using ICommon;
using ICommon.Bases;
using KYLib.Interfaces;
using KYLib.MathFn;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class LinuxLoginWindow : Dialog, ILoginWindow
	{
		[UI] Entry LoginCorreo = null;

		[UI] Entry LoginClave = null;

		[UI] Revealer LoginCorreoExp = null;

		[UI] Revealer LoginClaveExp = null;

		public LinuxLoginWindow() : this(new Builder("LinuxLoginWindow.glade")) { }

		private LinuxLoginWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxLoginWindow"))
		{
			builder.Autoconnect(this);
			DeleteEvent += Window_DeleteEvent;
			DefaultResponse = ResponseType.Close;
			Response += Window_DeleteEvent;
		}

		private void on_LoginBtn_clicked(object o, EventArgs args)
		{
			var correo = LoginCorreo.Text;
			var clave = LoginClave.Text;
			Usuario user = DomiciliosApp.Instance.Compradores.Find(C => C.Correo == correo);
			user ??= DomiciliosApp.Instance.Vendedores.Find(V => V.Correo == correo);

			//si es null es porque no es encuentra ese usuario.
			if (user == null)
			{
				LoginCorreoExp.RevealChild = true;
			}
			else
			{
				//validamos la clave
				if (user.Clave == clave)
				{
					DomiciliosApp.ClienteActual = user;
					//llamamos el callback, si existe
					OnLogin?.Invoke();
				}
				else
				{
					LoginClaveExp.RevealChild = true;
				}
			}
		}

		private void on_Login_changed(object o, EventArgs args)
		{
			var obj = o as Entry;
			if (obj == null) return;

			var Exp = LoginCorreo == o ? LoginCorreoExp : LoginClaveExp;
			if (Exp.ChildRevealed)
				Exp.RevealChild = false;

			//esta linea es solo para lo grafico, una mini animacion para saber cuantos caracteres se pueden poner.
			obj.ProgressFraction = obj.Text.Length / (float)obj.MaxLength;
		}

		private void Window_DeleteEvent(object o, SignalArgs args) =>
			Gtk.Application.Quit();

		public IPage LoginPage { get; set; }

		public IPage RegisterPage { get; set; }

		public System.Action OnLogin { get; set; }
	}
}
