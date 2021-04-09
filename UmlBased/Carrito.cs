﻿using System.Collections.Generic;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
    [DO(Newtonsoft.Json.MemberSerialization.OptIn)]
    /// <summary>
    /// Representa una cuenta de un establecimiento 
    /// </summary>
    public sealed class Carrito
    {
        private double total;
        private List<Pedido> productos;
        private double costo;

        public double Total { get => total; }
        public List<Pedido> Productos { get => productos; }
        public double Costo { get => costo; }

        public Carrito()
        {


        }

        public bool Agregar(Producto producto, double pago)
        {
            return true;
        }

        public bool Agregar(Producto producto)
        {
            return true;
        }

        public bool Pagar()
        {
            return true;
        }

        public bool Eliminar(Producto producto)
        {

            return true;
        }
    }
}