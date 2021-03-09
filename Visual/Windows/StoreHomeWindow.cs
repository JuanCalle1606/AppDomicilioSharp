using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace AppDomicilioSharp.Visual.Windows
{
	class StoreHomeWindow : Window
	{
		public StoreHomeWindow() : this(new Builder("StoreHomeWindow.glade")) { }

		private StoreHomeWindow(Builder builder) : base(builder.GetObject("StoreHomeWindow").Handle)
		{
			builder.Autoconnect(this);
		}
	}
}
