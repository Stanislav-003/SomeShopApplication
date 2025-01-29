using SomeShop.Application.Purchases.CreatePurchase;
using SomeShop.Domain.Users;

namespace SomeShop.Api.Controllers.Purchases;

public record CreatePurchaseRequest(Guid UserId, List<CreatePurchaseItemRequest> Items);
