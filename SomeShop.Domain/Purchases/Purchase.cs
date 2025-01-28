using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Users;

namespace SomeShop.Domain.Purchases;

public class Purchase : Entity
{
    private Purchase(Guid id, Number number, DateTime createdAt, TotalPrice totalPrice, Guid userId) : base(id)
    {
        Number = number;
        CreatedAt = createdAt;
        TotalPrice = totalPrice;
        UserId = userId;
    }

    private Purchase()
    {
        
    }
    public Number Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public TotalPrice TotalPrice { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    private readonly List<PurchaseItem> _items = new();
    public IReadOnlyCollection<PurchaseItem> Items => _items.AsReadOnly();

    public static Purchase Create(Number number, TotalPrice totalPrice, Guid userId)
    {
        var purchase = new Purchase(Guid.NewGuid(), number, DateTime.UtcNow, totalPrice, userId);
        
        return purchase;
    }

    public Result AddItem(Guid productId, int quantity, decimal price)
    {
        var item = new PurchaseItem(Guid.NewGuid(), productId, Id, quantity);
        _items.Add(item);

        RecalculateTotalPrice(price, quantity);

        return Result.Success();
    }

    private void RecalculateTotalPrice(decimal pricePerItem, int quantity)
    {
        TotalPrice = new TotalPrice(TotalPrice.Value + pricePerItem * quantity);
    }
}
