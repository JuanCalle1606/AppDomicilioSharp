using System;
using Gtk;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class OrdenesDialog : Dialog
	{
		public OrdenesDialog() : this(new Builder("OrdenesDialog.glade")) { }

		private OrdenesDialog(Builder builder) : base(builder.GetObject("OrdenesDialog").Handle)
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Cancel;
			DeleteEvent += Dialog_Response;
			Response += Dialog_Response;
			MostrarPedidos();
		}

		private void MostrarPedidos()
		{
			var user = DomiciliosApp.ClienteActual as Vendedor;
			foreach (var item in user.Pedidos)
			{

			}
		}

		private void Dialog_Response(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			Hide();
		}
	}
}
