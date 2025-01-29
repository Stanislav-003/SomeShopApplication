using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Products;
using SomeShop.Domain.Purchases;

namespace SomeShop.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Categtory categtory, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>()
            .Where(p => p.Category == categtory)
            .ToListAsync(cancellationToken);
    }
}
