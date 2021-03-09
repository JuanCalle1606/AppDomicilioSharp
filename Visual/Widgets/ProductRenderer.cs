using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace AppDomiciloSharp.Visual.Widgets
{
    public class ProductRenderer : Box
    {
        public ProductRenderer() : this(new Builder("ProductRenderer.glade")) { }

        private ProductRenderer(Builder builder) : base(builder.GetObject("ProductRenderer").Handle)
        {
            builder.Autoconnect(this);
        }
    }
}
