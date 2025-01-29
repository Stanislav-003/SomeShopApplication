using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;

namespace SomeShop.Infrastructure.Repositories;

public class PurchaseItemRepository : Repository<PurchaseItem>, IPurchaseItemRepository
{
    public PurchaseItemRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<PurchaseItem?>> GetByProductIdAsync(ProductId productId, CancellationToken ct = default)
    {
        return await _context.Set<PurchaseItem>()
            .Where(p => p.ProductId == productId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<PurchaseItem?>> GetByPurchaseIdAsync(PurchaseId purchaseId, CancellationToken ct = default)
    {
        return await _context.Set<PurchaseItem>()
            .Where(p => p.PurchaseId == purchaseId)
            .ToListAsync(ct);
    }
}
