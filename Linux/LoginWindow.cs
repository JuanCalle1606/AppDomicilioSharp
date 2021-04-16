using System;
using Gtk;
using ICommon.Bases;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxLoginWindow
	{
		[UI] Entry LoginCorreo = null;

		[UI] Entry LoginClave = null;

		[UI] Revealer LoginCorreoExp = null;

		[UI] Revealer LoginClaveExp = null;

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

		public IPage LoginPage { get; set; }
	}
}