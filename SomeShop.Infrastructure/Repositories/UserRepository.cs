using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Products;
using SomeShop.Domain.Users;

namespace SomeShop.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<User>> GetUsersByBirthday(CancellationToken ct = default)
    {
        var today = DateTime.UtcNow;

        return await _context.Set<User>()
            .Where(u => u.DateOfBirth.Month == today.Month && u.DateOfBirth.Day == today.Day)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<User>> GetRecentPurchasesAsync(int nDays, CancellationToken ct = default)
    {
        var recentDate = DateTime.UtcNow.AddDays(-nDays);

        return await _context.Set<User>()
            .Where(u => u.Purchases.Any(p => p.CreatedAt >= recentDate))
            .Include(u => u.Purchases)
            .ToListAsync(ct);
    }
}
