using System;
using KYLib.MathFn;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace UmlBased
{
    [DO(Newtonsoft.Json.MemberSerialization.OptIn)]
    /// <summary>
    /// Representa un pedido que se le ha hecho a un <see cref="Vendedor"/>
    /// </summary>
    public class Pedido
    {
        /// <inheritdoc/>
        [DP] public long Id { get; init; }

        /// <inheritdoc/>
        [DP] public Producto producto { get; init; }

        /// <inheritdoc/>
        [DP] public short Cantidad { get; init; }

        /// <inheritdoc/>
        [DP] public DateTime Fecha { get; init; }

        /// <inheritdoc/>
        [DP] public Real Precio { get; init; }

        /// <summary>
        /// Guarda al cliente que realizo este pedido en particular.
        /// </summary>
        [DP] public Comprador cliente { get; init; }

        /// <summary>
        /// Cancela un pedido, para cancelar un pedido debe ser de unica cuota o puede ser de multiples cuotas pero que solo se haya pagado una, de lo contrario no podra ser reembolsado.
        /// </summary>
        /// <returns>Retorna un valor que indica si se pudo o no cancelar el pedido.</returns>
        public bool Cancelar()
        {
            return true;
        }
    }
}