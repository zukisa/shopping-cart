using System;

namespace Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Order items
        /// </summary>
        public ProductOrderRequest[] ProductOrders { get; set; }
    }
}
