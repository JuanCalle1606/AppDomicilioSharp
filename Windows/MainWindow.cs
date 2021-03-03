using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using KYLib.Utils;

namespace AppDomicilioSharp.Windows
{
    class MainWindow : Window
    {
        [UI] TreeView uiTree = null;

        [UI] Button _button1 = null;

		[UI] ListStore liststore1 = null;

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            _counter++;
            var iter = liststore1.Append();
            liststore1.SetValues(iter,Rand.GetInt(0,12),"Pepito",true);
        }
    }
}
