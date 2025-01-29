using SomeShop.Domain.Abstractions;
using SomeShop.Domain.Purchases;
using SomeShop.Domain.Users;

namespace SomeShop.Domain.Products;

public class Product
{
    private Product()
    {
    }

    public ProductId Id { get; private set; }
    public Name Name { get; private set; }
    public Categtory Category { get; private set; }
    public Article Article { get; private set; }
    public Price Price { get; private set; }

    private readonly HashSet<PurchaseItem> _purchaseItems = new();
    public IReadOnlyCollection<PurchaseItem> PurchaseItems => _purchaseItems;

    public static Result<Product> Create(Name name, Categtory categtory, Article article, Price price)
    {
        var product = new Product
        {
            Id = new ProductId(Guid.NewGuid()),
            Name = name,
            Category = categtory,
            Article = article,
            Price = price
        };

        return product;
    }
}
