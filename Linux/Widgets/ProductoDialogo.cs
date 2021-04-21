using System;
using Gtk;
using KYLib.Extensions;
using Linux.Extensions;
using UmlBased;
using UI = Gtk.Builder.ObjectAttribute;

namespace Linux.Widgets
{
    public partial class ProductoWidget
    {
        [UI] Dialog Dialogo = null;
        [UI] Label Titulo = null;
        [UI] Label Precio = null;
        [UI] Label Domicilio = null;
        [UI] SpinButton Calificar = null;
        [UI] Button CalificarBtn = null;
        [UI] Image Foto = null;
        [UI] Label DescripcionDialogo = null;
        [UI] SpinButton CuotasDialogo = null;
        [UI] Label NombreVendedor = null;
        [UI] Label CalificacionVendedor = null;
        [UI] Button CarritoBtnDialogo = null;
        [UI] Button CerrarBtn = null;


        void on_VerBtn_clicked(object o, EventArgs args)
        {
            Dialogo.Show();
        }

        void ConfigurarDialogo()
        {
            Dialogo.DefaultResponse = ResponseType.Close;
            Titulo.Text = Producto.Name;
            DescripcionDialogo.Text = DescripcionDialogo.Text.Format(Producto.Descripcion);
            Precio.Text = Precio.Text.Format(Producto.Precio);
            //var vendedor = Producto.ObtenerVendedor();
            //NombreVendedor.Text = vendedor.Name;
            Foto.Load("https://media4.giphy.com/media/xUPGcAep2BZhomS0HC/giphy.gif", 250, 250);

        }



    }
}