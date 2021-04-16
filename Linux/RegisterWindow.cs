using System;
using System.Text.RegularExpressions;
using Gtk;
using ICommon.Bases;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxLoginWindow
	{
		[UI] Revealer RegRevel = null;

		[UI] Entry RegNombre = null;

		[UI] Entry RegCorreo = null;

		[UI] Entry RegCLave = null;

		[UI] Entry RegClaveRep = null;

		[UI] Entry RegDir = null;

		[UI] Entry RegTel = null;

		[UI] Label RegError = null;

		[UI] RadioButton RegComp = null;

		[UI] MessageDialog RegMsg = null;

		Regex ValidNombre = new Regex(@"^\w+( \w+)+$");

		Regex ValidCorreo = new Regex(@"^\w+(\.\w+)*@\w+(\.\w+)+$");

		Regex ValidDireccion = new Regex(@"^[\w ]+#\w+-\d+$");

		Regex ValidTelefono = new Regex(@"^3\d+$");

		private void on_RegBtn_clicked(object sender, EventArgs args)
		{

			#region Nombre
			var nombre = RegNombre.Text;
			if (string.IsNullOrWhiteSpace(nombre))
			{
				RegError.Text = "Tu nombre no puede estar vacio.";
				RegRevel.RevealChild = true;
				return;
			}
			else if (!ValidNombre.IsMatch(nombre))
			{
				RegError.Text = "El nombre ingresado no es valido.";
				RegRevel.RevealChild = true;
				return;
			}
			#endregion

			#region Correo
			var correo = RegCorreo.Text;
			if (string.IsNullOrWhiteSpace(correo))
			{
				RegError.Text = "El correo no puede estar vacio.";
				RegRevel.RevealChild = true;
				return;
			}
			else if (!ValidCorreo.IsMatch(correo))
			{
				RegError.Text = "El correo ingresado no es valido.";
				RegRevel.RevealChild = true;
				return;
			}
			#endregion

			#region Clave
			var clave = RegCLave.Text;
			if (string.IsNullOrWhiteSpace(clave))
			{
				RegError.Text = "La contraseña no puede estar vacia.";
				RegRevel.RevealChild = true;
				return;
			}
			else if (clave != RegClaveRep.Text)
			{
				RegError.Text = "Las contraseñas no coinciden.";
				RegRevel.RevealChild = true;
				return;
			}
			#endregion

			#region Dirección
			var direccion = RegDir.Text;
			if (string.IsNullOrWhiteSpace(direccion))
			{
				RegError.Text = "La dirección no puede estar vacia.";
				RegRevel.RevealChild = true;
				return;
			}
			else if (!ValidDireccion.IsMatch(direccion))
			{
				RegError.Text = "La dirección ingresada no es valida.";
				RegRevel.RevealChild = true;
				return;
			}
			#endregion

			#region Telefono
			var telefono = RegTel.Text;
			if (string.IsNullOrWhiteSpace(telefono))
			{
				RegError.Text = "El telefono no puede estar vacio.";
				RegRevel.RevealChild = true;
				return;
			}
			else if (!ValidTelefono.IsMatch(telefono))
			{
				RegError.Text = "El telefono ingresado no es valido.";
				RegRevel.RevealChild = true;
				return;
			}
			#endregion

			//aqui se debe crear el usuario
			var iscomp = RegComp.Active;

			RegMsg.Run();
			RegMsg.Visible = false;
		}
	}
}