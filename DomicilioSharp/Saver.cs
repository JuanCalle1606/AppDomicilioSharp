using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using ICommon;
using KYLib.ConsoleUtils;
using KYLib.Data;
using KYLib.Data.DataFiles;
using KYLib.Helpers;
using KYLib.Utils;
using Newtonsoft.Json;
using UmlBased;

namespace DomicilioSharp
{
	partial class Program
	{
		/// <summary>
		/// Guarda la ruta en la que se guardan los datos de aplicación
		/// </summary>
		public static readonly Assets SavesPath = new Assets(Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"DomicilioSharp"
		));

		/// <summary>
		/// Archivo en el que se guardan los datos.
		/// </summary>
		public const string SaveFile = "saves.json";

		/// <summary>
		/// Archivo de respaldo, en caso de error. Guarda la información como estaba cuando se abrio la app en caso de un error.
		/// </summary>
		public const string BackFile = "saves.json.bck";

		/// <summary>
		/// Carga los datos desde los archivos.
		/// </summary>
		private static void RestorePreferences()
		{
			//ignoramos los errores numericos que ocurran
			ConvertHelper.IgnoreErrors = true;
			//obtenemos informacion del directorio en el que se guardaran los datos.
			var dir = new DirectoryInfo(SavesPath.SearchPath);
			//si el directorio no existe lo creamos, esto unicamente deberia de ocurrir la primera vez que se ejecute la app pero tambien es posible que ocurra si el usuario ha borrado el directorio de guardado.
			if (!dir.Exists)
				dir.Create();
			// queremos guardar las referencias en el archivo de guardado, esto se hace para que no se generen objetos duplicados al cargar los datos.
			JsonFile.Default.Settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

			try
			{
				//intentamos cargar los datos guardados.
				//aqui puede generarse un error porque el archivo no existe o porque los datos estan malos.
				Files.Load<DomiciliosApp>(SavesPath[SaveFile], JsonFile.Default);
				Cons.Trace("Datos de usuario cargados", ForegroundColor.Green);
			}
			catch (Exception
#if DEBUG
			e
#endif
			)
			{
#if DEBUG
				Cons.TraceError(e.Message);
				Cons.TraceError(e.StackTrace);
#else
				//validamos si el archivo existe o no para saber cual fue el tipo de error.
				if (File.Exists(SavesPath[SaveFile]))
				{
					Cons.Error = "El archivo de guardado esta corrupto o no se puede leer.";
					//creamos una copia de seguridad solo en caso de error.
					File.Copy(SavesPath[SaveFile], SavesPath[BackFile], true);
				}
				else
					Cons.Error = "No se ha encontrado un archivo de guardado por lo que se creara uno.";
#endif
				//esta linea es por prevencio de errores aunque podria ser quitada ?
				DomiciliosApp.Instance = null;

				//si ocurre un error cargando los datos simplemente creamos unos datos nuevos, esto significa que toda la informacion esta perdida.
				new DomiciliosApp();
			}
			//creamos una rutina de cada 6 horas usando mi libreria KYLib.
			Runner.Every(ValidarPagos, TimeSpan.FromHours(6), CancellationToken.None);
		}

		[Author("Juan Carlos Arbelaez")]
		/// <summary>
		/// Valida si se ha hecho el descuento de pagos cada dia.
		/// </summary>
		private static void ValidarPagos()
		{
			DateTime fecha = DateTime.Now;
			if (fecha.Day != DomiciliosApp.Instance.UltimoPago.Day)
			{
				DomiciliosApp.Instance.UltimoPago = fecha;
				DomiciliosApp.ProcesarDia();
			}
		}

		/// <summary>
		/// se llama a este metodo antes de cerrar el programa para guardar las preferencias.
		/// </summary>
		public static void SavePreferences()
		{
			Cons.Trace("Guardando datos de usuario", ForegroundColor.Green);
			//guardamos los datos, aqui no es necesario crear la carpeta ya que ha sido creada al cargar los datos.
			Files.Save(DomiciliosApp.Instance, SavesPath[SaveFile], JsonFile.Default);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		/// <summary>
		/// Función externa que usamos para cargar dlls de una ubicación arbitraria.
		/// </summary>
		private static extern bool SetDllDirectory(string lpPathName);
	}
}