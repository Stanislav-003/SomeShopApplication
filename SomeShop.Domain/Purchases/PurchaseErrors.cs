using SomeShop.Domain.Abstractions;

namespace SomeShop.Domain.Purchases;

public static class PurchaseErrors
{
    public static Error InvalidQuantity => new("Error.InvalidQuantity", "Quantity must be greater than zero.");
    public static Error ItemNotFound => new("Error.ItemNotFound", "Purchase item not found.");
    public static Error NotFountPurchases => new("Error.ItemNotFound", "User has no purchases.");
}
