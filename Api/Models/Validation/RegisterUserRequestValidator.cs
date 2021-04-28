using FluentValidation;

namespace Api.Models.Validation
{
    /// <summary>
    /// Validator for User Registration
    /// </summary>
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisterUserRequestValidator()
        {
            RuleFor(_ => _.Email).NotEmpty().WithMessage("Email Address is required")
                                 .EmailAddress().WithMessage("Invalid email format")
                                 .MaximumLength(200).WithMessage("Only 200 characters for Email Address");
            RuleFor(_ => _.Password).NotEmpty().WithMessage("Password is required")
                                    .MaximumLength(20).WithMessage("Only 20 characters are allowed for Password")
                                    .MinimumLength(7).WithMessage("Password must be atleast 7 characters");
        }
    }
}
