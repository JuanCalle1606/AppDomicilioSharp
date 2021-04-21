#pragma warning disable CS0612
using Gdk;
using Gtk;
using ICommon;
using ICommon.Bases;
using KYLib.Extensions;
using KYLib.System;
using KYLib.Utils;
using UmlBased;

namespace Linux
{
	public class LinuxFactory : IFactory
	{
		/// <summary>
		/// Devuelve el factory para crear app de gtk.
		/// </summary>
		public static IFactory Default => new LinuxFactory();

		/// <inheritdoc/>
		public IApp CreateApp()
		{
			//inicializamos gtk
			Application.Init();
			//cargamos los temas
			LoadTheme();
			//creamos la nueva app
			return new LinuxApp("org.tori.domiciliosharp", GLib.ApplicationFlags.None);
		}

		/// <summary>
		/// Carga el tema de windows y el logo de la aplicación para que gtk lo conozca.
		/// </summary>
		private static void LoadTheme()
		{
			//nuestros archivos de recursos dse encuentran en {InstallDir}/Recursos
			Assets.UpdateRelPath("Recursos");
			//el tema sera solo para windows
			if (Info.CurrentSystem.IsWindows())
			{
				//creamos un nuevo proveedor de estilos.
				var prov = new CssProvider();
				//cargamos el thema de windows dark
				prov.LoadFromPath(Assets.GetPath("wind.css"));
				//obtenemos la pantalla y le aplicamos el tema.
				var screen = Gdk.Display.Default.DefaultScreen;
				StyleContext.AddProviderForScreen(screen, prov, StyleProviderPriority.Application);
			}
			//anexamos al tema de iconos actual el icono de nuestra app.
			IconTheme.AddBuiltinIcon("logo", (int)IconSize.Menu, new Pixbuf(Assets.GetPath("logo.svg")));
#if DEBUG
			//advertimos al finalizar objetos
			GLib.Object.WarnOnFinalize = true;
#endif
		}

		/// <inheritdoc/>
		public ILoginWindow CreateLoginWindow(Usuario DefaultUser, System.Action onUserLogin)
		{
			if (DefaultUser == null)
			{
				var dev = new LinuxLoginWindow();
				dev.OnLogin = () =>
				{
					dev.Dispose();
					onUserLogin?.Invoke();
				};
				return dev;
			}
			DomiciliosApp.ClienteActual = DefaultUser;
			onUserLogin?.Invoke();
			return null;
		}

		/// <inheritdoc/>
		public IWindow CreateCompWindow() => new LinuxCompWindow();

		/// <inheritdoc/>
		public IWindow CreateVendWindow()
		{
			throw new System.NotImplementedException();
		}
	}
}