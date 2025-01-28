using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Products;

namespace SomeShop.Domain.Purchases;

public class PurchaseItem : Entity
{
    public PurchaseItem(Guid id, Guid productId, Guid purchaseId, int quantity) : base(id)
    {
        ProductId = productId;
        PurchaseId = purchaseId;
        Quantity = quantity;
    }

    private PurchaseItem()
    {
        
    }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public Guid PurchaseId { get; private set; }
    public Purchase Purchase { get; private set; }
    public int Quantity { get; private set; }
}
