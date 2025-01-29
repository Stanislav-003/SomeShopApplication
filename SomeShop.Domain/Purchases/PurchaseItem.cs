using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Products;
using SomeShop.Domain.Users;

namespace SomeShop.Domain.Purchases;

public class PurchaseItem
{
    private PurchaseItem()
    {
    }

    public PurchaseItemId Id { get; set; }
    public Quantity Quantity { get; set; }
    public PricePerUnit PricePerUnit { get; set; }
    public PurchaseId PurchaseId { get; set; }
    public ProductId ProductId { get; set; }

    public static Result<PurchaseItem> Create(Quantity quantity, PricePerUnit pricePerUnit, PurchaseId purchaseId, ProductId productId)
    {
        var purchase = new PurchaseItem
        {
            Id = new PurchaseItemId(Guid.NewGuid()),
            Quantity = quantity,
            PricePerUnit = pricePerUnit,
            PurchaseId = purchaseId,
            ProductId = productId
        };

        return purchase;
    }
}
