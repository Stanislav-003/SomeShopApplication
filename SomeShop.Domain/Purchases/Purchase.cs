using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Domain.Purchases;

public class Purchase
{
    private Purchase()
    {
    }

    public PurchaseId Id { get; private set; }
    public Number Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public TotalPrice TotalPrice { get; private set; }
    public UserId UserId { get; private set; }

    private readonly HashSet<PurchaseItem> _purchaseItems = new();
    public IReadOnlyCollection<PurchaseItem> PurchaseItems => _purchaseItems;

    public static Result<Purchase> Create(Number number, UserId userId)
    {
        var purchase = new Purchase
        {
            Id = new PurchaseId(Guid.NewGuid()),
            Number = number,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        return purchase;
    }

    public void AddPurchaseItems(IEnumerable<PurchaseItem> purchaseItems)
    {
        foreach (var item in purchaseItems)
        {
            _purchaseItems.Add(item);
        }
        RecalculateTotalPrice();
    }

    private void RecalculateTotalPrice()
    {
        TotalPrice = new TotalPrice(_purchaseItems.Sum(i => i.Quantity.Value * i.PricePerUnit.Value));
    }
}
