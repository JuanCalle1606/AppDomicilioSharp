using System;
using GLib;
using Gtk;
using ICommon;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	/// <summary>
	/// Ventana que se usa para iniciar sesíon usando gtk
	/// </summary>
	partial class LinuxLoginWindow : Dialog, ILoginWindow
	{
		/// <summary>
		/// Cremoa una nueva ventana.
		/// </summary>
		public LinuxLoginWindow() : this(new Builder("LinuxLoginWindow.glade")) { }

		/// <summary>
		/// Constructor privado con un builder.
		/// </summary>
		private LinuxLoginWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxLoginWindow"))
		{
			//le decimos al builder que nos todo lo del archivo .glade con esta instancia
			builder.Autoconnect(this);
			//Agregamos eventos.
			DeleteEvent += Window_DeleteEvent;
			Response += Window_DeleteEvent;
			//decimos las respuestas por defecto de los dialogos.
			DefaultResponse = ResponseType.Close;
			RegMsg.DefaultResponse = ResponseType.Accept;
			builder.Dispose();
		}

		/// <summary>
		/// Se ejecuta cuando el usuario cambie la el texto de cualquiere entrada.
		/// </summary>
		private void on_Login_changed(object o, EventArgs args)
		{
			//obtenemos la entrada.
			var obj = o as Entry;
			if (obj == null) return;

			// vemos cual es el revealer asociado a esa entrada.
			var Exp = RegRevel;
			if (o == LoginCorreo)
				Exp = LoginCorreoExp;
			else if (o == LoginClave)
				Exp = LoginClaveExp;

			// ocultamos el revealer en caso de que se este mostrando.
			if (Exp.ChildRevealed)
				Exp.RevealChild = false;

			// actualizamos el progreso de la barrita de la entrada.
			obj.ProgressFraction = obj.Text.Length / (float)obj.MaxLength;
		}

		/// <summary>
		/// Se ejecuta cuando el usuario cierra la ventana de logeo o le da al botón cerrar.
		/// </summary>
		private void Window_DeleteEvent(object o, SignalArgs args) =>
			Gtk.Application.Quit();

		/// <inheritdoc/>
		public System.Action OnLogin { get; set; }
	}
}
