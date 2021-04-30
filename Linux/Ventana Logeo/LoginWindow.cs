using System;
using Gtk;
using ICommon.Bases;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxLoginWindow
	{
		/// <summary>
		/// Campo de entrada del correo para logear.
		/// </summary>
		[UI] Entry LoginCorreo = null;

		/// <summary>
		/// Campo de entrada de la clave para logear.
		/// </summary>
		[UI] Entry LoginClave = null;

		/// <summary>
		/// Revealer que muestra un error referente al correo.
		/// </summary>
		[UI] Revealer LoginCorreoExp = null;

		/// <summary>
		/// Revealer que muestra un error referente a la clave.
		/// </summary>
		[UI] Revealer LoginClaveExp = null;

		/// <summary>
		/// Se ejecuta cuando el usuario le da click al boton de iniciar sesión
		/// </summary>
		private void On_LoginBtn_clicked(object o, EventArgs args)
		{
			//obtenemos las entradas
			var correo = LoginCorreo.Text;
			var clave = LoginClave.Text;
			//buscamos por un usuario con ese correo.
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
	}
}