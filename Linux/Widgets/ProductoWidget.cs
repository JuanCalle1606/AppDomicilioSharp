using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux.Widgets
{
    public class ProductoWidget : Box
    {
        public ProductoWidget() : this(new Builder("ProductoWidget.glade")) { }

        private ProductoWidget(Builder builder) : base(builder.GetObject("ProductoWidget").Handle)
        {
            builder.Autoconnect(this);
        }
    }
}
