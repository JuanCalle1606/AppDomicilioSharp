using System;
using Gtk;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class LinuxVendWindow : Window
	{

		public Vendedor Vendedor { get; private set; }
		public LinuxVendWindow(Vendedor vendedor) : this(new Builder("LinuxVendWindow.glade"), vendedor) { }


		private LinuxVendWindow(Builder builder, Vendedor vendedor) : base(builder.GetRawOwnedObject("LinuxVendWindow"))
		{
			builder.Autoconnect(this);
			ConfigurarMenu();
			builder.Dispose();
		}

		private void ConfigurarMenu()
		{
			var User = (Vendedor)DomiciliosApp.ClienteActual
		}
	}
}
