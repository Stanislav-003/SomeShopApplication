using FluentValidation;

namespace SomeShop.Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.firstName).NotEmpty();

        RuleFor(x => x.lastName).NotEmpty();

        RuleFor(x => x.dateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future.")
            .GreaterThan(DateTime.Now.AddYears(-120)); // age > 120
    }
}
