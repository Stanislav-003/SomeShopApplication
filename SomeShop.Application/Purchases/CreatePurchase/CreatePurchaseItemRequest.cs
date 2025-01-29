using SomeShop.Domain.Products;

namespace SomeShop.Application.Purchases.CreatePurchase;

public record CreatePurchaseItemRequest(Guid ProductId, int Quantity);