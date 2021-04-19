using System.IO;
using Gdk;
using Gtk;

namespace Linux.Extensions
{
	public static class GtkExtensions
	{
		public static void Load(this Image image, string path, int width, int height)
		{
			if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
			{
				if (Pixbuf.GetFileInfo(path, out _, out _) != null)
				{
					if (width == 0 || height == 0)
					{
						image.Pixbuf = new Pixbuf(path);
					}
					else
					{
						using (var temp = new Pixbuf(path))
						{
							image.Pixbuf = temp.ScaleSimple(width, height, Gdk.InterpType.Bilinear);
						}
					}
				}
			}
		}
	}
}