using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Purchases;

namespace SomeShop.Application.Users.CreatePurchase;

public record CreatePurchaseCommand(
    Number number, 
    TotalPrice totalPrice,
    Guid userId,
    List<(Guid productId, int quantity, decimal price)> items) : ICommand<Guid>;
