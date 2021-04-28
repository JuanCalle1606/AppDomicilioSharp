using System;
using Gtk;
using ICommon;
using ICommon.Bases;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{

	[Author("Juan Carlos Arbelaez")]
	/// <summary>
	/// Venatna principal de los usuarios de tipo vendedor.
	/// </summary>
	class LinuxVendWindow : Window, IWindow
	{
		public LinuxVendWindow() : this(new Builder("LinuxVendWindow.glade")) { }

		private LinuxVendWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxVendWindow"))
		{
			builder.Autoconnect(this);
		}
	}
}
