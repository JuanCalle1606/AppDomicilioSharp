using System;
using Gdk;
using Gtk;
using ICommon;

namespace Linux.Extensions
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Almacena información sobre una ubicación y dimensiones de imagen.
	/// </summary>
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

		/// <summary>
		/// El has code lo sobreescribimos porque es algo importante ya que este objeto lo guardamos en cache por diccionrios lo cual guarda lo pares llave valor en una hashtable para mayor eficiencia.
		/// </summary>
		public override int GetHashCode()
		{
			return HashCode.Combine(Path, Width, Height);
		}
	}
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Guarda un par Imagen-info relacionados.
	/// </summary>
	internal struct ImageLoad
	{
		public Image Image;
		public GtkPixInfo Info;
	}
}