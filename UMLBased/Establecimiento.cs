using System.Collections.Generic;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace AppDomicilioSharp.UMLBased
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	/// <summary>
	/// Representa una cuenta de un establecimiento 
	/// </summary>
	public sealed class Establecimiento : Cuenta
	{
        private List<Producto> menu;
        private List<Categoria> categorias;
        private List<byte> calificacion;
        private List<DateTime> atencion_al_cliente;
        private List<Pedido> pedidos;

        public List<Producto> Menu { get => menu; }
        public List<Categoria> Categorias { get => categorias; }
        public List<byte> Calificacion { get => calificacion; }
        public List<DateTime> Atencion_al_cliente { get => atencion_al_cliente;  }
        public List<Pedido> Pedidos { get => pedidos;  }


        public bool AgregarPedido(Factura factura) 
        {
            return true;
        }


        public bool AgregarPedidos(List<Factura> facturas)
        {
            return true;
        }

        public bool Calificar(byte calificacion)
        {
            byte score = calificacion;
            this.calificacion.Add(score);
            return true;
        }
    }
}