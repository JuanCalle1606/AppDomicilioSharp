using System;
using Gdk;
using Gtk;

namespace Linux.Extensions
{
	internal class GtkPixInfo : IEquatable<GtkPixInfo>
	{
		public string Path { get; init; }
		public int Width { get; init; }
		public int Height { get; init; }

		public bool Equals(GtkPixInfo other)
		{
			return
			(Path.Equals(other.Path)) &&
			(Width.Equals(other.Width)) &&
			(Height.Equals(other.Height));
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Path, Width, Height);
		}
	}
	internal struct ImageLoad
	{
		public Image Image;
		public GtkPixInfo Info;
	}
}