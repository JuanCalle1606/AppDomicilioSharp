using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
	class HistoryDialog : Dialog
	{
		public HistoryDialog() : this(new Builder("HistoryDialog.glade")) { }

		private HistoryDialog(Builder builder) : base(builder.GetObject("HistoryDialog").Handle)
		{
			builder.Autoconnect(this);
			DefaultResponse = ResponseType.Cancel;

			Response += Dialog_Response;
		}

		private void Dialog_Response(object o, ResponseArgs args)
		{
			Hide();
		}
	}
}
