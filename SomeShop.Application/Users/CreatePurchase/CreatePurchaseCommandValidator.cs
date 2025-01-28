using FluentValidation;

namespace SomeShop.Application.Users.CreatePurchase;

internal class CreatePurchaseCommandValidator : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseCommandValidator()
    {
        RuleFor(c => c.number).NotNull().Must(number => number.Value.Length is >= 1 and <= 10);

        RuleFor(c => c.totalPrice).NotNull().Must(price => price.Value > 0);

        RuleFor(c => c.userId).NotEmpty();

        RuleFor(c => c.items)
            .NotEmpty()
            .Must(items => items.All(item => item.quantity > 0))
            .Must(items => items.All(item => item.price > 0));

        RuleForEach(c => c.items).ChildRules(items =>
        {
            items.RuleFor(item => item.productId).NotEmpty();

            items.RuleFor(item => item.quantity).GreaterThan(0);

            items.RuleFor(item => item.price).GreaterThan(0);
        });
    }
}
