using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace AppDomicilioSharp.Visual.Dialogs
{
    class LoginDialog : Dialog
    {
        public LoginDialog() : this(new Builder("LoginDialog.glade")) { }

        private LoginDialog(Builder builder) : base(builder.GetObject("LoginDialog").Handle)
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
