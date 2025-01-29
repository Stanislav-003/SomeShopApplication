namespace SomeShop.Domain.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Categtory categtory, CancellationToken cancellationToken = default);
}
