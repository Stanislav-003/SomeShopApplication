using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Users;

namespace SomeShop.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<User>> GetRecentPurchasesAsync(int nDays, CancellationToken ct = default)
    {
        var recentDate = DateTime.UtcNow.AddDays(-nDays);

        return await DbContext.Set<User>()
            .Where(u => u.Purchases.Any(p => p.CreatedAt >= recentDate))
            .Include(u => u.Purchases)  
            .ThenInclude(p => p.Items)  
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<User>> GetUsersByBirthday(DateTime? birthDay = null, CancellationToken ct = default)
    {
        var query = DbContext.Set<User>().AsQueryable();

        if (birthDay.HasValue)
        {
            var month = birthDay.Value.Month;
            var day = birthDay.Value.Day;

            query = query.Where(u => u.DateOfBirth.Month == month && u.DateOfBirth.Day == day);
        }

        return await query.ToListAsync(ct);
    }
}
