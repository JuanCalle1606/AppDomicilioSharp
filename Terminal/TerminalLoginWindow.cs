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
            int tipo_cuenta = 0;
			Console.WriteLine("Por favor indica el tipo de cuenta que deseas crear");
			Console.WriteLine("Ingresa '1' para crear una cuenta de tipo Comprador y '2' para crear una cuenta de tipo Vendedor");
            
			while( tipo_cuenta == 0)
			{ 
               try
			   {
                  tipo_cuenta = int.Parse(Console.ReadLine());
			   }
			   catch
			   {
				   tipo_cuenta = 0;
				   Console.WriteLine("Recuerda que debes ingresar '1' para crear una cuenta de tipo Comprador y '2' para crear una cuenta de tipo Vendedor");
			   }
			}

            bool cuenta_creada = false;

			while(cuenta_creada == false)
			{
				string nombre = "vacio";
				string direccion = "vacio";
				string clave = "vacio";
				string foto = "vacio";
				string telefono = "vacio";
				string correo = "vacio";

				try
				{
					Console.WriteLine("Por favor ingresa un nombre para tu cuenta");
					nombre = Console.ReadLine();
					Console.WriteLine("Por favor ingresa tu direccion de residencia o puesto de venta");
					direccion = Console.ReadLine();
					Console.WriteLine("Por favor ingresa una clave para tu cuenta");
					clave = Console.ReadLine();
					Console.WriteLine("Por favor ingresa una URL que contenga tu foto de perfil");
					foto = Console.ReadLine();
					Console.WriteLine("Por favor ingresa tu numero celular");
					telefono = Console.ReadLine();
					Console.WriteLine("Por favor ingresa tu correo electronico");
					correo = Console.ReadLine();
				}catch
				{
					Console.WriteLine("Uno o mas datos se han ingresado de forma incorrecta, por favor intenta de nuevo");
				}

				if(tipo_cuenta == 1)
				{
					try
					{
                        Usuario nueva_cuenta = new Comprador()
						{
							Id = DomiciliosApp.Instance.NextUserId++,
							Name = nombre,
				            Correo = correo,
							Direccion = direccion,
							Telefono = telefono,
							Creacion = DateTime.Now,
						};

						cuenta_creada = true;	
						
					} catch
					{
						Console.WriteLine("Ha ocurrido un error inesperado, por favor revisa los datos ingresados");
					}
				}

				else if (tipo_cuenta == 2)
				{
					try
					{
                        Usuario nueva_cuenta = new Vendedor()
						{
							Id = DomiciliosApp.Instance.NextUserId++,
							Name = nombre,
				            Correo = correo,
							Direccion = direccion,
							Telefono = telefono,
							Creacion = DateTime.Now,
						};

						cuenta_creada = true;	
						
					} catch
					{
						Console.WriteLine("Ha ocurrido un error inesperado, por favor revisa los datos ingresados");
					}
				}
			}
		
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