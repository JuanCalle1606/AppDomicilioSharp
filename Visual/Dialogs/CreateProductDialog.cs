using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace AppDomicilioSharp.Visual.Dialogs
{
	class CreateProductDialog : Dialog
	{
		public CreateProductDialog() : this(new Builder("CreateProductDialog.glade")) { }

		private CreateProductDialog(Builder builder) : base(builder.GetObject("CreateProductDialog").Handle)
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
