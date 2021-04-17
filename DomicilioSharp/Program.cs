using System;
using System.Collections.Generic;
using System.IO;
using ICommon;
using ICommon.Bases;
using KYLib.ConsoleUtils;
using KYLib.Data;
using KYLib.Data.DataFiles;
using KYLib.Extensions;
using KYLib.Helpers;
using KYLib.MathFn;
using KYLib.System;
using Linux;
using Newtonsoft.Json;
using Terminal;
using UmlBased;

namespace DomicilioSharp
{
	class Program
	{
		/// <summary>
		/// Guarda la ruta en la que se guardan los datos de aplicación
		/// </summary>
		public static readonly string SavesPath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"DomicilioShrap/saves.json"
		);

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
			//creamos la aplicacion;
			App = Factory.CreateApp();

			//creamos la ventana/dialogo de inicio de sesion;
			var login = Factory.CreateLoginWindow(DomiciliosApp.ClienteActual, OnUserLogin);
			App.AddWindow(login);
			login.Show();

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
			Cons.Line = "Usuario logeado!";
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

		private static Int CreateFactory()
		{
			//lo primero que validamos es si sera una aplicación de consola.
			if (ArgsList.Contains("-c"))
			{
				Factory = TerminalFactory.Default;
				return 0;
			}
			else if (Info.CurrentSystem.IsLinux() || ArgsList.Contains("--gtk"))
			{
				//creamos la aplicacion con Gtk;
				Factory = LinuxFactory.Default;
				return 0;
			}
			//esto deberia entrar unicamente cuando se buildea en windows y el usuario abre la aplicación con DomicilioSharp.exe en lugar de con Windows.exe, esto es valido pero no recomendable ya que lo que haremos es invocar el proceso por consola lo que ocacionara que se consuma la memoria de 2 procesos.
			else if (Info.CurrentSystem.IsWindows())
			{
				//invocar Windows.exe
				Environment.Exit(0);
				return 0;
			}

			Cons.Error = "El sistema operativo actual no esta soportado, si este sistema tiene instalado gtk ejecute este programa con el parametro \"--gtk\" para usar ese motor grafico o utilize \"-c\" para usar la aplicación por consola.";
			return 1;
		}

		/// <summary>
		/// Carga los datos desde los archivos.
		/// </summary>
		private static void RestorePreferences()
		{
			//ignoramos los errores numericos que ocurran
			ConvertHelper.IgnoreErrors = true;
			//obtenemos informacion del directorio en el que se guardaran los datos.
			var dir = new FileInfo(SavesPath).Directory;
			//si el directorio no existe lo creamos, esto unicamente deberia de ocurrir la primera vez que se ejecute la app pero tambien es posible que ocurra si el usuario ha borrado el directorio de guardado.
			if (!dir.Exists)
				dir.Create();
			// queremos guardar las referencias en el archivo de guardado, esto se hace para que no se generen objetos duplicados al cargar los datos.
			JsonFile.Default.Settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

			try
			{
				//intentamos cargar los datos guardados.
				//aqui puede generarse un error porque el archivo no existe o porque los datos estan malos.
				Files.Load<DomiciliosApp>(SavesPath, JsonFile.Default);
			}
			catch (Exception)
			{
				//valdiamos si el archivo esixte o no para saber cual fue el tipo de error.
				if (File.Exists(SavesPath))
					Cons.Error = "El archivo de guardado esta corrupto o no se puede leer.";
				else
					Cons.Error = "No se ha encontrado un archivo de guardado por lo que se creara uno.";

				//esta linea es por prevencio de errores aunque podria ser quitada ?
				DomiciliosApp.Instance = null;

				//si ocurre un error cargando los datos simplemente creamos unos datos nuevos, esto significa que toda la informacion esta perdida.
				new DomiciliosApp();
			}
		}

		/// <summary>
		/// se llama a este metodo antes de cerrar el programa para guardar las preferencias.
		/// </summary>
		public static void SavePreferences()
		{
			//guardamos los datos, aqui no es necesario crear la carpeta ya que ha sido creada al cargar los datos.
			Files.Save(DomiciliosApp.Instance, SavesPath, JsonFile.Default);
		}
	}
}
