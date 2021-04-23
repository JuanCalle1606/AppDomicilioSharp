using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux
{
    class LinuxVendWindow : Window
    {
        public LinuxVendWindow() : this(new Builder("LinuxVendWindow.glade")) { }

        private LinuxVendWindow(Builder builder) : base(builder.GetRawOwnedObject("LinuxVendWindow"))
        {
            builder.Autoconnect(this);
        }
    }
}
