using FluentValidation;

namespace Api.Models.Validation
{
    /// <summary>
    /// Base class for product validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ProductRequestBaseValidator<T> : AbstractValidator<T>
        where T : ProductRequestsBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected ProductRequestBaseValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().WithMessage("Product Name is required")
                                .MaximumLength(100).WithMessage("Only 100 characters allowed for Product Name");
            RuleFor(_ => _.Description).NotEmpty().WithMessage("Product Description is required")
                                       .MaximumLength(200).WithMessage("Only 200 characters allowed for Product Description");
            RuleFor(_ => _.Image).NotEmpty().WithMessage("Product image is required")
                                 .MaximumLength(100).WithMessage("Only 100 characters allowed for Product image");
            RuleFor(_ => _.Price).NotEmpty().WithMessage("Product Price is required");
            RuleFor(_ => _.StockLevel).NotEmpty().WithMessage("Product Stock Level is required");
        }
    }
}
