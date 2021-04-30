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
		/// <summary>
		/// Revealer que muestra todos los errores referentes a validaciones de registro.
		/// </summary>
		[UI] Revealer RegRevel = null;

		/// <summary>
		/// Campo de entrada del nombre del usuario.
		/// </summary>
		[UI] Entry RegNombre = null;

		/// <summary>
		/// Campo de entrada del correo.
		/// </summary>
		[UI] Entry RegCorreo = null;

		/// <summary>
		/// Campo de entrada de la clave.
		/// </summary>
		[UI] Entry RegCLave = null;

		/// <summary>
		/// Campo de entrada donde se repite la clave.
		/// </summary>
		[UI] Entry RegClaveRep = null;

		/// <summary>
		/// Campo de entrada de la dirección.
		/// </summary>
		[UI] Entry RegDir = null;

		/// <summary>
		/// Campo de entrada del telefono.
		/// </summary>
		[UI] Entry RegTel = null;

		/// <summary>
		/// Label que contiene un texto a mostrar al usuario.
		/// </summary>
		[UI] Label RegError = null;

		/// <summary>
		/// Radio que indica si es un comprador.
		/// </summary>
		[UI] RadioButton RegComp = null;

		/// <summary>
		/// Dialogo que se muestra al usuario cuando se crea correctamente la cuenta.
		/// </summary>
		[UI] MessageDialog RegMsg = null;

		/// <summary>
		/// Regex que valida que el nombre ingresado sea valido.
		/// </summary>
		Regex ValidNombre = new(@"^\w+( \w+)+$");

		/// <summary>
		/// Regex que valida que el correo ingresado sea valido.
		/// </summary>
		Regex ValidCorreo = new(@"^\w+(\.\w+)*@\w+(\.\w+)+$");

		/// <summary>
		/// Regex que valida que la dirección ingresada sea valida.
		/// </summary>
		Regex ValidDireccion = new(@"^[\w ]+#\w+-\d+$");

		/// <summary>
		/// Regex que valida que el telefono ingresado sea valido.
		/// </summary>
		Regex ValidTelefono = new(@"^\d+$");

		/// <summary>
		/// Se eejcuta cada que se le da click al boton de registrarse y hace todas las validaciones
		/// </summary>
		private void On_RegBtn_clicked(object sender, EventArgs args)
		{
			// primero hacemos las validaciones para cada campo, en cada campo se valida que no sea una cadena vacia y luego si cumple con su regex, si tiene

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
			else
			{
				Usuario user = DomiciliosApp.Instance.Compradores.Find(C => C.Correo == correo);
				user ??= DomiciliosApp.Instance.Vendedores.Find(V => V.Correo == correo);
				if (user != null)
				{
					RegError.Text = "El correo ingresado ya esta registrado.";
					RegRevel.RevealChild = true;
					return;
				}
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

			//creamos el usuario con los datos obtenidos
			CreateUser(nombre, correo, clave, direccion, telefono, RegComp.Active);

			// mostramos el dialogo de que se creo la cuenta.
			RegMsg.Run();
			RegMsg.Visible = false;
		}

		/// <summary>
		/// Aqui creamos el usuario con los datos dados.
		/// </summary>
		private static void CreateUser(string nombre, string correo, string clave, string direccion, string telefono, bool iscomp)
		{
			//creamos dependiendo de sie s comprador o vendedor.
			if (iscomp)
			{
				Comprador user = new()
				{
					Id = DomiciliosApp.Instance.NextUserId++,
					Name = nombre,
					Correo = correo,
					Clave = clave,
					Direccion = direccion,
					Telefono = telefono,
					Creacion = DateTime.Now
				};
				DomiciliosApp.Instance.Compradores.Add(user);
			}
			else
			{
				Vendedor user = new()
				{
					Id = DomiciliosApp.Instance.NextUserId++,
					Name = nombre,
					Correo = correo,
					Clave = clave,
					Direccion = direccion,
					Telefono = telefono,
					Creacion = DateTime.Now
				};
				DomiciliosApp.Instance.Vendedores.Add(user);
			}

		}
	}
}