using SomeShop.Domain.Abstractions;

namespace SomeShop.Domain.Products;

public class Product : Entity
{
    public Product(Guid id, Name name, Categtory categtory, Article article, Price price) : base(id)
    {
        Name = name;
        Category = categtory;
        Article = article;
        Price = price;
    }

    private Product()
    {
    }

    public Name Name { get; private set; }
    public Categtory Category { get; private set; }
    public Article Article { get; private set; }
    public Price Price { get; private set; }
}
