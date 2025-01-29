namespace SomeShop.Domain.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Categtory categtory, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetByIdAsync(IEnumerable<ProductId> productIds, CancellationToken cancellationToken);
}
