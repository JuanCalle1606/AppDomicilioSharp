using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Gdk;
using Gtk;

namespace Linux.Extensions
{
	public static class GtkExtensions
	{
		/// <summary>
		/// Cliente que usamos para descargar imagenes de internet.
		/// </summary>
		private static HttpClient client = new();

		/// <summary>
		/// Pares de Path-Pixbuf que actuan como cache.
		/// </summary>
		private static Dictionary<GtkPixInfo, Pixbuf> cache = new();

		/// <summary>
		/// Lista de imagenes a ser cargadas.
		/// </summary>
		private static List<ImageLoad> Queue = new();

		/// <summary>
		/// Indica si en el momento se esta cargando una iamgen.
		/// </summary>
		private static bool loading = false;

		/// <summary>
		/// Carga una imagen de forma asincronica.
		/// </summary>
		/// <param name="image">GtkImage en la cual guardamos la imagen.</param>
		/// <param name="path">Ubicación de la imagen.</param>
		/// <param name="width">Anchura de la imagen.</param>
		/// <param name="height">Altura de la imagen.</param>
		public static void Load(this Image image, string path, int width, int height)
		{
			// Ubicación vacia?
			if (string.IsNullOrWhiteSpace(path)) return;

			var info = new GtkPixInfo()
			{
				Path = path,
				Width = width,
				Height = height
			};

			// tenemos la imagen ya en cache?
			if (cache.ContainsKey(info))
			{
#if DEBUG
				Console.WriteLine($"\"{path}\" cargado de cache a {width}x{height}");
#endif
				// si todo esta ok, la imagen la ponemos.
				image.Pixbuf = cache[info];
				return;
			}
			// guardamos la información de lo que queremos cargar.
			var load = new ImageLoad()
			{
				Image = image,
				Info = info
			};
			// agregamos a la lista de carga.
			Queue.Add(load);
			// procesamos.
			ProcessNextLoad();
		}

		private static async void ProcessNextLoad()
		{
			// lista vacia o ya se esta cragando algo retornamos.
			if (loading || Queue.Count < 1) return;
			// indicamos quee stamos cargando algo.
			loading = true;
			// obtenemos el elemento a cargar y lo removemos de la lista.
			var cload = Queue[0];
			Queue.RemoveAt(0);
			// validamos el cache, igual que en el metodo superior.
			if (cache.ContainsKey(cload.Info))
			{
#if DEBUG
				Console.WriteLine($"\"{cload.Info.Path}\" cargado de cache a {cload.Info.Width}x{cload.Info.Height}");
#endif
				cload.Image.Pixbuf = cache[cload.Info];
				loading = false;
				ProcessNextLoad();
				return;
			}
#if DEBUG
			Console.WriteLine($"Cargando desde \"{cload.Info.Path}\" a {cload.Info.Width}x{cload.Info.Height}");
#endif
			// obtenemos la ubicacion del recurso
			var location = new Uri(cload.Info.Path);
			Stream st;
			// es un archivo? en caso de no serlo entonces es una url
			if (location.IsFile)
			{
				// validemos que sea un formato correcto de archivo.
				if (Pixbuf.GetFileInfo(cload.Info.Path, out _, out _) != null)
					st = File.OpenRead(cload.Info.Path);
				else
				{
					loading = false;
					ProcessNextLoad();
					return;
				}
			}
			else
			{
				// obtenemos el stream desde un recurso web
				st = await client.GetStreamAsync(location);
			}
			// iremos cargando de a 16384 bytes.
			byte[] buf = new byte[16384];
			int i;
			// loader usados para almacenar la información.
			var loader = new PixbufLoader();
			// establecemos el tamaño del "lienzo" en memoria
			loader.SetSize(cload.Info.Width, cload.Info.Height);
			// cuando este lsito el espacio en memoria asignamos el pixbuf
			loader.AreaPrepared += (_, _) => cload.Image.Pixbuf = loader.Pixbuf;

			// leemos el stream
			while ((i = await st.ReadAsync(buf, 0, 16384)) != 0)
			{
				// escribimos todos los datos en memoria
				loader.Write(buf, (ulong)i);
			}

#if DEBUG
			Console.WriteLine($"\"{cload.Info.Path}\" se ha cargado a {cload.Info.Width}x{cload.Info.Height}");
#endif
			// cerramos todo.
			st.Close();
			await st.DisposeAsync();
			loader.Close();
			loader.Dispose();
			// añadimos al cache
			cache.TryAdd(cload.Info, cload.Image.Pixbuf);
			// procesamos la siguiente imagen en la cola
			loading = false;
			ProcessNextLoad();
		}
	}
}