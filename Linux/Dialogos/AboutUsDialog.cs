using GLib;
using Gtk;
using ICommon;

namespace Linux
{
	[Author("Juan Pablo Calle")]
	/// <summary>
	/// Dialogo que muestra información sobre los autores.
	/// </summary>
	class AboutUsDialog : AboutDialog
	{
		public AboutUsDialog() : this(new Builder("AboutUsDialog.glade")) { }

		private AboutUsDialog(Builder builder) : base(builder.GetRawOwnedObject("AboutUsDialog"))
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Close;
			DeleteEvent += Window_DeleteEvent;
			Response += Window_DeleteEvent;
			builder.Dispose();
		}

		/// <summary>
		/// Previene la eliminación de la ventana.
		/// </summary>
		private void Window_DeleteEvent(object o, SignalArgs args)
		{
			args.RetVal = true;
			Hide();
		}
	}
}
