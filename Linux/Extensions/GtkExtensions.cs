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
		private static HttpClient client = new();

		private static Dictionary<string, Pixbuf> cache = new();

		private static List<ImageLoad> Queue = new();

		private static bool loading = false;

		public static void Load(this Image image, string path, int width, int height)
		{
			if (string.IsNullOrWhiteSpace(path)) return;
			if (cache.ContainsKey(path))
			{
				image.Pixbuf = ResizePixbuf(cache[path], width, height);
				return;
			}
			var load = new ImageLoad()
			{
				Image = image,
				Path = path,
				Width = width,
				Height = height
			};
			Queue.Add(load);
			ProcessNextLoad();
		}

		private static async void ProcessNextLoad()
		{
			if (loading) return;
			if (Queue.Count < 1) return;
			loading = true;
			var cload = Queue[0];
			Queue.RemoveAt(0);
#if DEBUG
			Console.WriteLine($"Cargando desde \"{cload.Path}\"");
#endif
			var location = new Uri(cload.Path);
			if (location.IsFile && Pixbuf.GetFileInfo(cload.Path, out _, out _) != null)
			{
				var temp = new Pixbuf(cload.Path);
				cache[cload.Path] = temp;
				cload.Image.Pixbuf = ResizePixbuf(temp, cload.Width, cload.Height);
			}
			else
			{
				using (var st = await client.GetStreamAsync(location))
				{
					var temp = new Pixbuf(st);
					cache[cload.Path] = temp;
					cload.Image.Pixbuf = ResizePixbuf(temp, cload.Width, cload.Height);
				}
			}
#if DEBUG
			Console.WriteLine($"\"{cload.Path}\" se ha cargado");
#endif
			loading = false;
			ProcessNextLoad();
		}

		private static Pixbuf ResizePixbuf(Pixbuf pixbuf, int width, int height)
		{
			if (width == 0 || height == 0)
				return pixbuf;
			return pixbuf.ScaleSimple(width, height, InterpType.Bilinear);
		}
	}

	internal class ImageLoad
	{
		public Image Image;
		public string Path;
		public int Width;
		public int Height;
	}
}