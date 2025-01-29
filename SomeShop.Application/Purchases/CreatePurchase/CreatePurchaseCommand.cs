using SomeShop.Application.Abstractions.Messaging;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Application.Purchases.CreatePurchase;

public record CreatePurchaseCommand(Guid UserId, List<CreatePurchaseItemRequest> Items) : ICommand<PurchaseId>;