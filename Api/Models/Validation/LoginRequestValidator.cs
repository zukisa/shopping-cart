using FluentValidation;

namespace Api.Models.Validation
{
    /// <summary>
    /// Login Request Validator
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginRequestValidator()
        {
            RuleFor(_ => _.Email).NotEmpty().WithMessage("Email Address is required");
            RuleFor(_ => _.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
