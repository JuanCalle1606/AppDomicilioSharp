using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace AppDomiciloSharp.Visual.Windows
{
    class ClientHomeWindow : Window
    {
        public ClientHomeWindow() : this(new Builder("ClientHomeWindow.glade")) { }

        private ClientHomeWindow(Builder builder) : base(builder.GetObject("ClientHomeWindow").Handle)
        {
            builder.Autoconnect(this);
        }
    }
}
