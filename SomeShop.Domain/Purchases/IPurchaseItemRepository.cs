using SomeShop.Domain.Products;
using SomeShop.Domain.Users;

namespace SomeShop.Domain.Purchases;

public interface IPurchaseItemRepository
{
    Task<IEnumerable<PurchaseItem?>> GetByPurchaseIdAsync(PurchaseId id, CancellationToken ct = default);
    Task<IEnumerable<PurchaseItem?>> GetByProductIdAsync(ProductId id, CancellationToken ct = default);
}
