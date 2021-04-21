using GLib;
using Gtk;

namespace Linux
{
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

		private void Window_DeleteEvent(object o, SignalArgs args)
		{
			args.RetVal = true;
			Hide();
		}
	}
}
