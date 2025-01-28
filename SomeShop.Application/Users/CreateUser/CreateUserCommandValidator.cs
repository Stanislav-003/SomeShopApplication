using FluentValidation;

namespace SomeShop.Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.firstName).NotEmpty().Must(firstName => firstName.Value.Length is >= 2 and <= 50);
        RuleFor(c => c.lastName).NotEmpty().Must(lastName => lastName.Value.Length is >= 2 and <= 50);
        RuleFor(c => c.dateOfBirth).NotEmpty().Must(date => date <= DateTime.UtcNow).Must(date => date > DateTime.UtcNow.AddYears(-120));
    }
}
