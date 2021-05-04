using System;
using System.Linq;
using Gtk;
using ICommon;
using Linux.Widgets;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	class HistoryDialog : Dialog
	{
		[UI] ListBox ListaPedidos = null;

		public HistoryDialog() : this(new Builder("HistoryDialog.glade")) { }

		private HistoryDialog(Builder builder) : base(builder.GetRawOwnedObject("HistoryDialog"))
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Cancel;
			Response += Dialog_Response;
			DeleteEvent += Dialog_Response;
			builder.Dispose();
		}

		public void Actualizar()
		{
			var user = DomiciliosApp.ClienteActual as Comprador;
			var diff = user.HistorialPedidos.Count - ListaPedidos.Children.Length;
			if (diff == 0) return;
			var count = ListaPedidos.Children.Length;

			foreach (var item in user.HistorialPedidos.Skip(count))
				ListaPedidos.Add(new ProductoWidget(item.Producto));
			ListaPedidos.UnselectAll();
		}

		private void Dialog_Response(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			Hide();
			ListaPedidos.UnselectAll();
		}
	}
}
