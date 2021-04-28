using FluentValidation;

namespace Api.Models.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateProductRequestValidator : ProductRequestBaseValidator<UpdateProductRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateProductRequestValidator() : base()
        {
            RuleFor(_ => _.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }
}
