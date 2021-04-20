using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	partial class LinuxCompWindow
	{
		/// <summary>
		/// Muesta el dialogo de acerca de.
		/// </summary>
		void on_UserMenuAbout_activate(object o, EventArgs args) =>
			new AboutUsDialog();
	}
}