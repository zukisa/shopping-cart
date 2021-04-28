using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductOrderRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Product Name 
        /// </summary>
        [JsonIgnore]
        public string ProductName { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        [JsonIgnore]
        public decimal Price { get; set; }
    }
}
