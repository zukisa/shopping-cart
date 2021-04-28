using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Validation
{
    /// <summary>
    /// Create Product Request validator
    /// </summary>
    public class CreateProductRequestValidator : ProductRequestBaseValidator<CreateProductRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateProductRequestValidator() : base(){}
    }
}
