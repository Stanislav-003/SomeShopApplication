using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Purchases;
using System.Threading;

namespace SomeShop.Infrastructure.Repositories;

public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Purchase>> GetPurchasesByUserId(Guid userId, CancellationToken cancellation = default)
    {
        return await DbContext.Set<Purchase>()
           .Include(p => p.Items)
           .ThenInclude(i => i.Product)  
           .Where(p => p.UserId == userId)
           .ToListAsync(cancellation);

    }

    public async Task<IEnumerable<Purchase>> GetPurchasesForPeriod(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Purchase>()
            .Include(p => p.Items)
            .ThenInclude(i => i.Product)
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .ToListAsync(cancellationToken);
    }
}
