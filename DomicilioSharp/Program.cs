using System;
using System.Collections.Generic;
using System.IO;
using ICommon;
using ICommon.Bases;
using KYLib.ConsoleUtils;
using KYLib.Extensions;
using KYLib.MathFn;
using KYLib.System;
using Linux;
using Terminal;
using UmlBased;

namespace DomicilioSharp
{
	[Author("Juan Pablo Calle")]
	static partial class Program
	{
		/// <summary>
		/// Lista de argumentos pasados por consola.
		/// </summary>
		public static List<string> ArgsList;

		/// <summary>
		/// Aplicación principal que esta corriendo.
		/// </summary>
		public static IApp App;

		/// <summary>
		/// Factory que se usa para obtener las ventanas y componentes de la aplicación.
		/// </summary>
		public static IFactory Factory;

		[STAThread]
		/// <summary>
		/// Punto de entrada de DomicilioSharp
		/// </summary>
		static int Main(string[] args)
		{
			Mod.Init();
			ArgsList = new(args);
			return RunApp(true);
		}

		/// <summary>
		/// Metodo principal del programa, es el que ejecuta todo.
		/// </summary>
		/// <param name="create">Indica si se debe intancias el factory, se pasa false si ya hay uno definido.</param>
		public static Int RunApp(bool create)
		{
			// cargamos los datos guardados antes de cualquier cosa
			RestorePreferences();

			// debemos crear la app? esta propiedad siempre es true cuando el punto de inicio es este programa, si el punto de inicio es Windows.exe esto sera false.
			if (create)
			{
				var ec = CreateFactory();
				//si el codigo es diferente de 0 lo devolvemos.
				if (ec != 0)
					return ec;
			}

			try
			{
				//creamos la aplicacion;
				App = Factory.CreateApp();
			}
			catch (Exception
#if DEBUG
			e
#endif
			)
			{
#if DEBUG
				Cons.TraceError(e.Message);
#else
				// En Gtk puede ocurrir un error si no se encuentra gtk+ instalado en el sistema en cuyo caso terminamos la ejecución del programa con codigo de error.
				if (Factory is LinuxFactory)
					Cons.Error = "No ha sido posible crear la aplicación debido a que GTK+ no se encuentra instalado en el sistema. Use -c para usar la app en modo consola.";
				else// esto no deberia ocurrir jamas?
					Cons.Error = "Ha ocurrido un error inesperado en la ejecución del programa";
#endif
				return 0x1;
			}

			//creamos la ventana/dialogo de inicio de sesion;
			var login = Factory.CreateLoginWindow(null, OnUserLogin);
			App.AddWindow(login);
			login?.Show();

			// cargamos permisos de administrador
			LoadAdmin();

			//corremos la app
			var exitcode = (App?.StartApp()).GetValueOrDefault(1);
			//cuando todo termina guardamos los datos.
			SavePreferences();
			return exitcode;
		}

		/// <summary>
		/// Callback que debe ser ejecutado luego de que el usuario se loguee
		/// </summary>
		public static void OnUserLogin()
		{
			IWindow win = null;
			if (DomiciliosApp.ClienteActual is Comprador)
			{
				win = Factory.CreateCompWindow();
			}
			else if (DomiciliosApp.ClienteActual is Vendedor)
			{
				win = Factory.CreateVendWindow();
			}
			else
			{
				// WTF?
				Environment.Exit(1);
			}
			App.AddWindow(win);
			win.Show();
		}

		/// <summary>
		/// Crea el factory usado en el programa.
		/// </summary>
		private static Int CreateFactory()
		{
			//lo primero que validamos es si sera una aplicación de consola.
			if (ArgsList.Contains("-c"))
			{
				Factory = TerminalFactory.Default;
				return 0;
			}
			else if (Info.CurrentSystem.IsLinux() ||
					ArgsList.Contains("--gtk") ||
					Info.CurrentSystem.IsWindows())
			{
				// solo windows: cargamos las dll de la carpeta de auto instalación
				if (Info.CurrentSystem.IsWindows())
				{
					//3.24.24-win es un bundle personalizado que cree combiando dlls de msys2 y gtk3 runtime para permitir el uso correcto de temas y que por defecto tiene el tema window-10 de iconos en lugar de adwaita.
					var GtkPath = Path.Combine(
						Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
						@"Gtk\3.24.24-win\bin"
					);
					SetDllDirectory(GtkPath);
				}
				//creamos la aplicacion con Gtk;
				Factory = LinuxFactory.Default;
				return 0;
			}
			// aqui se llega si estamos corriendo por ejemplo en mac, la aplicación funciona en mac pero entonces es necesario pasar el --gtk y por supuesto haber instalado primero gtk.
			Cons.Error = "El sistema operativo actual no esta soportado, si este sistema tiene instalado gtk ejecute este programa con el parametro \"--gtk\" para usar ese motor grafico o utilize \"-c\" para usar la aplicación por consola.";
			return 1;
		}
	}
}
