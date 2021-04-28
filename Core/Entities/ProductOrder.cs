using Core.Entities.Base;
using System;

namespace Core.Entities
{
    public class ProductOrder : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
