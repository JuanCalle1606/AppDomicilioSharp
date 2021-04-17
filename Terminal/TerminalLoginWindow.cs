using System;
using ICommon;
using ICommon.Bases;
using KYLib.ConsoleUtils;
using UmlBased;

namespace Terminal
{
	public class TerminalLoginWindow : ConsoleMenu, ILoginWindow
	{
		public TerminalLoginWindow()
		{
			Title = "Domicilios Sharp";
			var loginpage = new TerminalPage("Iniciar sesión");
			var registerpage = new TerminalPage("Registrarse");
			loginpage.Task = onlogin;
			registerpage.Task = onregister;

			AddItem("Cancelar y salir", Stop, true);
			AddItem(loginpage);
			AddItem(registerpage);

			LoginPage = loginpage;
			RegisterPage = registerpage;
		}

		private void onregister()
		{
			throw new NotImplementedException();
		}

		private void onlogin()
		{
			Cons.Trace("Ingrese el correo para iniciar sesión:", ForegroundColor.Cyan);
			var correo = Cons.Line;
			Cons.Trace($"Ingrese la clave para iniciar sesión como {correo}:", ForegroundColor.Cyan);
			var clave = Cons.GetSecretString();
			Cons.Clear();
			// buscamos entre los usuarios.
			Usuario user = DomiciliosApp.Instance.Compradores.Find(C => C.Correo == correo);
			user ??= DomiciliosApp.Instance.Vendedores.Find(V => V.Correo == correo);
			//si es null es porque no es encuentra ese usuario.
			if (user == null)
			{
				Cons.Error = $"No existe un usuario con correo {correo}, primero debes registrarte";
				_ = Cons.Key;
			}
			else
			{
				//validamos la clave
				if (user.Clave == clave)
				{
					DomiciliosApp.ClienteActual = user;
					//llamamos el callback, si existe
					OnLogin?.Invoke();
					// una vez logeados nos salimos del menos de logeo para ir a la app normal.
					Stop();
				}
				else
				{
					Cons.Error = $"La clave proporcionada no corresponde a la cuenta {correo}";
					_ = Cons.Key;
				}
			}
		}

		public TerminalPage LoginPage { get; private set; }

		public TerminalPage RegisterPage { get; private set; }

		public string Name => Title;

		public Action OnLogin { get; set; }

		public void Show() => Start();
	}
}