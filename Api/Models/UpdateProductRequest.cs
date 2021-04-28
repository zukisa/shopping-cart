using System;

namespace Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateProductRequest : ProductRequestsBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
    }
}
