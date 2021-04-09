namespace UmlBased
{
    /// <summary>
    /// Enumeración de tipos de clientes disponibles.
    /// </summary>
    public enum TipoComprador
    {
        /// <summary>
        /// Tipo de cliente por defecto.
        /// </summary>
        Normal,
        /// <summary>
        /// Tipo de cliente premium.
        /// </summary>
        VIP,
        /// <summary>
        /// Tipo de cliente preferencial con descuento por compras.
        /// </summary>
        Prioritario
    }

    /// <summary>
    /// Enumeración de estados de un pedido.
    /// </summary>
    public enum EstadoPedido
    {
        /// <summary>
        /// Estado por defecto, indica que el pedido esta en carrito y que no se a pagado.
        /// </summary>
		Carrito,
        /// <summary>
        /// Indica que se ha pagado unicamente la primera cuota por lo que el pedido esta en espera a ser entregado.
        /// </summary>
		Pagado,
        /// <summary>
        /// Indica que el pedido a sido entregado pero es por cuotas y aun faltan cuotas por pagar.
        /// </summary>
		Entregado,
        /// <summary>
        /// Indica que el pedido a sido entregado y todas las cuotas se han pagado.
        /// </summary>,
		Finalizado,
        /// <summary>
        /// Indica que el pedido fue cancelado.
        /// </summary>
        Cancelado
    }
}