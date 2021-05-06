using System;
using Gtk;
using ICommon;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	partial class MenuDialogo
	{
		[UI] Entry ProdNombre = null;

		[UI] Dialog CrearProducto = null;

		void On_AgregarBtn_clicked(object o, EventArgs args)
		{
			ProdNombre.Text = string.Empty;
			CrearProducto.Show();
		}

		private void ProdDialogClose(object o, GLib.SignalArgs args)
		{
			args.RetVal = true;
			CrearProducto.Hide();
		}
	}
}