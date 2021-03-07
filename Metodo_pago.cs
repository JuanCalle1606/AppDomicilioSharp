using System;

namespace AppDomicilioSharp
{
    class Metodo_pago
    {
        //Atributos
        private byte cuotas_totales;
        private byte cuotas_pagadas;
        private double preciototal;
        private bool finalizado;

        //Propiedades
        public byte Cuotas_totales { get => cuotas_totales; set => cuotas_totales = value; }
        public byte Cuotas_pagadas { get => cuotas_pagadas; set => cuotas_pagadas = value; }
        public double Preciototal { get => preciototal; set => preciototal = value; }
        public bool Finalizado { get => finalizado; set => finalizado = value; }

        //Constructor falta el try el catch 
        public Metodo_pago(byte cuotas_totales, byte cuotas_pagadas, double precio_total, bool finalizado)
        {

            this.Cuotas_totales = cuotas_totales;
            this.Cuotas_pagadas = cuotas_pagadas;
            this.Preciototal = precio_total;
            this.Finalizado = finalizado;


        }

    }

}

