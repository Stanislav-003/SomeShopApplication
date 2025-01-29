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

    public static Result<Purchase> Create(Number number, TotalPrice totalPrice, UserId userId)
    {
        var purchase = new Purchase
        {
            Id = new PurchaseId(Guid.NewGuid()),
            Number = number,
            CreatedAt = DateTime.UtcNow,
            TotalPrice = totalPrice,
            UserId = userId
        };

        return purchase;
    }

    public void AddPurchaseItem(PurchaseItem purchaseItem)
    {
        if (purchaseItem == null)
        {
            throw new ArgumentNullException(nameof(purchaseItem));
        }

        _purchaseItems.Add(purchaseItem);
    }

    //public Result<PurchaseItem> AddItem(Guid productId, int quantity, decimal price)
    //{
    //    var item = new PurchaseItem(Guid.NewGuid(), productId, Id, quantity);
    //    _items.Add(item);

    //    RecalculateTotalPrice(price, quantity);

    //    return item;
    //}

    //private void RecalculateTotalPrice(decimal pricePerItem, int quantity)
    //{
    //    TotalPrice = new TotalPrice(TotalPrice.Value + pricePerItem * quantity);
    //}
}
