using FluentValidation;
using FormApp.Application.DTOs.Auth;

namespace FormApp.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Auth.EmailIsRequired")
            .MaximumLength(255).WithMessage("Auth.EmailMaxLength255")
            .EmailAddress().WithMessage("Auth.EmailMustBeValid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Auth.PasswordIsRequired")
            .MinimumLength(6).WithMessage("Auth.PasswordMinLength6")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Auth.PasswordMustContainSpecialChar");
    }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        // Username is completely optional, no validation needed
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters");

        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");
    }
}
